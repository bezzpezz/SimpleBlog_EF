using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleBlog_EF.Models.DataBase
{
    public class DBModels
    {
    }

    public class Post
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int PostId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual string Title { get; set; }
        public virtual string Slug { get; set; }
        public virtual string Content { get; set; }

        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime? UpdatedAt { get; set; }
        public virtual DateTime? DeletedAt { get; set; }

        public virtual bool IsDeleted { get { return DeletedAt != null; } }

        public Post()
        {
            Tags = new List<Tag>();
        }

        //[ForeignKey("Tags")]
        public virtual IList<Tag> Tags { get; set; }
        public virtual User User { get; set; }
    }

    public class Tag
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public virtual int TagId { get; set; }
        public virtual string Slug { get; set; }
        public virtual string Name { get; set; }

        public Tag()
        {
            Posts = new List<Post>();
        }

        //[ForeignKey("Posts")]
        public virtual IList<Post> Posts { get; set; }
    }

    public class Post_Tags
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("PostId")]
        public int PostId { get; set; }

        [ForeignKey("TagId")]
        public int TagId { get; set; }

        public virtual Post Post { get; set; }
        public virtual Tag Tag { get; set; }
    }

    #region AppUserDB Schema

    public class User
    {
        private const int WorkFactor = 13;

        // This stops Timeattacks on this server - stops people from checking how long i takes for the server to return a response (always longer if they are and then they know you are registered) if  user is in the database or not
        public static void FakeHash()
        {
            BCrypt.Net.BCrypt.HashPassword("", WorkFactor);
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }
        
        [Required]
        public string Email { get; set; }
        public string Avatar { get; set; }

        [Required, DataType(DataType.Password)]
        public string PasswordHash { get; set; }
        
        public virtual IList<Role> Roles { get; set; }

        public User()
        {
            // Instatiate the Roles list here to avoid null references
            Roles = new List<Role>();
        }

        public virtual void SetPassword(string Password)
        {
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(Password, WorkFactor);
        }

        public virtual bool CheckPassword(string Password)
        {
            return BCrypt.Net.BCrypt.Verify(Password, PasswordHash);
        }
    }

    public class Role
    {
        //public Role()
        //{
            // Instatiate the Roles list here to avoid null references
            //Users = new List<User>();
        //}

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }
        public string Name { get; set; }

        //public virtual IList<User> Users { get; set; }
    }

    //public class UserRole
    //{
    //    [Key]
    //    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    //    public int Id { get; set; }

    //    [ForeignKey("User")]
    //    public int UserId { get; set; }

    //    [ForeignKey("Role")]
    //    public int RoleId { get; set; }

    //    public virtual User User { get; set; }
    //    public virtual Role Role { get; set; }
    //}

    #endregion

    #region MainDB Schema

    #region CustomerData

    public class Customer
        {
            [Key]
            [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public bool Active { get; set; }

            [ForeignKey("CustomerType")]
            public int CustomerTypeId { get; set; }

            public DateTime? CreateDate { get; set; }
            public DateTime? LastUpdate { get; set; }

            [MaxLength(11)]
            public string HomePhone { get; set; }

            [MaxLength(11)]
            public string WorkPhone { get; set; }

            [MaxLength(11)]
            public string MobilePhone { get; set; }

            public virtual CustomerType CustomerType { get; set; }
    }

    public class CustomerType
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public string Desc { get; set; }
        }

        public class Address
        {
            [Key]
            [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public string AddressLine1 { get; set; }
            public string AddressLine2 { get; set; }

            //[ForeignKey("City")]
            //public int CityId { get; set; }

            [MaxLength(10)]
            public string PostCode { get; set; }

            [ForeignKey("AddressType")]
            public int AddressTypeId { get; set; }

            public virtual City City { get; set; }
            public virtual AddressType AddressType { get; set; }
        }   

        public class City
        {
            [Key]
            [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public string CityName { get; set; }

            public virtual Country Country { get; set; }
        }

        // Lookup Table
        public class Country
        {
            [Key]
            [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime? LastUpdate { get; set; }
        }

        // Lookup Table
        public class AddressType
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public string Desc { get; set; }
        }

        public class CustomerAddress
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            [ForeignKey("Address")]
            public int AddressId { get; set; }

            [ForeignKey("Customer")]
            public int CutomerId { get; set; }

            public virtual Address Address { get; set; }
            public virtual Customer Customer { get; set; }

        }

    #endregion

    #region CompanyData

    public class Employee
        {
            [Key]
            [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            public int? ManagerId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }

            [ForeignKey("Address")]
            public int AddressId { get; set; }

            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }
            public DateTime? DateOfBirth { get; set; }
            public bool Active { get; set; }
            public string Avatar { get; set; }
            public DateTime? DateOfCommenceWork { get; set; }
            public DateTime? DateOfLeaveWork { get; set; }
            public DateTime? DateCreated { get; set; }
            public DateTime? LastUpdate { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }

            public virtual Address Address { get; set; }
            public virtual Employee Manager { get; set; }
            public virtual Store Store { get; set; }
        }

        public class Store
        {
            [Key]
            [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            //[ForeignKey("Employee")] 
            //public int EmployeeId { get; set; }

            [ForeignKey("Address")]
            public int AddressId { get; set; }
            public DateTime LastUpdate { get; set; }

            public virtual IList<Employee> Employees { get; set; }
            public virtual Address Address { get; set; }
        }

        public class Payment
        {
            [Key]
            public int Id { get; set; }

            [ForeignKey("Customer")]
            public int CustomerId { get; set; }

            [ForeignKey("Employee")]
            public int EmployeeId { get; set; }

            [ForeignKey("PaymentType")]
            public int PaymentTypeId { get; set; }

            public virtual Customer Customer { get; set; }
            public virtual Employee Employee { get; set; }
            public virtual PaymentType PaymentType { get; set; }
        }

        // Lookup Table
        public class PaymentType
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            [Required]
            public string Name { get; set; }
            public string Description { get; set; }
        }

        public class JobOrder
        {
            [Key]
            [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            [Required]
            public string JobOrderCode { get; set; }

            public string Color { get; set; }
            public string GlossLevel { get; set; }
            public string NoOfParts { get; set; }

            [DisplayFormat(DataFormatString = "{0:ddd, d MMM yyyy}", ApplyFormatInEditMode = true)]
            public DateTime DateRecieved { get; set; }

            [DisplayFormat(DataFormatString = "{0:ddd, d MMM yyyy}", ApplyFormatInEditMode = true)]
            public DateTime DateDue { get; set; }
            public string DeliveryInfo { get; set; }

            [ForeignKey("Customer")]
            public int CustomerId { get; set; }

            public int CustomerPaymentMethodId { get; set; }

            public int StatusId { get; set; }

            public DateTime? DateLoaded { get; set; }
            public DateTime? DateUnderCoated { get; set; }
            public DateTime? DateTopCoated { get; set; }
            public DateTime? DateOfQC { get; set; }
            public DateTime? DateOfLastRespray { get; set; }
            public int? ResprayCount { get; set; }
            public DateTime? DateOfLastUpdate { get; set; }
            public DateTime? DateQCReady { get; set; }
            public DateTime? DateOrderPaid { get; set; }
            public DateTime? DateOrderComplete { get; set; }
            public DateTime? DateOrderDelivered { get; set; }
            public bool IsDelevered { get; set; }
    
            public string Notes { get; set; }
            public string TotalAmount { get; set; }
            public bool SampleGiven { get; set; }
            public bool PriorityJob { get; set; }
            public bool IsRemoved { get; set; }

            public virtual Customer Customer { get; set; }
        }

        public class JobOrderDetails
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            [ForeignKey("JobOrder")]
            public int JobOrderId { get; set; }

            [ForeignKey("Product")]
            public int ProductId { get; set; }
            public float UnitPrice { get; set; }
            public int Quantity { get; set; }
            public int Discount { get; set; }

            public virtual JobOrder JobOrder { get; set; }
            public virtual Product Product { get; set; }
        }

        public class Product
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            public string Name { get; set; }
            public float Price { get; set; }
            public float CostPrice { get; set; }
            public float SellPriceInc { get; set; }
            public float SellPriceEx { get; set; }

            [ForeignKey("Supplier")]
            public int SupplierId { get; set; }

            [ForeignKey("ProductCategory")]
            public int ProductCategoryId { get; set; }

            public virtual Supplier Supplier { get; set; }
            public virtual ProductCategory ProductCategory { get; set; }

        }  
        
        public class ProductCategory
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            
            [Required]
            public string Name { get; set; }
            public string Description { get; set; }
        }

        public class Supplier
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            public string Name { get; set; }
            public string Description { get; set; }
            public string ContactName { get; set; }

            [DataType(DataType.EmailAddress)]
            public string ContactEmail { get; set; }

            [MaxLength(11)]
            public string ContactPhone { get; set; }

            [ForeignKey("Address")]
            public int AddressId { get; set; }

            public virtual Address Address { get; set; }
        }

    #endregion

    #endregion
}
