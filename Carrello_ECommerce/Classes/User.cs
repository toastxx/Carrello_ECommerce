namespace Carrello_ECommerce.Classes
{
    public class User
    {
        #region Fields
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Cart Cart { get; private set; }
        #endregion

        #region Constructors
        public User(string username, string hashedPassword, string firstName, string lastName)
        {
            Username = username;
            HashedPassword = hashedPassword;
            FirstName = firstName;
            LastName = lastName;
            Cart = new Cart();
        }
        #endregion
    }
}