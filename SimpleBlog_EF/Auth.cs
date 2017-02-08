﻿using SimpleBlog_EF.Models.DataBase;
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
        private static AppUsersDBContext db;

        public static User User
        {
            get
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                    return null;

                var user = HttpContext.Current.Items[UserKey] as User;
                if(user == null)
                {
                    using (db = new AppUsersDBContext())
                    {
                        user = db.Users.FirstOrDefault(u => u.Username == HttpContext.Current.User.Identity.Name);

                        if (user == null)
                            return null;

                        HttpContext.Current.Items[UserKey] = user;
                    }
                }

                return user;
            }
        }

    }
}
