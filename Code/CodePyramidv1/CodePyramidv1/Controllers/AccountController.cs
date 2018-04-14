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

        
        // GET: Account
        public ActionResult Index()
        {
            var model = new LoginViewModel();
            return View(model);
        }
        public ActionResult Login()
        {
            var model = new LoginViewModel();
            //            return RedirectToAction("Index");
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {

            CodePyramidContext context = HttpContext.RequestServices.GetService(typeof(CodePyramidContext)) as CodePyramidContext;
            string name = context.GetLogonInfo(model);

            if (string.IsNullOrEmpty(name))
            {
                //return View("NotAuthenticated");
                return RedirectToAction("LoginFailure");
            }
            //return View("Authenticated", name);
            return RedirectToAction("LoginSuccess", "Account", new { model = model.Username });
        }

        // GET: Account/Details/5
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
//            return View(model);
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