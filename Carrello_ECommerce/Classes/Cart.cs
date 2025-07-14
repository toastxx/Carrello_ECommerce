using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrello_ECommerce.Classes
{
    public class Cart
    {
        #region Fields
        private List<Product> Products { get; set; } = new List<Product>();
        #endregion

        #region Methods
        // Metodo per aggiungere un prodotto al carrello
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

        // Metodo per rimuovere un prodotto
        public void RemoveProduct(string name)
        {
            Products.RemoveAll(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        // Metodo per aggiornare un prodotto
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

        // Metodo per ottenere il totale del carrello
        public decimal GetTotal()
        {
            return Products.Sum(p => p.GetTotalPrice());
        }

        // Metodo per mostrare i prodotti nel carrello
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