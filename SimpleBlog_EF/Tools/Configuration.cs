namespace SimpleBlog_EF.DataAccessLayer.AppUserDBMigrations
{
    using Models.DataBase;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SimpleBlog_EF.DataAccessLayer.AppUsersDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DataAccessLayer\AppUserDBMigrations";
        }

        protected override void Seed(SimpleBlog_EF.DataAccessLayer.AppUsersDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            
            context.Users.AddOrUpdate(
                new User() { Username = "Beren", Email = "berenlyell@hotmail.com", Password_Hash = "123"},
                new User() { Username = "Steve", Email = "steve@test.com", Password_Hash = "123" }
            );

            context.Roles.AddOrUpdate(
                new Role() { Name = "admin"},
                new Role() { Name = "CanEdit" },
                new Role() { Name = "user" }
            );

            context.UserRoles.AddOrUpdate(
                new UserRole() { UserId = 1, RoleId = 1},
                new UserRole() { UserId = 1, RoleId = 2},
                new UserRole() { UserId = 2, RoleId = 3}
            );

        }
    }
}
