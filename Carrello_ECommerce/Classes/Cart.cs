using Carrello_ECommerce.Classes.Interfaces;

namespace Carrello_ECommerce.Classes
{
    public class Cart : ICart
    {
        #region Fields
        private List<Product> Products { get; set; } = new List<Product>();
        #endregion

        #region Methods
        public void AddProduct(string name, decimal price, int quantity, decimal discountPercentage = 0)
        {
            var existingProduct = Products.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (existingProduct != null)
            {
                existingProduct.Quantity += quantity;
            }
            else
            {
                Products.Add(new Product(name, price, quantity, discountPercentage));
            }
        }

        public void RemoveProduct(string name)
        {
            Products.RemoveAll(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public void UpdateProduct(string name, int? newQuantity = null, decimal? newPrice = null, decimal? newDiscount = null)
        {
            var product = Products.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (product != null)
            {
                if (newQuantity.HasValue) product.Quantity = newQuantity.Value;
                if (newPrice.HasValue) product.Price = newPrice.Value;
                if (newDiscount.HasValue) product.DiscountPercentage = newDiscount.Value;
            }
        }

        public decimal GetTotal()
        {
            return Products.Sum(p => p.GetTotalPrice());
        }

        public void DisplayCart()
        {
            Console.WriteLine("\n=== Cart Contents ===");
            foreach (var product in Products)
            {
                Console.WriteLine($"Product: {product.Name}");
                Console.WriteLine($"Quantity: {product.Quantity}");
                Console.WriteLine($"Price: €{product.Price:F2}");
                Console.WriteLine($"Discount: {product.DiscountPercentage}%");
                Console.WriteLine($"Total: €{product.GetTotalPrice():F2}");
                Console.WriteLine("-------------------");
            }
            Console.WriteLine($"Cart Total: €{GetTotal():F2}\n");
        }
        #endregion
    }
}