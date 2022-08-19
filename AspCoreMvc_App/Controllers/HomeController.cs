﻿using AspCoreMvc_App.Models;
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
    [Authorize]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StudentDetailContext _context;

        public HomeController(ILogger<HomeController> logger, StudentDetailContext context)
        {
            _logger = logger;
            _context = context;
        }
       
        [Route("[action]")]
        public IActionResult Index()
        {
            var allCategories = GetRequest.GetCategories();
            //ViewBag.category = allCategories;
            return View(allCategories);
        }

        //BURADA KALDIM 18.08
        [Route("[action]/{categoryId}")]
        public IActionResult Index(int categoryId)
        {

            var allCategories = GetRequest.GetCategories();
            var category = allCategories.Where(x => x.GenreID == categoryId).FirstOrDefault();
            ViewBag.category = category;
            return View(allCategories);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
