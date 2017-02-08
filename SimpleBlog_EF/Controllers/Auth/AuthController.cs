using SimpleBlog_EF.ViewModels;
using System.Web.Mvc;
using System.Web.Security;
using SimpleBlog_EF.DataAccessLayer;
using System.Linq;

namespace SimpleBlog_EF.Controllers.Auth
{
    public class AuthController : Controller
    {
        AppUsersDBContext db;

        // GET: Auth
        public ActionResult Login()
        {
            return View(new AuthLogin { });
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("home");
        }

        [HttpPost]
        public ActionResult Login(AuthLogin form, string returnUrl)
        {

            if (!ModelState.IsValid)
                return View(form);

            using (db = new AppUsersDBContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Username == form.Username);

                //avoif timing attacks here
                if (user == null)
                    Models.DataBase.User.FakeHash();

                if (user == null || !user.CheckPassword(form.Password))
                    ModelState.AddModelError("Username", "Username or Password Incorrect");

                if (!ModelState.IsValid)
                    return View(form);

                FormsAuthentication.SetAuthCookie(user.Username, true);

                if (!string.IsNullOrWhiteSpace(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToRoute("home");
            }

        }
    }
}