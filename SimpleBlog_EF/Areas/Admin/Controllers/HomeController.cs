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

        public ActionResult New()
        {
            return View("Form", new PostsForm {
                IsNew = true
            });
        }

        public ActionResult Edit(int id)
        {
            using(db = new MainDBContext())
            {
                var post = db.Posts.Find(id);

                if (post == null)
                    return HttpNotFound();

                return View("Form", new PostsForm
                {
                    IsNew = false,
                    PostId = id,
                    Content = post.Content,
                    Slug = post.Slug,
                    Title = post.Title
                });
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Form(PostsForm form)
        {
            form.IsNew = form.PostId == null;

            if (!ModelState.IsValid)
                return View(form);

            Post post;
            if (form.IsNew)
            {
                post = new Post
                {
                    CreatedAt = DateTime.UtcNow,
                    User = Auth.User
                };
            }
            else
            {
                using(db= new MainDBContext())
                {
                    post = db.Posts.Find(form.PostId);             

                    if (post == null)
                        return HttpNotFound();

                    post.UpdatedAt = DateTime.UtcNow;
                    post.Title = form.Title;
                    post.Slug = form.Slug;
                    post.Content = form.Content;

                    db.SaveChanges();
                }
            }

            return RedirectToAction("index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Trash(int? id)
        {
            using(db = new MainDBContext())
            {
                var post = db.Posts.Find(id);
                if (post == null)
                    return HttpNotFound();

                post.DeletedAt = DateTime.UtcNow;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            using (db = new MainDBContext())
            {
                var post = db.Posts.Find(id);
                if (post == null)
                    return HttpNotFound();

                db.Posts.Remove(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Restore(int? id)
        {
            using (db = new MainDBContext())
            {
                var post = db.Posts.Find(id);
                if (post == null)
                    return HttpNotFound();

                post.DeletedAt = null;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}