namespace Carrello_ECommerce.Classes.Interfaces
{
    public interface ICart
    {
        void AddProduct(string name, decimal price, int quantity, decimal discountPercentage = 0);
        void RemoveProduct(string name);
        void UpdateProduct(string name, int? newQuantity = null, decimal? newPrice = null, decimal? newDiscount = null);
        decimal GetTotal();
        void DisplayCart();
    }
}