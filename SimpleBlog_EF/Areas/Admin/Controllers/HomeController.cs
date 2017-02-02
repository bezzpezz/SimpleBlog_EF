using SimpleBlog_EF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog_EF.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class HomeController : Controller
    {
        // GET: Admin/Home
        [SelectedTab("home")]
        public ActionResult Index()
        {
            return View();
        }
    }
}