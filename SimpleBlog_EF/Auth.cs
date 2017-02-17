using SimpleBlog_EF.Models.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleBlog_EF.DataAccessLayer;

namespace SimpleBlog_EF
{
    public class Auth
    {
        private const string UserKey = "SimpleBlog.Auth.UserKey";
        private static MainDBContext db;

        public static User User
        {
            get
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                    return null;

                var user = HttpContext.Current.Items[UserKey] as User;
                if(user == null)
                {
                    db = new MainDBContext();
                                    
                    user = db.Users.Include("Roles").ToList()
                        .FirstOrDefault(u => u.Username == HttpContext.Current.User.Identity.Name);

                    if (user == null)
                        return null;

                    HttpContext.Current.Items[UserKey] = user;

                }

                return user;
            }
        }

    }
}
