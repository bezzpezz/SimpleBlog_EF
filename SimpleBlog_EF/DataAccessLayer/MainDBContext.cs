using SimpleBlog_EF.Models.DataBase;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SimpleBlog_EF.DataAccessLayer
{
    public class MainDBContext : DbContext
    {
        // this is used for code level explicit initialization of the DB connection string here instead of web.config
        public MainDBContext() : base("DefaultConnection")
        {

        }

        //DbSet initialization
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<AddressType> AddresseTypes { get; set; }
        public DbSet<CustomerType> CustomerTypes { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<JobOrder> JobOrders { get; set; }
        public DbSet<JobOrderDetails> JobOrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("MainDB");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Add<CascadeDeleteAttributeConvention>();

            modelBuilder.Entity<Employee>()
             .HasOptional(e => e.Manager)
             .WithMany()
             .HasForeignKey(m => m.ManagerId);

            //modelBuilder.Entity<Store>()
            // .HasOptional(e => e.Employees)
            // .WithMany()
            // .HasForeignKey(f=>f.)
            // .WillCascadeOnDelete(false);

            //Posts
            //modelBuilder.Entity<Post>()
            //.HasMany(u => u.Tags)
            //.WithMany()
            //.Map(m =>
            //{
            //    m.ToTable("Post_Tags");
            //    m.MapLeftKey("PostId");
            //    m.MapRightKey("Id");
            //});

            //Tags
            //modelBuilder.Entity<Tag>()
            //.HasMany(u => u.Posts)
            //.WithMany()
            //.Map(m =>
            //{
            //    m.ToTable("Post_Tags");
            //    m.MapLeftKey("TagId");
            //    m.MapRightKey("PostId");
            //});

        }
    }
}
