using Supermarket.Logic.Data;
using System.Collections.Generic;
using System.Linq;

namespace Supermarket.Logic
{
    public class Checkout : ICheckout
    {
        private readonly IDataRepository _repo;

        private Dictionary<string, CartItem> _cart = new Dictionary<string, CartItem>();

        public Checkout(IDataRepository repo)
        {
            _repo = repo;
        }

        public void Scan(string item)
        {
            var product = _repo.LookupProduct(item);
            if (product != null)
            {
                if (_cart.TryGetValue(product.Sku, out CartItem cartItem))
                {
                    // Increment quantity
                    cartItem.Quantity += 1;
                }
                else
                {
                    // Add item to cart
                    _cart.Add(product.Sku, new CartItem(product, 1));
                }
            }
        }

        public int GetTotalPrice()
        {
            return _cart.Values
                .Sum(ci => ci.Product.UnitPrice * ci.Quantity);
        }        
    }
}
