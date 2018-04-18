using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodePyramidv1.Data;
using CodePyramidv1.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePyramidv1.Controllers
{
    public class AccountController : Controller
    {

        public ActionResult Index()
        {
            var model = new LoginViewModel();
            return View(model);
        }

        public ActionResult Logout()
        {
            /*            //Destroy
                        CookieOptions option = new CookieOptions
                        {
                            Expires = DateTime.Now.AddDays(-1)
                        };
                        Response.Cookies.Append("currentUser", "if_you_see_this_big_error_happened", option);

            //            Response.Cookies.Delete("currentUser");

            */
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }

            TempData["Message"] = "Logged Out! If you'd like, you can log in again below.";
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
            }
            else
            {
                ViewBag.Message = "";
            }
            var model = new LoginViewModel();
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            CodePyramidContext context = HttpContext.RequestServices.GetService(typeof(CodePyramidContext)) as CodePyramidContext;
            string name = context.GetLogonInfo(model);

            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("LoginFailure");
            }

            //Create a username cookie with a 1 day duration
            CookieOptions option = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1)
            };
            Response.Cookies.Append("currentUser", model.Username, option);

            return RedirectToAction("LoginSuccess", "Account", new { model = model.Username });
        }

        public ActionResult Register()
        {
            var model = new RegisterViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            CodePyramidContext context = HttpContext.RequestServices.GetService(typeof(CodePyramidContext)) as CodePyramidContext;
            int rowsAffected = context.RegisterUser(model);
            if(rowsAffected == 0)
            {
                return RedirectToAction("RegistrationFailure");
            }
            return RedirectToAction("RegistrationSuccess");
        }

        public ActionResult LoginSuccess()
        {
            return View();
        }

        public ActionResult LoginFailure()
        {
            return View();
        }

        public ActionResult RegistrationSuccess()
        {
            return View();
        }

        public ActionResult RegistrationFailure()
        {
            return View();
        }

        public ActionResult Authenticated(string model)
        {
            return View("Authenticated", model);
        }
        
        public ActionResult NotAuthenticated()
        {
            return View();
        }











        // GET: Account/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Account/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Account/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Account/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Account/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Account/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}