using SimpleBlog_EF.Models.DataBase;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SimpleBlog_EF.DataAccessLayer
{
    public class AppUsersDBContext : DbContext
    {

        public AppUsersDBContext() : base("DefaultConnection")
        {

        }

        //DbSet initialization
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("AppUsers");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
