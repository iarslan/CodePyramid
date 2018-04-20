using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodePyramidv1.Data;
using Microsoft.AspNetCore.Mvc;

namespace CodePyramidv1.Controllers
{
    public class htmlcssController : Controller
    {

        /*  QUICK CHEAT SHEET FOR THE ID'S ON LESSONS/QUIZZES
         *   
         *   COURSES
         *   -------
         *   (courseId)     (courseName)
         *   1              "HTML/CSS"
         *   2              "Javascript"
         *   
         *  SECTIONS
         *  --------
         *  (sectionId)     (sectionName)               (courseId)
         *  1               "HTML/CSS Lesson 1"         1
         *  2               "HTML/CSS Lesson 2"         1
         *  3               "HTML/CSS Lesson 3"         1
         *  4               "HTML/CSS Lesson 4"         1
         *  5               "HTML/CSS Lesson 5"         1
         *  6               "Javascript Lesson 1"       2
         *  7               "Javascript Lesson 2"       2
         *  8               "Javascript Lesson 3"       2
         *  9               "Javascript Lesson 4"       2
         *  10              "Javascript Lesson 5"       2
         *  11              "HTML/CSS Quiz 1"           1 
         *  12              "HTML/CSS Quiz 2"           1 
         *  13              "HTML/CSS Quiz 3"           1 
         *  14              "HTML/CSS Quiz 4"           1 
         *  15              "HTML/CSS Quiz 5"           1 
         *  16              "Javascript Quiz 1"         2
         *  17              "Javascript Quiz 2"         2
         *  18              "Javascript Quiz 3"         2
         *  19              "Javascript Quiz 4"         2
         *  20              "Javascript Quiz 5"         2
         * 
         */

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

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult htmlcsslesson1p1()
        {
            return View();
        }

        public IActionResult htmlcsslesson1p2()
        {
            return View();
        }

        public IActionResult htmlcsslesson1p3()
        {
            return View();
        }

        public IActionResult htmlcsslesson1p4() //QUIZ
        {
            CodePyramidContext context = HttpContext.RequestServices.GetService(typeof(CodePyramidContext)) as CodePyramidContext;
            int progressSuccess = context.InsertLessonCompletion("HTML/CSS Lesson 1", Request.Cookies["currentUser"]);
            return View();
        }

        public IActionResult htmlcsslesson2p1()
        {
            return View();
        }

        public IActionResult htmlcsslesson2p2()
        {
            return View();
        }

        public IActionResult htmlcsslesson2p3()
        {
            return View();
        }

        public IActionResult htmlcsslesson2p4()
        {
            return View();
        }

        public IActionResult htmlcsslesson2p5()     //QUIZ
        {
            CodePyramidContext context = HttpContext.RequestServices.GetService(typeof(CodePyramidContext)) as CodePyramidContext;
            int progressSuccess = context.InsertLessonCompletion("HTML/CSS Lesson 2", Request.Cookies["currentUser"]);
            return View();
        }

        public IActionResult htmlcsslesson3p1()
        {
            return View();
        }

        public IActionResult htmlcsslesson3p2()
        {
            return View();
        }

        public IActionResult htmlcsslesson3p3()
        {
            return View();
        }

        public IActionResult htmlcsslesson3p4() //QUIZ
        {
            CodePyramidContext context = HttpContext.RequestServices.GetService(typeof(CodePyramidContext)) as CodePyramidContext;
            int progressSuccess = context.InsertLessonCompletion("HTML/CSS Lesson 3", Request.Cookies["currentUser"]);
            return View();
        }

        public IActionResult htmlcsslesson4p1()
        {
            return View();
        }

        public IActionResult htmlcsslesson4p2()
        {
            return View();
        }

        public IActionResult htmlcsslesson4p3()
        {
            CodePyramidContext context = HttpContext.RequestServices.GetService(typeof(CodePyramidContext)) as CodePyramidContext;
            int progressSuccess = context.InsertLessonCompletion("HTML/CSS Lesson 4", Request.Cookies["currentUser"]);
            return View();
        }

        public IActionResult htmlcsslesson5p1()
        {
            return View();
        }

        public IActionResult htmlcsslesson5p2()
        {
            return View();
        }

        public IActionResult htmlcsslesson5p3()
        {
            CodePyramidContext context = HttpContext.RequestServices.GetService(typeof(CodePyramidContext)) as CodePyramidContext;
            int progressSuccess = context.InsertLessonCompletion("HTML/CSS Lesson 5", Request.Cookies["currentUser"]);
            return View();
        }


    }
}