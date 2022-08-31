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
                ViewBag.ReturnUrl = HttpContext.Request.Query["ReturnUrl"].ToString();

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
            string returnUrl = HttpContext.Request.Query["ReturnUrl"].ToString();

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(p.Username, p.Password, false, true);
                if (result.Succeeded)
                {
                    //if (redirectUrl != "" && redirectCtrl != "")
                    if(returnUrl != "")
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        //Just redirect to our index after logging in. 
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
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

        }

        public async Task<IActionResult> Log_Out()
        {

            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
