using App.Library;
using AspCoreMvc_App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreMvc_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StudentDetailContext _context;

        public HomeController(ILogger<HomeController> logger, StudentDetailContext context)
        {
            _logger = logger;
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        [Route("[action]")]
        public IActionResult Index()
        {
            var allCategories = GetRequest.GetCategories();
            ViewBag.category = allCategories;
            return View(allCategories);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("[action]/{categoryName}")]
        public IActionResult Index(string categoryName)
        {
            
            var allCategories = GetRequest.GetCategories();
            var category = allCategories.Where(x => x.CategoryName == categoryName).FirstOrDefault();
            ViewBag.category = category;
            return View(allCategories);
        }




        [Route("[action]")]
        public async Task<IActionResult> CartAsync([FromQuery(Name = "gameId")] int gameId)
        {
            string userId = User.Identity.Name;
            var cartItems = GetRequest.GetCartItems(userId);
            var purchased = GetRequest.GetPurchases(userId);

            //if (!HttpContext.Request.Cookies.ContainsKey(".AspNetCore.Identity.Application"))
            //{
            //string returnUrl = HttpContext.Request.Query["ReturnUrl"].ToString();
            //return Redirect(returnUrl);
            //return RedirectToAction("Index", "UserData", new { redirectUrl = redirectUrl, redirectCtrl = redirectCtrl, gameId = gameId });
            //    return View();
            //}
            //else
            //{
            ViewBag.totalPrice = 0.00;
            if (gameId != 0)
            {
                //While working on Docker container
                var url = "http://cart.api/api/cart/post";

                //While working on local
                //var url = "http://localhost:5004/api/cart/post";


                var game = GetRequest.GetGameById(gameId);
                if (!Helper.isCartItem(game, cartItems) && !Helper.isPurchased(game, purchased))
                {
                    CartItemDetail gameToAdd = new CartItemDetail
                    {
                        GameName = game.GameName,
                        GamePrice = game.GamePrice,
                        ImageUrl = game.ImageUrl,
                        Publisher = game.Publisher,
                        UserID = userId
                    };
                    await PostRequest.PostApiAsync(url, gameToAdd);
                    cartItems = GetRequest.GetCartItems(userId);
                }

                foreach (var item in cartItems)
                {
                    ViewBag.totalPrice += item.GamePrice;
                }
                if (cartItems != null)
                    ViewBag.totalPrice = Math.Round(ViewBag.totalPrice, 2);
                return View(cartItems);
            }
            else
            {
                foreach (var item in cartItems)
                {
                    ViewBag.totalPrice += item.GamePrice;
                }
                if (cartItems != null)
                    ViewBag.totalPrice = Math.Round(ViewBag.totalPrice, 2);
                return View(cartItems);
            }
            //}

        }

        [Route("[action]")]
        public async Task<IActionResult> CartRemoveAsync(int id)
        {
            var userId = User.Identity.Name;
            //While working on Docker container
            var url = $"http://cart.api/api/cart/delete?id={id}&userId={userId}";

            //While working on local
            //var url = $"http://localhost:5004/api/cart/delete?id={id}&userId={userId}";


            await PostRequest.DeleteApiAsync(url);

            return RedirectToAction("Cart");
        }

        [Route("[action]")]
        public IActionResult Purchases()
        {
            string userId = User.Identity.Name;

            var purchased = GetRequest.GetPurchases(userId);
            return View(purchased);
        }

        [Route("[action]")]
        public async Task<IActionResult> CheckoutAsync()
        {
            string userId = User.Identity.Name;
            var cartItems = GetRequest.GetCartItems(userId);

            //While working on Docker container
            var url = "http://order.api/api/order/post";

            //While working on local
            //var url = "http://localhost:5006/api/order/post";

            await PostRequest.PostApiAsync(url, cartItems);
            return RedirectToAction("Purchases");
        }


        //public bool isPurchased(GameDetailResponse game, List<List<GameOrderLink>> p)
        //{
        //    foreach (var i in p)
        //    {
        //        foreach (var j in i)
        //        {
        //            if (j.Game.GameName == game.GameName)
        //                return true;
        //        }
        //    }
        //    return false;
        //}

        //public bool isCartItem(GameDetailResponse game, List<CartItemDetail> cart)
        //{
        //    if (cart.Where(x => x.GameName == game.GameName).FirstOrDefault() != null)
        //    {
        //        return true;
        //    }
        //    return false;
        //}
    }
}
