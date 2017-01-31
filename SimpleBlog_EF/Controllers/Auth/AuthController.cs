using SimpleBlog_EF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog_EF.Controllers.Auth
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AuthLogin form)
        {
            if (!ModelState.IsValid)
                return View(form);

            if (form.Username != "beren" || form.Password != "pass")
            {
                ModelState.AddModelError("Username", "Username or Password isn't Valid");
                return View(form);
            }

            return Content("The Form is Valid");
        }
    }
}