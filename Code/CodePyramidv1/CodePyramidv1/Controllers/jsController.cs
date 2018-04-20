using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodePyramidv1.Data;
using Microsoft.AspNetCore.Mvc;

namespace CodePyramidv1.Controllers
{
    public class jsController : Controller
    {   
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult assessmentSubmission(String percentScore, String assessmentName)
        {
            CodePyramidContext context = HttpContext.RequestServices.GetService(typeof(CodePyramidContext)) as CodePyramidContext;
            int quizSuccess = context.InsertAssessmentScore(assessmentName, Request.Cookies["currentUser"], Convert.ToInt16(percentScore));

            if (quizSuccess == 0)
            {
                TempData["Result"] = String.Format("Thanks for submitting! You scored " + percentScore + "%. Nice work!");
                return View();
            }
            else
            {
                TempData["Result"] = "DISASTER! Your score was: " + percentScore + "%, but the error code was: " + quizSuccess;
                return View();
            }
        }

        public IActionResult jslesson1p1()
        {
            return View();
        }

        public IActionResult jslesson1p2()
        {
            return View();
        }

        public IActionResult jslesson1p3()
        {
            CodePyramidContext context = HttpContext.RequestServices.GetService(typeof(CodePyramidContext)) as CodePyramidContext;
            int progressSuccess = context.InsertLessonCompletion("Javascript Lesson 1", Request.Cookies["currentUser"]);
            return View();
        }

        public IActionResult jslesson2p1()
        {
            return View();
        }

        public IActionResult jslesson2p2()
        {
            return View();
        }

        public IActionResult jslesson2p3()
        {
            return View();
        }

        public IActionResult jslesson2p4()
        {
            return View();
        }

        public IActionResult jslesson2p5()
        {
            CodePyramidContext context = HttpContext.RequestServices.GetService(typeof(CodePyramidContext)) as CodePyramidContext;
            int progressSuccess = context.InsertLessonCompletion("Javascript Lesson 2", Request.Cookies["currentUser"]);
            return View();
        }

        public IActionResult jslesson3p1()
        {
            return View();
        }

        public IActionResult jslesson3p2()
        {
            return View();
        }

        public IActionResult jslesson3p3()
        {
            CodePyramidContext context = HttpContext.RequestServices.GetService(typeof(CodePyramidContext)) as CodePyramidContext;
            int progressSuccess = context.InsertLessonCompletion("Javascript Lesson 3", Request.Cookies["currentUser"]);
            return View();
        }

        public IActionResult jslesson4p1()
        {
            return View();
        }

        public IActionResult jslesson4p2()
        {
            return View();
        }

        public IActionResult jslesson4p3()
        {
            CodePyramidContext context = HttpContext.RequestServices.GetService(typeof(CodePyramidContext)) as CodePyramidContext;
            int progressSuccess = context.InsertLessonCompletion("Javascript Lesson 4", Request.Cookies["currentUser"]);
            return View();
        }

        public IActionResult jslesson5p1()
        {
            return View();
        }

        public IActionResult jslesson5p2()
        {
            return View();
        }

        public IActionResult jslesson5p3()
        {
            CodePyramidContext context = HttpContext.RequestServices.GetService(typeof(CodePyramidContext)) as CodePyramidContext;
            int progressSuccess = context.InsertLessonCompletion("Javascript Lesson 5", Request.Cookies["currentUser"]);
            return View();
        }

    }
}