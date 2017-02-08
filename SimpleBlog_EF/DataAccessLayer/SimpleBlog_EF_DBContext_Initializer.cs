using SimpleBlog_EF.Models.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlog_EF.DataAccessLayer
{
    public class AppUsersDBContext_Initializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<AppUsersDBContext>
    {
        protected override void Seed(AppUsersDBContext context)
        {
            var users = new List<User>
            {
                new User {Username="Beren",Email="berenlyell@hotmail.com", PasswordHash="239487sfh" },
                new User {Username="Steve",Email="steve@hotmail.com", PasswordHash="239487sfh" },
            };

            var roles = new List<Role>
            {
                new Role {Name="admin"},
                new Role {Name="user"},
            };

            var userRoles = new List<UserRole>
            {
                new UserRole {RoleId=1, UserId=1 },
                new UserRole {RoleId=2, UserId=1 },
                new UserRole {RoleId=2, UserId=1 }
            };
        }

    }
}
