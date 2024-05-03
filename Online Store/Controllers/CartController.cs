using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using Online_Store.Data;
using Online_Store.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;


namespace Online_Store.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult AddToCart()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int id,string action)
        {
            try
            {
                // If the user doesn't have a cart, create a new one
                var userId = HttpContext.Session.GetInt32("UserID");
                if (userId == null)
                            {
                               // Redirect to login or registration page if user is not authenticated
                                return RedirectToAction("Login", "User");
                            }
                    var cart = _context.Carts.Include(c => c.ProductCards)
                                                        .FirstOrDefault(c => c.UserId == Convert.ToInt32(userId));
                if (cart == null)
                {
                    // If the user doesn't have a cart, create a new one
                    cart = new Cart
                    {
                        UserId = Convert.ToInt32(userId),
                        Quantity = 1,
                        // ProductCards = new List<ProductCart>()
                    };
                    _context.Carts.Add(cart);
                _context.SaveChanges();

                 }

                // Add the product to the cart
                ProductCart productcart = new ProductCart
                {
                    ProductId = id
                    ,
                    CartId =cart.CartId
                };
                _context.ProductCarts.Add(productcart);

                // Save changes to the database
                _context.SaveChanges();

                // Set a success message to be displayed after redirection
                TempData["successData"] = "Item has been added successfully";

                // Redirect to a specific action or view after adding to cart
                // For example, you can redirect to the cart index page
                ViewBag.Message = "Change succesfully";
                return RedirectToAction(action, "Product");
              TempData["msg"] = "<script>alert('Change succesfully');</script>";

            }
            catch (Exception ex)
            {
                // Handle exceptions if any
                // You may want to log the exception or show an error message to the user
                TempData["errorData"] = "An error occurred while adding the item to the cart.";
                return RedirectToAction("Index", "Product");
                TempData["msg"] = "<script>alert('Sorry,it's faild');</script>";


            }
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult AddToCart(int productId)
        //{
        //    try
        //    {
        //        // Check if user is authenticated and retrieve user ID from session
        //        var userId = HttpContext.Session.GetInt32("UserID");

        //        if (userId == null)
        //        {
        //            // Redirect to login or registration page if user is not authenticated
        //            return RedirectToAction("Login", "User");
        //        }

        //        // Check if the product exists
        //        //var product = _context.Products.FirstOrDefault(p => p.ProductId == productId);
        //        //if (product == null)
        //        //{
        //        //    // Set an error message to inform the user that the product does not exist
        //        //    TempData["errorData"] = "The selected product does not exist.";
        //        //    return RedirectToAction("ClothesIndex", "Product");
        //        //}

        //        // Check if the user already has a cart
        //        var cart = _context.Carts.Include(c => c.ProductCards)
        //                                 .FirstOrDefault(c => c.UserId == userId);
        //        if (cart == null)
        //        {
        //            // If the user doesn't have a cart, create a new one
        //            cart = new Cart
        //            {
        //                UserId =Convert.ToInt32(userId),
        //                Quantity = 1,
        //                //ProductCards = new List<ProductCart>()
        //            };
        //            _context.Carts.Add(cart);
        //        }

        //        // Add the product to the cart
        //        cart.ProductCards.Add(new ProductCart
        //        {
        //            ProductId = productId
        //            ,
        //            CartId = cart.CartId
        //        });

        //        // Save changes to the database
        //        _context.SaveChanges();

        //        // Set a success message to be displayed after redirection
        //        TempData["successData"] = "Item has been added successfully";

        //        // Redirect to a specific action or view after adding to cart
        //        return RedirectToAction("Index", "Product");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exceptions if any
        //        TempData["errorData"] = "An error occurred while adding the item to the cart.";
        //        return RedirectToAction("ClothesIndex", "Product");
        //    }
        //}



    }
}
