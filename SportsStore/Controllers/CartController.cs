using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Infrastructure;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        private Cart cart;

        public CartController(IProductRepository repo, Cart cartService)
        {
            this.repository = repo;
            this.cart = cartService;
        }

        //private Cart GetCart()
        //{
        //    return HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
        //}

        //private void SaveCart(Cart cart)
        //{
        //    HttpContext.Session.SetJson("Cart", cart);
        //}

        public RedirectResult AddToCart(int productID, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productID);

            if (product != null)
            {
                //Cart cart = this.GetCart();
                //cart.AddItem(product, 1);

                //this.SaveCart(cart);
                cart.AddItem(product, 1);
            }

            return Redirect(returnUrl);
            //return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int productID, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productID);

            if (product != null)
            {
                //Cart cart = GetCart();
                //cart.RemoveLine(product);

                //this.SaveCart(cart);
                cart.RemoveLine(product);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public IActionResult Index(string returnUrl)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Для оформления заказа заполните корзину!");
            }
            
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }
    }
}