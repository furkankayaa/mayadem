using AspCoreMvc_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AspCoreMvc_App.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using App.Library;
using System.Security.Claims;

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
        [Route("[action]/{categoryId}")]
        public IActionResult Index(int categoryId)
        {

            var allCategories = GetRequest.GetCategories();
            var category = allCategories.Where(x => x.GenreID == categoryId).FirstOrDefault();
            ViewBag.category = category;
            return View(allCategories);
        }

        
        

        [Route("[action]")]
        public async Task<IActionResult> CartAsync([FromQuery(Name = "redirectUrl")] string redirectUrl, [FromQuery(Name = "redirectCtrl")] string redirectCtrl, [FromQuery(Name = "gameId")] int gameId)
        {
            string userName = User.Identity.Name;
            var cartItems = GetRequest.GetCartItems(userName);

            if (!HttpContext.Request.Cookies.ContainsKey(".AspNetCore.Identity.Application"))
            {

                return RedirectToAction("Index", "UserData", new { redirectUrl = redirectUrl, redirectCtrl = redirectCtrl, gameId =gameId });
            }
            else
            {
                
                if (gameId != 0)
                {
                    
                    //While working on Docker container
                    //var url = "http://cart.api/api/cart/post";

                    //While working on local
                    var url = "http://localhost:5004/api/cart/post";

                    var game = GetRequest.GetGameById(gameId);
                    if (cartItems.Where(x => x.ID == gameId).FirstOrDefault() == null)
                    {
                        CartItemDetail gameToAdd = new CartItemDetail
                        {
                            GameName = game.GameName,
                            GamePrice = game.GamePrice,
                            ImageUrl = game.ImageUrl,
                            Publisher = game.Publisher,
                            UserName = userName
                        };
                        await PostRequest.PostApiAsync(url, gameToAdd);
                        cartItems = GetRequest.GetCartItems(userName);
                    }
                    return View(cartItems);
                }
                else
                {

                    return View(cartItems);
                }
            }
                
        }

        [Route("[action]")]
        public async Task<IActionResult> CartRemoveAsync(int id, string userName)
        {
            //While working on Docker container
            //var url = $"http://cart.api/api/cart/delete?id={id}&userName={userName}";

            //While working on local
            var url = $"http://localhost:5004/api/cart/delete?id={id}&userName={userName}";

            
            await PostRequest.DeleteApiAsync(url);
            
            return RedirectToAction("Cart");
        }

    }
}
