using SimpleBlog_EF.Areas.Admin.ViewModels;
using SimpleBlog_EF.Infrastructure;
using SimpleBlog_EF.Models.DataBase;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using SimpleBlog_EF.DataAccessLayer;

namespace SimpleBlog_EF.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [SelectedTab("users")]
    public class UsersController : Controller
    {
        //Global DB Context
        private AppUsersDBContext db;
        // GET: Admin/Users
        public ActionResult Index()
        {
            using (var db = new AppUsersDBContext())
            {
                return View(new UsersIndex
                {
                    Users = db.Users.ToList()
                });
            }
        }

        public ActionResult New()
        {
            return View(new UsersNew { });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult New(UsersNew form)
        {
            using (var db = new AppUsersDBContext())
            {
                if (db.Users.Any(u => u.Username == form.Username))
                {
                    ModelState.AddModelError("Username", "Username must be unique!");
                }

                if (!ModelState.IsValid)
                {
                    return View(form);
                };

                var user = new User
                {
                    Email = form.Email,
                    Username = form.Username
                };

                user.SetPassword(form.Password);
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("index");
            }
        }

        public ActionResult Edit(int id)
        {
            using (db = new AppUsersDBContext())
            {
                //var user = db.Users.Where(x => x.Id == id).FirstOrDefault();
                var user = db.Users.Find(id);

                if (user == null)
                    return HttpNotFound();

                return View(new UsersEdit
                {
                    Username = user.Username,
                    Email = user.Email
                });
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UsersEdit form)
        {

            using (db = new AppUsersDBContext())
            {
                var user = db.Users.Find(id);
                if (user == null)
                    return HttpNotFound();

                if (db.Users.Any(u => u.Username == form.Username && u.Id != id))
                    ModelState.AddModelError("Username", "Username must be unique!");

                if (!ModelState.IsValid)
                    return View(form);

                user.Username = form.Username;
                user.Email = form.Email;
                db.SaveChanges();

                return RedirectToAction("index");
            }
        }


        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ResetPassword(int id)
        {
            using (db = new AppUsersDBContext())
            {
                var user = db.Users.Find(id);
                if (user == null)
                    return HttpNotFound();

                return View(new UsersResetPassword
                {
                    Username = user.Username
                });
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ResetPassword(int id, UsersResetPassword form)
        {
            using (db = new AppUsersDBContext())
            {
                var user = db.Users.Find(id);
                if (user == null)
                    return HttpNotFound();

                form.Username = user.Username;

                if (!ModelState.IsValid)
                    return View(form);

                user.SetPassword(form.Password);
                db.SaveChanges();

                return RedirectToAction("index");
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            using (db = new AppUsersDBContext())
            {
                var user = db.Users.Find(id);
                if (user == null)
                    return HttpNotFound();

                db.Users.Remove(user);
                db.SaveChanges();

                return RedirectToAction("index");
            }
        }
    }
}