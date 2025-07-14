using Carrello_ECommerce.Classes.Interfaces;
using Carrello_ECommerce.Classes.Utils;

namespace Carrello_ECommerce.Classes
{
    public class ECommerceSystem
    {
        #region Fields
        private Dictionary<string, IUser> Users { get; set; }
        private IUser? CurrentUser { get; set; }
        private bool IsLogged => CurrentUser != null;
        private readonly string UsersFilePath = "users.json";
        #endregion

        #region Constructors
        public ECommerceSystem()
        {
            Users = LoadUsers();
        }
        #endregion

        #region Methods
        private Dictionary<string, IUser> LoadUsers()
        {
            if (File.Exists(UsersFilePath))
            {
                var json = File.ReadAllText(UsersFilePath);
                var loadedUsers = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, User>>(json)
                    ?? new Dictionary<string, User>();

                var result = new Dictionary<string, IUser>();
                foreach (var kvp in loadedUsers)
                {
                    result[kvp.Key] = kvp.Value;
                }
                return result;
            }
            return new Dictionary<string, IUser>();
        }

        private void SaveUsers()
        {
            var usersToSave = new Dictionary<string, User>();
            foreach (var kvp in Users)
            {
                if (kvp.Value is User user)
                {
                    usersToSave[kvp.Key] = user;
                }
            }

            var json = System.Text.Json.JsonSerializer.Serialize(usersToSave);
            File.WriteAllText(UsersFilePath, json);
        }

        public bool Register(string username, string password, string firstName, string lastName)
        {
            if (Users.ContainsKey(username))
                return false;

            var hashedPassword = PasswordHasher.HashPassword(password);
            Users[username] = new User(username, hashedPassword, firstName, lastName);
            SaveUsers();
            return true;
        }

        public bool Login(string username, string password)
        {
            if (!Users.ContainsKey(username))
                return false;

            var user = Users[username];
            if (PasswordHasher.VerifyPassword(password, user.HashedPassword))
            {
                CurrentUser = user;
                return true;
            }
            return false;
        }

        public void Logout()
        {
            CurrentUser = null;
        }

        public void RunMenu()
        {
            while (true)
            {
                if (!IsLogged)
                {
                    Console.WriteLine("\n1. Registrati");
                    Console.WriteLine("2. Login");
                    Console.WriteLine("3. Esci");
                    Console.Write("Scegli un'opzione: ");

                    switch (Console.ReadLine())
                    {
                        case "1":
                            RegisterMenu();
                            break;
                        case "2":
                            LoginMenu();
                            break;
                        case "3":
                            return;
                        default:
                            Console.WriteLine("Opzione Invalida");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("\n1. Aggiungi Prodotto");
                    Console.WriteLine("2. Rimuovi Prodotto");
                    Console.WriteLine("3. Modifica Prodotto");
                    Console.WriteLine("4. Mostra Carrello");
                    Console.WriteLine("5. Logout");
                    Console.WriteLine("6. Esci");
                    Console.Write("Scegli un'opzione: ");

                    switch (Console.ReadLine())
                    {
                        case "1":
                            AddProductMenu();
                            break;
                        case "2":
                            RemoveProductMenu();
                            break;
                        case "3":
                            ModifyProductMenu();
                            break;
                        case "4":
                            CurrentUser?.Cart.DisplayCart();
                            break;
                        case "5":
                            Logout();
                            Console.WriteLine("Logout completato");
                            break;
                        case "6":
                            return;
                        default:
                            Console.WriteLine("Opzione Invalida");
                            break;
                    }
                }
            }
        }

        private void RegisterMenu()
        {
            Console.Write("Inserisci username: ");
            var username = Console.ReadLine() ?? "";
            Console.Write("Inserisci password: ");
            var password = Console.ReadLine() ?? "";
            Console.Write("Inserisci nome: ");
            var firstName = Console.ReadLine() ?? "";
            Console.Write("Inserisci cognome: ");
            var lastName = Console.ReadLine() ?? "";

            if (Register(username, password, firstName, lastName))
                Console.WriteLine("Registrazione completata");
            else
                Console.WriteLine("Username già presente");
        }

        private void LoginMenu()
        {
            Console.Write("Inserisci username: ");
            var username = Console.ReadLine() ?? "";
            Console.Write("Inserisci password: ");
            var password = Console.ReadLine() ?? "";

            if (Login(username, password))
                Console.WriteLine("Login completato");
            else
                Console.WriteLine("Username o password invalido/a");
        }

        private void AddProductMenu()
        {
            if (CurrentUser == null) return;

            Console.Write("Inserisci nome prodotto: ");
            var name = Console.ReadLine() ?? "";
            Console.Write("Inserisci prezzo: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price)) return;
            Console.Write("Inserisci quantità: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity)) return;
            Console.Write("Inserisci % sconto (0 se nulla): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal discount)) return;

            CurrentUser.Cart.AddProduct(name, price, quantity, discount);
            SaveUsers();
        }

        private void RemoveProductMenu()
        {
            if (CurrentUser == null) return;

            Console.Write("Inserisci nome prodotto per rimuovere: ");
            var name = Console.ReadLine() ?? "";
            CurrentUser.Cart.RemoveProduct(name);
            SaveUsers();
        }

        private void ModifyProductMenu()
        {
            if (CurrentUser == null) return;

            Console.Write("Inserisci nome prodotto per modificare: ");
            var name = Console.ReadLine() ?? "";

            Console.Write("Inserisci nuova quantità (premi Enter per saltare): ");
            var quantityInput = Console.ReadLine();
            int? newQuantity = !string.IsNullOrEmpty(quantityInput) && int.TryParse(quantityInput, out int q) ? q : null;

            Console.Write("Inserisci nuovo prezzo (premi Enter per saltare): ");
            var priceInput = Console.ReadLine();
            decimal? newPrice = !string.IsNullOrEmpty(priceInput) && decimal.TryParse(priceInput, out decimal p) ? p : null;

            Console.Write("Inserisci la nuova % sconto (premi Enter per saltare): ");
            var discountInput = Console.ReadLine();
            decimal? newDiscount = !string.IsNullOrEmpty(discountInput) && decimal.TryParse(discountInput, out decimal d) ? d : null;

            CurrentUser.Cart.UpdateProduct(name, newQuantity, newPrice, newDiscount);
            SaveUsers();
        }
        #endregion
    }
}