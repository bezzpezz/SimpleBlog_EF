using SimpleBlog_EF.Areas.Admin.ViewModels;
using SimpleBlog_EF.DataAccessLayer;
using SimpleBlog_EF.Infrastructure;
using SimpleBlog_EF.Models.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog_EF.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [SelectedTab("home")]
    public class HomeController : Controller
    {
        private MainDBContext db;
        private const int postsPerPage = 3;

        // GET: Admin/Home
        public ActionResult Index(int page = 1)
        {
            var totalPostsCount = 0;

            using (db = new MainDBContext())
            {
                totalPostsCount = db.Posts.Count();
                var currentPostPage = db.Posts
                        .Include("User")
                        .Include("Tags")
                    .OrderByDescending(c => c.CreatedAt)
                    .Skip((page - 1) * postsPerPage)
                    .Take(postsPerPage)
                    .ToList();

                return View(new PostsIndex
                {
                    Posts = new PageData<Post>(currentPostPage, totalPostsCount, page, postsPerPage)

                });
            }
        }
    }
}