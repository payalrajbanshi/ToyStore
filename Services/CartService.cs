//creating cart with session
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using ToyStore.Models;

namespace ToyStore.Services
{
    public class CartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string CartSessionKey = "CartSession";

        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public List<CartItem> GetCartItems()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cartJson = session.GetString(CartSessionKey);
            if (string.IsNullOrEmpty(cartJson)) return new List<CartItem>();
            return JsonConvert.DeserializeObject<List<CartItem>>(cartJson);
        }

        public void SaveCartItems(List<CartItem> items)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var json = JsonConvert.SerializeObject(items);
            session.SetString(CartSessionKey, json);
        }

        public void AddToCart(Product product)
        {
            var items = GetCartItems();
            var existing = items.FirstOrDefault(i => i.ProductId == product.Id);
            if (existing != null)
            {
                existing.Quantity++;
            }
            else
            {
                items.Add(new CartItem { ProductId = product.Id, Product = product, Quantity = 1 });
            }
            SaveCartItems(items);
        }

        public void RemoveFromCart(int productId)
        {
            var items = GetCartItems();
            var existing = items.FirstOrDefault(i => i.ProductId == productId);
            if (existing != null)
            {
                items.Remove(existing);
                SaveCartItems(items);
            }
        }

        public void ClearCart()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            session.Remove(CartSessionKey);
        }
    }
}
