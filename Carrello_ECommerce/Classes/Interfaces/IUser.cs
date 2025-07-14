namespace Carrello_ECommerce.Classes.Interfaces
{
    public interface IUser
    {
        string Username { get; }
        string HashedPassword { get; }
        string FirstName { get; }
        string LastName { get; }
        ICart Cart { get; }
    }
}