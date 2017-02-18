using SimpleBlog_EF.DataAccessLayer;
using SimpleBlog_EF.Models.DataBase;
using SimpleBlog_EF.ViewModels;
using SimpleBlog_EF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Text.RegularExpressions;

namespace SimpleBlog_EF.Controllers.Posts
{
    public class PostsController : Controller
    {
        MainDBContext db;
        private const int PostsPerPage = 5;
        // GET: Home
        public ActionResult Index(int page = 1)
        {
            #region create pass when no user passhash exists in db
            // this is used for creating a pass when there is none in the DB to begin with
            //var user = new User() { Username = "FakeUser" };
            //user.SetPassword("test");
            //ViewBag.hash = "Hash for test= " + user.PasswordHash;
            #endregion

            using (db = new MainDBContext())
            {
                var baseQuery = db.Posts.Where(t => t.DeletedAt == null).OrderByDescending(t => t.CreatedAt);
                var totalPostCount = baseQuery.Count();
                //var postIds = baseQuery.Skip((page - 1) * PostsPerPage).Take(PostsPerPage).Select(t => t.PostId).ToList();
                //var posts = baseQuery.Include("User").Include("Tags").Where(t => postIds.Contains(t.PostId)).F(t => t.Tags).Select(t => t.).ToList();


                var posts = baseQuery
                        .Include("User")
                        .Include("Tags")
                    .Skip((page - 1) * PostsPerPage)
                    .Take(PostsPerPage)
                    .ToList();

                return View(new PostsIndex
                {
                    Posts = new PageData<Post>(posts, totalPostCount, page, PostsPerPage)
                });
            }
        }

        public ActionResult Tag(string idAndSlug, int page = 1)
        {
            var parts = SepreateIdAndSlug(idAndSlug);
            if (parts == null)
                return HttpNotFound();

            using (db = new MainDBContext())
            {
                var tag = db.Tags.Find(parts.Item1);
                if (tag == null)
                    return HttpNotFound();

                if (!tag.Slug.Equals(parts.Item2, StringComparison.CurrentCultureIgnoreCase))
                    return RedirectToRoutePermanent("tag", new { id = parts.Item1, slug = tag.Slug });

                var totalPostCount = tag.Posts.Count();

                var postIds = tag.Posts
                    .OrderByDescending(g => g.CreatedAt)
                    .Skip((page - 1) * PostsPerPage)
                    .Take(PostsPerPage)
                    .Where(t => t.DeletedAt == null)
                    .Select(t => t.PostId)
                    .ToArray();

                var posts = db.Posts
                        .Include("User")
                        .Include("Tags")
                    .OrderByDescending(g=>g.CreatedAt)  
                    .Where(t=>postIds.Contains(t.PostId))       
                    .ToList();

                return View(new PostsTag
                {
                    Tag = tag,
                    Posts = new PageData<Post>(posts, totalPostCount, page, PostsPerPage)
                });
            }
        }

        public ActionResult Show(string idAndSlug)
        {
            var parts = SepreateIdAndSlug(idAndSlug);
            if (parts == null)
                return HttpNotFound();

            using (db = new MainDBContext())
            {
                var post = db.Posts.Find(parts.Item1);
                if (post == null || post.IsDeleted)
                    return HttpNotFound();

                if (!post.Slug.Equals(parts.Item2, StringComparison.CurrentCultureIgnoreCase))
                    return RedirectToRoutePermanent("Post", new { id = parts.Item1, slug = post.Slug });


                var newPost = new PostsShow
                {
                    Post = post

                };
                newPost.Post.Tags = post.Tags.ToList();
                return View(newPost);
            }
        }

        private Tuple<int, string> SepreateIdAndSlug(string idAndSlug)
        {
            var matches = Regex.Match(idAndSlug, @"^(\d+)\-(.*)?$");
            if (!matches.Success)          
                return null;

            var id = int.Parse(matches.Result("$1"));
            var slug = matches.Result("$2");
            return Tuple.Create(id, slug);
        }
    }
}