using Carrello_ECommerce.Classes.Interfaces;

namespace Carrello_ECommerce.Classes
{
    public class User : IUser
    {
        #region Fields
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICart Cart { get; private set; }
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