namespace Carrello_ECommerce.Classes
{
    public class Product
    {
        #region Fields
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal DiscountPercentage { get; set; }
        #endregion

        #region Constructors
        public Product(string name, decimal price, int quantity, decimal discountPercentage = 0)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
            DiscountPercentage = discountPercentage;
        }
        #endregion

        #region Methods
        // Metodo per calcolare il prezzo totale
        public decimal GetTotalPrice()
        {
            return Price * Quantity * (1 - DiscountPercentage / 100);
        }
        #endregion
    }
}