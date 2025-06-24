using Microsoft.AspNetCore.Mvc;
using ToyStore.Data;
using ToyStore.Services;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ToyStore.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _cartService;
        private readonly AppDbContext _context;

        public CartController(CartService cartService, AppDbContext context)
        {
            _cartService = cartService;
            _context = context;
        }

        public IActionResult Index()
        {
            var cartItems = _cartService.GetCartItems();

            // Load product details for each cart item
            foreach (var item in cartItems)
            {
                if (item.Product == null)
                {
                    item.Product = _context.Products.FirstOrDefault(p => p.Id == item.ProductId);
                }
            }

            return View(cartItems);
        }

        public IActionResult Add(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            _cartService.AddToCart(product);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            _cartService.RemoveFromCart(id);
            return RedirectToAction("Index");
        }

        public IActionResult Clear()
        {
            _cartService.ClearCart();
            return RedirectToAction("Index");
        }
    }
}
