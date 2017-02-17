using SimpleBlog_EF.Models.DataBase;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SimpleBlog_EF.DataAccessLayer
{
    public class AppUsersDBContext : DbContext
    {

        public AppUsersDBContext() : base("DefaultConnection")
        {
            // Drop db if exists
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AppUsersDBContext>());
        }

        //DbSet initialization
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        //public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("AppUsers");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany()
                .Map(m =>
                {
                    m.ToTable("UserRoles");
                    m.MapLeftKey("UserId");
                    m.MapRightKey("RoleId");
                });

            //modelBuilder.Entity<Role>()
            //    .HasMany(u => u.Users)
            //    .WithMany()
            //    .Map(m =>
            //    {
            //        m.ToTable("UserRoles");
            //        m.MapLeftKey("RoleId");
            //        m.MapRightKey("UserId");
            //    });
        }
    }
}
