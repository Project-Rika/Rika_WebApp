using Microsoft.AspNetCore.Mvc;
using Rika_WebApp.Models;
using Rika_WebApp.ViewModels;
using System.Text.Json;

namespace Rika_WebApp.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            List<CartItemModel> cart;
            if (Request.Cookies.TryGetValue("Cart", out var cartJson))
            {
                cart = JsonSerializer.Deserialize<List<CartItemModel>>(cartJson) ?? [];
            }
            else
            {
                cart = [];
            }
            return View(cart);
        }


        [HttpPost]
        public IActionResult AddToCart([FromBody] ProductViewModel product)
        {
            List<CartItemModel> cart;
            if (Request.Cookies.TryGetValue("Cart", out var cartJson))
            {
                cart = JsonSerializer.Deserialize<List<CartItemModel>>(cartJson) ?? [];
            }
            else
            {
                cart = [];
            }

            var cartItem = cart.Find(item => item.ArticleNumber == product.ArticleNumber);
            if (cartItem != null)
            {
                cartItem.Quantity += product.Quantity;
            }
            else
            {
                var newCartItem = new CartItemModel
                {
                    ArticleNumber = product.ArticleNumber!,
                    ProductName = product.ProductName!,
                    Quantity = product.Quantity,
                    Price = product.Price
                };
                cart.Add(newCartItem);
            }

            var options = new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(1) };
            Response.Cookies.Append("Cart", JsonSerializer.Serialize(cart), options);

            return Json(new { cartCount = cart.Sum(p => p.Quantity) });
        }


        [HttpPost]
        public IActionResult EmptyCart()
        {
            if (Request.Cookies["Cart"] != null)
            {
                Response.Cookies.Delete("Cart");
            }

            return RedirectToAction("Index"); 
             
        }

        public IActionResult UpdateCartCount()
        {
            return ViewComponent("CartCount");
        }
    }
}