using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspCoreMvc_App.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using App.Library;

namespace AspCoreMvc_App.Controllers
{
    public class UserDataController : Controller
    {

        private readonly StudentDetailContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        //StudentDetailContext _context = new StudentDetailContext();
        public UserDataController(StudentDetailContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [AllowAnonymous]
        public IActionResult Index()
        {
            if (!HttpContext.Request.Cookies.ContainsKey(".AspNetCore.Identity.Application"))
            {
                ViewBag.redirectUrl = HttpContext.Request.Query["redirectUrl"].ToString();
                ViewBag.redirectCtrl = HttpContext.Request.Query["redirectCtrl"].ToString();
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }            
        }

        [AllowAnonymous]
        [HttpPost]
        //string uname, string pwd
        public async Task<IActionResult> Verify(SignedUser p)
        {

            string redirectUrl = HttpContext.Request.Query["redirectUrl"].ToString();
            string redirectCtrl = HttpContext.Request.Query["redirectCtrl"].ToString();
            string gameId = HttpContext.Request.Query["gameId"].ToString();

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(p.Username, p.Password, false, true);
                if (result.Succeeded)
                {
                    if (redirectUrl != "" && redirectCtrl != "")
                    {
                        return RedirectToAction(redirectUrl, redirectCtrl, new { gameId = gameId });
                    }
                    else
                    {
                        //Just redirect to our index after logging in. 
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            //var verify_user = _context.UserDatas.Find(uname);
            //if (verify_user != null && verify_user.Password == pwd)
            //{
            //    var claims = new List<Claim>
            //        {
            //        new Claim(ClaimTypes.Name, uname),
            //        new Claim(ClaimTypes.NameIdentifier, uname)
            //        };

            //    var userIdentity = new ClaimsIdentity(claims, "login");

            //    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
            //    await HttpContext.SignInAsync("Cookies", principal);
            //    HttpContext.User = principal;

            //    Console.WriteLine(User.Identity.Name);

            //}
            return View("Index");
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("[controller]/[action]")]
        //string name, string pw, string cnf_pw
        public async Task<IActionResult> CreateAsync(UserData p)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    UserName = p.Username
                };
                var result = await _userManager.CreateAsync(user, p.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View("Index");
            //if (pw == cnf_pw) {
            //    _context.UserDatas.Add(new UserData { Username = name, Password = pw });
            //    _context.SaveChanges();
            //    return RedirectToAction("Index", "Home");
            //}
            //else
            //{
            //    return View("Index");
            //}

        }

        public async Task<IActionResult> Log_Out()
        {

            await _signInManager.SignOutAsync();
            //await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Index", "Home");
        }

    }
}
