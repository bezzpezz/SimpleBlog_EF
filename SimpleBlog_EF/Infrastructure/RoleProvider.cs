using SimpleBlog_EF.DataAccessLayer;
using SimpleBlog_EF.Models.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlog_EF.Infrastructure
{
    public class RoleProvider : System.Web.Security.RoleProvider
    {
        //Global DB Context
        private AppUsersDBContext db;

        public override string[] GetRolesForUser(string username)
        {
            //var userRoles = new string[] { };

            //using (db = new AppUsersDBContext())
            //{
            //    userRoles = db.Roles.Select(x => x.Name).ToArray();
            //}

            return Auth.User.Roles.Select(role => role.Name).ToArray();

            //return userRoles;
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
