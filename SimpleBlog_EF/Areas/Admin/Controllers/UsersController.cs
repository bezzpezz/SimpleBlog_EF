using SimpleBlog_EF.Areas.Admin.ViewModels;
using SimpleBlog_EF.Infrastructure;
using SimpleBlog_EF.Models.DataBase;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using SimpleBlog_EF.DataAccessLayer;
using System.Collections.Generic;
using System.Web.Security;

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
                var ui = new UsersIndex
                {
                    Users = db.Users.Include("Roles")
                        .ToList()
                };
                return View(ui);
            }
        }

        public ActionResult New()
        {
            using (db = new AppUsersDBContext())
            {
                return View(new UsersNew
                {
                    Roles = db.Roles.Select(role => new RoleCheckBox
                    {
                        Id = role.RoleId,
                        IsChecked = false,
                        Name = role.Name
                    }).ToList()
                });
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult New(UsersNew form)
        {
            using (db = new AppUsersDBContext())
            {
                var user = new User();
                SyncRoles(form.Roles, user.Roles);

                if (db.Users.Any(u => u.Username == form.Username))
                {
                    ModelState.AddModelError("Username", "Username must be unique!");
                }

                if (!ModelState.IsValid)
                {
                    return View(form);
                };

                user.Email = form.Email;
                user.Username = form.Username;
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
                var user = db.Users.Find(id);

                if (user == null)
                    return HttpNotFound();

                var userEdit = new UsersEdit();

                userEdit.Username = user.Username; // user.Roles.Contains(role)
                userEdit.Email = user.Email;

                var userRoles = user.Roles;
                //var dbRoles = db.Roles.ToList();

                // was stuck here for hours - all I had to do was put the db.Roles into a list becuase it was of type dbset wich aparently is not a primitive type 
                userEdit.Roles = db.Roles.ToList().Select(role => new RoleCheckBox
                {
                    Id = role.RoleId,
                    IsChecked = user.Roles.Contains(role),
                    Name = role.Name
                }).ToList();

                return View(userEdit);
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

                SyncRoles(form.Roles, user.Roles);

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

        //Get
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

        // Helper Methods
        public void SyncRoles(IList<RoleCheckBox> checkboxes, ICollection<Role> roles)
        {
            var selectedRoles = new List<Role>();

            //using (db = new AppUsersDBContext())
            //{
            foreach (var role in db.Roles)
            {
                var checkbox = checkboxes.Single(c => c.Id == role.RoleId);

                if (checkbox.IsChecked)
                {
                    selectedRoles.Add(role);
                }
            }

            foreach (var toAdd in selectedRoles.Where(t => !roles.Contains(t)))
                roles.Add(toAdd);

            foreach (var toRemove in roles.Where(t => !selectedRoles.Contains(t)).ToList())
                roles.Remove(toRemove);
            //}
        }
    }
}