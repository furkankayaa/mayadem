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

namespace AspCoreMvc_App.Controllers
{
    public class UserDataController : Controller
    {

        private readonly StudentDetailContext _context;

        public UserDataController(StudentDetailContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {

            if(!HttpContext.Request.Cookies.ContainsKey(".AspNetCore.Cookies"))
                return View();
            else
            {
                return RedirectToAction("Index", "Home");
            }
            //}
        }

        [HttpPost]
        public async Task<IActionResult> Verify(string uname, string pwd)
        {
            var verify_user = _context.UserDatas.Find(uname);
            if (verify_user != null && verify_user.Password == pwd)
            {
                var claims = new List<Claim>
                    {
                    new Claim(ClaimTypes.Name, uname)
                    };

                var userIdentity = new ClaimsIdentity(claims, "login");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync("Cookies", principal);

                //Just redirect to our index after logging in. 
                return RedirectToAction("Index", "Home");
            }
            return View("Index");
        }

        [HttpPost]
        public IActionResult Create(string name, string pw, string cnf_pw)
        {
            if (pw == cnf_pw) {
                _context.UserDatas.Add(new UserData { Username = name, Password = pw });
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Index");
            }
            
        }

        public async Task<IActionResult> Log_Out()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Index", "UserData");
        }

    }
}
