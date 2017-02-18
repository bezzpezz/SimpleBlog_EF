using SimpleBlog_EF.Areas.Admin.ViewModels;
using SimpleBlog_EF.DataAccessLayer;
using SimpleBlog_EF.Infrastructure;
using SimpleBlog_EF.Infrastructure.Extensions;
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
            using (db = new MainDBContext())
            {
                return View("Form", new PostsForm
                {
                    IsNew = true,
                    Tags = db.Tags.ToList().Select(tag => new TagCheckBox
                    {
                        Id = tag.TagId,
                        Name = tag.Name,
                        IsChecked = false
                    }).ToList()
                });
            }
        }

        public ActionResult Edit(int id)
        {
            using (db = new MainDBContext())
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
                    Title = post.Title,
                    Tags = db.Tags.ToList().Select(tag => new TagCheckBox
                    {
                        Id = tag.TagId,
                        Name = tag.Name,
                        IsChecked = post.Tags.Contains(tag)
                    }).ToList()
                });
            }
        }

        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false) ]
        public ActionResult Form(PostsForm form)
        {
            form.IsNew = form.PostId == null;

            if (!ModelState.IsValid)
                return View(form);

            using (db = new MainDBContext())
            {
                var selectedTags = ReconsileTags(form.Tags).ToList();

                var post = new Post();
                if (form.IsNew)
                {
                    post.CreatedAt = DateTime.UtcNow;
                    post.User = db.Users
                        .FirstOrDefault(u => u.Username == System.Web.HttpContext.Current.User.Identity.Name);

                    foreach (var tag in selectedTags)
                    {
                        post.Tags.Add(tag);
                    };

                    db.Posts.Add(post);
                }
                else
                {
                    post = db.Posts.Find(form.PostId);

                    if (post == null)
                        return HttpNotFound();

                    post.UpdatedAt = DateTime.UtcNow;

                    foreach (var toAdd in selectedTags.Where(t => !post.Tags.Contains(t)))
                        post.Tags.Add(toAdd);

                    foreach (var toRemove in post.Tags.Where(t => !selectedTags.Contains(t)).ToList())
                        post.Tags.Remove(toRemove);
                }


                post.Title = form.Title;
                post.Slug = form.Slug;
                post.Content = form.Content;
                db.SaveChanges();

                return RedirectToAction("index");
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult NewPost(PostsForm form)
        {
            using (db = new MainDBContext())
            {
                var post = new Post();
                var user = db.Users.Find(1);
                var selectedTags = ReconsileTags(form.Tags).ToList();

                if (!ModelState.IsValid)
                {
                    return View(form);
                };

                post.CreatedAt = DateTime.UtcNow;
                post.User = db.Users.ToList()
                        .FirstOrDefault(u => u.Username == System.Web.HttpContext.Current.User.Identity.Name);

                foreach (var tag in selectedTags)
                {
                    post.Tags.Add(tag);
                };

                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("index");
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Trash(int? id)
        {
            using (db = new MainDBContext())
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

        private IEnumerable<Tag> ReconsileTags(IEnumerable<TagCheckBox> tags)
        {
            foreach (var tag in tags.Where(t => t.IsChecked))
            {
                if (tag.Id != null)
                {
                    yield return db.Tags.Find(tag.Id);
                    continue;
                }

                var existingTag = db.Tags.FirstOrDefault(t => t.Name == tag.Name);
                if (existingTag != null)
                {
                    yield return existingTag;
                    continue;
                }

                var newTag = new Tag
                {
                    Name = tag.Name,
                    Slug = tag.Name.Slugify()
                };

                db.Tags.Add(newTag);

                yield return newTag;
            }
        }
    }
}