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

namespace AspCoreMvc_App.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StudentDetailContext _context;

        public HomeController(ILogger<HomeController> logger, StudentDetailContext context)
        {
            _logger = logger;
            _context = context;
        }
       
        public IActionResult Index()
        {
            //if(HttpContext.Session.GetString("login") == "1")
            //{
                ViewBag.students = _context.StudentDetails.ToList();
                return View();
            //}
            //else
            //{
            //    return RedirectToAction("Index", "UserData");
            //}

        }

        [HttpPost]
        public IActionResult Index(StudentDetail d)
        {
            _context.StudentDetails.Add(d);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult DeleteStudent(int id)
        {
            var toDelete = _context.StudentDetails.Find(id);
            _context.StudentDetails.Remove(toDelete);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult GetStudent(int id)
        {
            var toGet = _context.StudentDetails.Find(id);
            return View(toGet);
        }

        [HttpPost]
        public IActionResult UpdateStudent(StudentDetail student)
        {

            var toUpdate = _context.StudentDetails.Find(student.ID);
            toUpdate.Name = student.Name;
            toUpdate.Surname = student.Surname;
            _context.SaveChanges();
            return RedirectToAction("Index");
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
