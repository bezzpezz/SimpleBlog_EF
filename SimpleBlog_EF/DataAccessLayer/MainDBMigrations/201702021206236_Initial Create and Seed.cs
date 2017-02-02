namespace SimpleBlog_EF.DataAccessLayer.MainDBMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreateandSeed : DbMigration
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
                        CityId = c.Int(nullable: false),
                        PostCode = c.String(maxLength: 10),
                        AddressTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("MainDB.AddressType", t => t.AddressTypeId, cascadeDelete: true)
                .ForeignKey("MainDB.City", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId)
                .Index(t => t.AddressTypeId);
            
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
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("MainDB.Country", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
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
                        Email = c.String(),
                        DateOfBirth = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        AddressId = c.Int(nullable: false),
                        Avatar = c.String(),
                        DateOfCommenceWork = c.DateTime(),
                        DateOfLeaveWork = c.DateTime(),
                        DateCreated = c.DateTime(),
                        LastUpdate = c.DateTime(),
                        Username = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("MainDB.Address", t => t.AddressId, cascadeDelete: true)
                .ForeignKey("MainDB.Employee", t => t.ManagerId)
                .Index(t => t.ManagerId)
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
                "MainDB.Store",
                c => new
                    {
                        StoreId = c.Int(nullable: false, identity: true),
                        ManagerEmployeeId = c.Int(nullable: false),
                        AddressId = c.Int(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.StoreId)
                .ForeignKey("MainDB.Address", t => t.AddressId, cascadeDelete: true)
                .ForeignKey("MainDB.Employee", t => t.ManagerEmployeeId, cascadeDelete: false)
                .Index(t => t.ManagerEmployeeId)
                .Index(t => t.AddressId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("MainDB.Store", "ManagerEmployeeId", "MainDB.Employee");
            DropForeignKey("MainDB.Store", "AddressId", "MainDB.Address");
            DropForeignKey("MainDB.Payment", "PaymentTypeId", "MainDB.PaymentType");
            DropForeignKey("MainDB.Payment", "EmployeeId", "MainDB.Employee");
            DropForeignKey("MainDB.Payment", "CustomerId", "MainDB.Customer");
            DropForeignKey("MainDB.JobOrderDetails", "ProductId", "MainDB.Product");
            DropForeignKey("MainDB.Product", "SupplierId", "MainDB.Supplier");
            DropForeignKey("MainDB.Supplier", "AddressId", "MainDB.Address");
            DropForeignKey("MainDB.Product", "ProductCategoryId", "MainDB.ProductCategory");
            DropForeignKey("MainDB.JobOrderDetails", "JobOrderId", "MainDB.JobOrder");
            DropForeignKey("MainDB.JobOrder", "CustomerId", "MainDB.Customer");
            DropForeignKey("MainDB.Employee", "ManagerId", "MainDB.Employee");
            DropForeignKey("MainDB.Employee", "AddressId", "MainDB.Address");
            DropForeignKey("MainDB.Customer", "CustomerTypeId", "MainDB.CustomerType");
            DropForeignKey("MainDB.Address", "CityId", "MainDB.City");
            DropForeignKey("MainDB.City", "CountryId", "MainDB.Country");
            DropForeignKey("MainDB.Address", "AddressTypeId", "MainDB.AddressType");
            DropIndex("MainDB.Store", new[] { "AddressId" });
            DropIndex("MainDB.Store", new[] { "ManagerEmployeeId" });
            DropIndex("MainDB.Payment", new[] { "PaymentTypeId" });
            DropIndex("MainDB.Payment", new[] { "EmployeeId" });
            DropIndex("MainDB.Payment", new[] { "CustomerId" });
            DropIndex("MainDB.Supplier", new[] { "AddressId" });
            DropIndex("MainDB.Product", new[] { "ProductCategoryId" });
            DropIndex("MainDB.Product", new[] { "SupplierId" });
            DropIndex("MainDB.JobOrder", new[] { "CustomerId" });
            DropIndex("MainDB.JobOrderDetails", new[] { "ProductId" });
            DropIndex("MainDB.JobOrderDetails", new[] { "JobOrderId" });
            DropIndex("MainDB.Employee", new[] { "AddressId" });
            DropIndex("MainDB.Employee", new[] { "ManagerId" });
            DropIndex("MainDB.Customer", new[] { "CustomerTypeId" });
            DropIndex("MainDB.City", new[] { "CountryId" });
            DropIndex("MainDB.Address", new[] { "AddressTypeId" });
            DropIndex("MainDB.Address", new[] { "CityId" });
            DropTable("MainDB.Store");
            DropTable("MainDB.PaymentType");
            DropTable("MainDB.Payment");
            DropTable("MainDB.Supplier");
            DropTable("MainDB.ProductCategory");
            DropTable("MainDB.Product");
            DropTable("MainDB.JobOrder");
            DropTable("MainDB.JobOrderDetails");
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
