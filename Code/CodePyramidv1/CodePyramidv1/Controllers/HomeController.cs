using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CodePyramidv1.Models;
using CodePyramidv1.Data;

namespace CodePyramidv1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "C o d e P y r a m i d";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Don't hesitate to contact the folks at CodePyramid with any questions.";

            return View();
        }
        public IActionResult Courses()
        {
            ViewData["Message"] = "Courses at CodePyramid:";
            CodePyramidContext context = HttpContext.RequestServices.GetService(typeof(CodePyramidContext)) as CodePyramidContext;

            var list = context.GetAllCourses();


            return View(list);
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
