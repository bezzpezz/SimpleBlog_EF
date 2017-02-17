namespace SimpleBlog_EF.DataAccessLayer.MainDBMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MainDBInitialCreateandSeed : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "MainDB.Address",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AddressLine1 = c.String(),
                        AddressLine2 = c.String(),
                        PostCode = c.String(maxLength: 10),
                        AddressTypeId = c.Int(nullable: false),
                        City_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("MainDB.AddressType", t => t.AddressTypeId, cascadeDelete: true)
                .ForeignKey("MainDB.City", t => t.City_Id)
                .Index(t => t.AddressTypeId)
                .Index(t => t.City_Id);
            
            CreateTable(
                "MainDB.AddressType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Desc = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "MainDB.City",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CityName = c.String(),
                        Country_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("MainDB.Country", t => t.Country_Id)
                .Index(t => t.Country_Id);
            
            CreateTable(
                "MainDB.Country",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LastUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "MainDB.Customer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Active = c.Boolean(nullable: false),
                        CustomerTypeId = c.Int(nullable: false),
                        CreateDate = c.DateTime(),
                        LastUpdate = c.DateTime(),
                        HomePhone = c.String(maxLength: 11),
                        WorkPhone = c.String(maxLength: 11),
                        MobilePhone = c.String(maxLength: 11),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("MainDB.CustomerType", t => t.CustomerTypeId, cascadeDelete: true)
                .Index(t => t.CustomerTypeId);
            
            CreateTable(
                "MainDB.CustomerType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Desc = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "MainDB.Employee",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ManagerId = c.Int(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        AddressId = c.Int(nullable: false),
                        Email = c.String(),
                        DateOfBirth = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        Avatar = c.String(),
                        DateOfCommenceWork = c.DateTime(),
                        DateOfLeaveWork = c.DateTime(),
                        DateCreated = c.DateTime(),
                        LastUpdate = c.DateTime(),
                        Username = c.String(),
                        Password = c.String(),
                        Store_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("MainDB.Address", t => t.AddressId, cascadeDelete: true)
                .ForeignKey("MainDB.Employee", t => t.ManagerId)
                .ForeignKey("MainDB.Store", t => t.Store_Id)
                .Index(t => t.ManagerId)
                .Index(t => t.AddressId)
                .Index(t => t.Store_Id);
            
            CreateTable(
                "MainDB.Store",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AddressId = c.Int(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("MainDB.Address", t => t.AddressId, cascadeDelete: true)
                .Index(t => t.AddressId);
            
            CreateTable(
                "MainDB.JobOrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobOrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        UnitPrice = c.Single(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Discount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("MainDB.JobOrder", t => t.JobOrderId, cascadeDelete: true)
                .ForeignKey("MainDB.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.JobOrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "MainDB.JobOrder",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobOrderCode = c.String(nullable: false),
                        Color = c.String(),
                        GlossLevel = c.String(),
                        NoOfParts = c.String(),
                        DateRecieved = c.DateTime(nullable: false),
                        DateDue = c.DateTime(nullable: false),
                        DeliveryInfo = c.String(),
                        CustomerId = c.Int(nullable: false),
                        CustomerPaymentMethodId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        DateLoaded = c.DateTime(),
                        DateUnderCoated = c.DateTime(),
                        DateTopCoated = c.DateTime(),
                        DateOfQC = c.DateTime(),
                        DateOfLastRespray = c.DateTime(),
                        ResprayCount = c.Int(),
                        DateOfLastUpdate = c.DateTime(),
                        DateQCReady = c.DateTime(),
                        DateOrderPaid = c.DateTime(),
                        DateOrderComplete = c.DateTime(),
                        DateOrderDelivered = c.DateTime(),
                        IsDelevered = c.Boolean(nullable: false),
                        Notes = c.String(),
                        TotalAmount = c.String(),
                        SampleGiven = c.Boolean(nullable: false),
                        PriorityJob = c.Boolean(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("MainDB.Customer", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "MainDB.Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Single(nullable: false),
                        CostPrice = c.Single(nullable: false),
                        SellPriceInc = c.Single(nullable: false),
                        SellPriceEx = c.Single(nullable: false),
                        SupplierId = c.Int(nullable: false),
                        ProductCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("MainDB.ProductCategory", t => t.ProductCategoryId, cascadeDelete: true)
                .ForeignKey("MainDB.Supplier", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.SupplierId)
                .Index(t => t.ProductCategoryId);
            
            CreateTable(
                "MainDB.ProductCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "MainDB.Supplier",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        ContactName = c.String(),
                        ContactEmail = c.String(),
                        ContactPhone = c.String(maxLength: 11),
                        AddressId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("MainDB.Address", t => t.AddressId, cascadeDelete: true)
                .Index(t => t.AddressId);
            
            CreateTable(
                "MainDB.Payment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        PaymentTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("MainDB.Customer", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("MainDB.Employee", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("MainDB.PaymentType", t => t.PaymentTypeId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.EmployeeId)
                .Index(t => t.PaymentTypeId);
            
            CreateTable(
                "MainDB.PaymentType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "MainDB.Post",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Title = c.String(),
                        Slug = c.String(),
                        Content = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("MainDB.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "MainDB.Tag",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        Slug = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.TagId);
            
            CreateTable(
                "MainDB.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Avatar = c.String(),
                        PasswordHash = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "MainDB.Role",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "MainDB.TagPost",
                c => new
                    {
                        Tag_TagId = c.Int(nullable: false),
                        Post_PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_TagId, t.Post_PostId })
                .ForeignKey("MainDB.Tag", t => t.Tag_TagId, cascadeDelete: true)
                .ForeignKey("MainDB.Post", t => t.Post_PostId, cascadeDelete: true)
                .Index(t => t.Tag_TagId)
                .Index(t => t.Post_PostId);
            
            CreateTable(
                "MainDB.UserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("MainDB.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("MainDB.Role", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("MainDB.Post", "UserId", "MainDB.User");
            DropForeignKey("MainDB.UserRoles", "RoleId", "MainDB.Role");
            DropForeignKey("MainDB.UserRoles", "UserId", "MainDB.User");
            DropForeignKey("MainDB.TagPost", "Post_PostId", "MainDB.Post");
            DropForeignKey("MainDB.TagPost", "Tag_TagId", "MainDB.Tag");
            DropForeignKey("MainDB.Payment", "PaymentTypeId", "MainDB.PaymentType");
            DropForeignKey("MainDB.Payment", "EmployeeId", "MainDB.Employee");
            DropForeignKey("MainDB.Payment", "CustomerId", "MainDB.Customer");
            DropForeignKey("MainDB.JobOrderDetails", "ProductId", "MainDB.Product");
            DropForeignKey("MainDB.Product", "SupplierId", "MainDB.Supplier");
            DropForeignKey("MainDB.Supplier", "AddressId", "MainDB.Address");
            DropForeignKey("MainDB.Product", "ProductCategoryId", "MainDB.ProductCategory");
            DropForeignKey("MainDB.JobOrderDetails", "JobOrderId", "MainDB.JobOrder");
            DropForeignKey("MainDB.JobOrder", "CustomerId", "MainDB.Customer");
            DropForeignKey("MainDB.Employee", "Store_Id", "MainDB.Store");
            DropForeignKey("MainDB.Store", "AddressId", "MainDB.Address");
            DropForeignKey("MainDB.Employee", "ManagerId", "MainDB.Employee");
            DropForeignKey("MainDB.Employee", "AddressId", "MainDB.Address");
            DropForeignKey("MainDB.Customer", "CustomerTypeId", "MainDB.CustomerType");
            DropForeignKey("MainDB.Address", "City_Id", "MainDB.City");
            DropForeignKey("MainDB.City", "Country_Id", "MainDB.Country");
            DropForeignKey("MainDB.Address", "AddressTypeId", "MainDB.AddressType");
            DropIndex("MainDB.UserRoles", new[] { "RoleId" });
            DropIndex("MainDB.UserRoles", new[] { "UserId" });
            DropIndex("MainDB.TagPost", new[] { "Post_PostId" });
            DropIndex("MainDB.TagPost", new[] { "Tag_TagId" });
            DropIndex("MainDB.Post", new[] { "UserId" });
            DropIndex("MainDB.Payment", new[] { "PaymentTypeId" });
            DropIndex("MainDB.Payment", new[] { "EmployeeId" });
            DropIndex("MainDB.Payment", new[] { "CustomerId" });
            DropIndex("MainDB.Supplier", new[] { "AddressId" });
            DropIndex("MainDB.Product", new[] { "ProductCategoryId" });
            DropIndex("MainDB.Product", new[] { "SupplierId" });
            DropIndex("MainDB.JobOrder", new[] { "CustomerId" });
            DropIndex("MainDB.JobOrderDetails", new[] { "ProductId" });
            DropIndex("MainDB.JobOrderDetails", new[] { "JobOrderId" });
            DropIndex("MainDB.Store", new[] { "AddressId" });
            DropIndex("MainDB.Employee", new[] { "Store_Id" });
            DropIndex("MainDB.Employee", new[] { "AddressId" });
            DropIndex("MainDB.Employee", new[] { "ManagerId" });
            DropIndex("MainDB.Customer", new[] { "CustomerTypeId" });
            DropIndex("MainDB.City", new[] { "Country_Id" });
            DropIndex("MainDB.Address", new[] { "City_Id" });
            DropIndex("MainDB.Address", new[] { "AddressTypeId" });
            DropTable("MainDB.UserRoles");
            DropTable("MainDB.TagPost");
            DropTable("MainDB.Role");
            DropTable("MainDB.User");
            DropTable("MainDB.Tag");
            DropTable("MainDB.Post");
            DropTable("MainDB.PaymentType");
            DropTable("MainDB.Payment");
            DropTable("MainDB.Supplier");
            DropTable("MainDB.ProductCategory");
            DropTable("MainDB.Product");
            DropTable("MainDB.JobOrder");
            DropTable("MainDB.JobOrderDetails");
            DropTable("MainDB.Store");
            DropTable("MainDB.Employee");
            DropTable("MainDB.CustomerType");
            DropTable("MainDB.Customer");
            DropTable("MainDB.Country");
            DropTable("MainDB.City");
            DropTable("MainDB.AddressType");
            DropTable("MainDB.Address");
        }
    }
}
