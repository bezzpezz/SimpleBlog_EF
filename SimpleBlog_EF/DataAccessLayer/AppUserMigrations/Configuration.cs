namespace SimpleBlog_EF.DataAccessLayer.AppUserMigrations
{
    using Models.DataBase;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SimpleBlog_EF.DataAccessLayer.AppUsersDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DataAccessLayer\AppUserMigrations";
        }

        protected override void Seed(SimpleBlog_EF.DataAccessLayer.AppUsersDBContext context)
        {
            //  This method will be called after migrating to the latest version.
            //only run seed if there is data is no data already in the table - this stops the data doubling up each seed
            if (!context.Users.Any())
            {
                context.Users.AddOrUpdate(
                    new User() { Username = "Beren", Email = "berenlyell@hotmail.com", PasswordHash = "$2a$13$LEyYCZqiAdkLdnz.nrDFyuoV/k.jsj.g5T0XPjCG.Ifu8vAD4djgy" },
                    new User() { Username = "Steve", Email = "steve@test.com", PasswordHash = "$2a$13$LEyYCZqiAdkLdnz.nrDFyuoV/k.jsj.g5T0XPjCG.Ifu8vAD4djgy" }
                );

                context.Roles.AddOrUpdate(
                    new Role() { Name = "user" },
                    new Role() { Name = "editor" },
                    new Role() { Name = "admin" }
                );         
            }
        }
    }
}
