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
       
        [HttpGet]
        [Route("")]
        [Route("[action]")]
        public IActionResult Index()
        {
            var allCategories = GetRequest.GetCategories();
            ViewBag.category = allCategories;
            return View(allCategories);
        }

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
        public IActionResult Cart()
        {
            GetRequest.GetCategories();
            return View();
        }

    }
}
