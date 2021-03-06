﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CodePyramidv1.Models;
using CodePyramidv1.Data;
using CodePyramidv1.ViewModels;
using System.Net.Mail;
    using Microsoft.AspNetCore.Http;

namespace CodePyramidv1.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult CookiesTestCreate()
        {
            //Create
            CookieOptions option = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1)
            };
            Response.Cookies.Append("currentUser", "dummy", option);

            return View();
        }

        public ActionResult CookiesTestRetrieve()
        {
            //Retrieve
            String q = Request.Cookies["currentUser"];
            if( q != null ) 
            {
                ViewData["Message"] = "Found it: " + q;
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult CookiesTestDestroy()
        {
            //Destroy
            Response.Cookies.Delete("currentUser");
            return View();
        }

        public IActionResult Index() {

            ViewBag.Message = "FFF";
            String uname = Request.Cookies["currentUser"];
            if (uname == null)
            {
                ViewBag.Greeting = "Learn How To Code For Free";
            }
            else
            {
                ViewBag.Greeting = "Welcome, " + uname + "!";
            }
            return View(ViewBag);
        }

        public IActionResult About()
        {
            return View();
        }

        public ActionResult MyAccount()
        {
            String uname = Request.Cookies["currentUser"];

            if( uname == null )
            {
                return RedirectToAction("Login", "Account");
            }

            CodePyramidContext context = HttpContext.RequestServices.GetService(typeof(CodePyramidContext)) as CodePyramidContext;
            ProgressAndAssessmentViewModel paavm = context.FetchProgressResults(uname);
            paavm.CompletedLessons.Sort();
            return View(paavm);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Don't hesitate to contact the folks at CodePyramid with any questions.";
            try
            {
                ViewBag.Message = TempData["result"].ToString();
            }
            catch(System.NullReferenceException)
            {
                ViewBag.Message = "";
            }
                    return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactViewModel cvm)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential("codepyramidhelp@gmail.com", "RiGL1kk87!Wkdnrwox104k21nf@95!48!"),
                    //UseDefaultCredentials = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    
                    EnableSsl = true
                };
            MailAddress from = new MailAddress("codepyramidhelp@gmail.com", cvm.Name);
            MailAddress to = new MailAddress("codepyramidhelp@gmail.com", "CodePyramid");
            MailMessage myMail = new System.Net.Mail.MailMessage(from, to);
            myMail.Subject = cvm.Subject;
            myMail.Body = cvm.Body;
            myMail.BodyEncoding = System.Text.Encoding.UTF8;
            myMail.IsBodyHtml = true;
            smtpClient.Send(myMail);

            TempData["result"] = "Successfuly sent. Thanks!";
            return RedirectToAction("Contact");
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
       
        /*
         *  THIS IS THE CODE WE CAN USE TO INTERACT WITH COOKIES
         * 
         *  CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append("key", "hello", option);

            Response.Cookies.Delete("key");

            var f = Request.Cookies["key"];

         * 
         * 
         */

    /*  
              
    //Create
    CookieOptions option = new CookieOptions
    {
        Expires = DateTime.Now.AddDays(1)
    };
    Response.Cookies.Append("key", "hello", option);

    //Retrieve
    var q = Request.Cookies["key"];

    //Destroy
    Response.Cookies.Delete("key");
 
    */

    }
}
