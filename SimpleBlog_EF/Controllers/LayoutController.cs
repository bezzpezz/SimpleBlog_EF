using SimpleBlog_EF;
using SimpleBlog_EF.DataAccessLayer;
using SimpleBlog_EF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog_EF.Controllers
{
    public class LayoutController : Controller
    {
        private MainDBContext db;

        [ChildActionOnly]
        public ActionResult Sidebar()
        {
            using (db = new MainDBContext())
            {
                var layoutSideBar = new LayoutSideBar()
                {
                    IsLoggedIn = SimpleBlog_EF.Auth.User != null,
                    UserName = SimpleBlog_EF.Auth.User != null ? SimpleBlog_EF.Auth.User.Username : "",
                    IsAdmin = User.IsInRole("admin"),
                    Tags = db.Tags.ToList().Select(tag => new
                    {
                        tag.TagId,
                        tag.Name,
                        tag.Slug,
                        PostCount = tag.Posts.Count
                    }).Where(t => t.PostCount > 0)
                    .OrderByDescending(p => p.PostCount)
                    .Select(tag => new SideBarTag(tag.TagId, tag.Name, tag.Slug, tag.PostCount)).ToList()
                };

                return View(layoutSideBar);
            }
        }
    }
}