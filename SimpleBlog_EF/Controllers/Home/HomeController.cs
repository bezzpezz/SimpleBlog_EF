using SimpleBlog_EF.Models.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog_EF.Controllers.Home
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var user = new User() { Username = "FakeUser" };
            user.SetPassword("test");

            ViewBag.hash = "Hash for test= " + user.PasswordHash;
            return View();
        }
    }
}