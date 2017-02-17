namespace SimpleBlog_EF.DataAccessLayer.MainDBMigrations
{
    using Models.DataBase;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SimpleBlog_EF.DataAccessLayer.MainDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DataAccessLayer\MainDBMigrations\";
        }

        protected override void Seed(SimpleBlog_EF.DataAccessLayer.MainDBContext context)
        {
            #region AppUser Creation
            if (!context.Users.Any())
            {
                context.Roles.AddOrUpdate(
                    new Role() { Name = "user" },
                    new Role() { Name = "editor" },
                    new Role() { Name = "admin" }
                );

                context.Users.AddOrUpdate(//user => user.UserId,
                    new User()
                    {
                        Username = "Beren",
                        Email = "berenlyell@hotmail.com",
                        PasswordHash = "$2a$13$LEyYCZqiAdkLdnz.nrDFyuoV/k.jsj.g5T0XPjCG.Ifu8vAD4djgy"
                    },
                    new User()
                    {
                        Username = "Steve",
                        Email = "steve@test.com",
                        PasswordHash = "$2a$13$LEyYCZqiAdkLdnz.nrDFyuoV/k.jsj.g5T0XPjCG.Ifu8vAD4djgy",

                    }
                );
            }
            #endregion


            #region Lookup table data / types addition
            context.AddresseTypes.AddOrUpdate(
            new AddressType() { Desc = "Postal" },
            new AddressType() { Desc = "Billing" },
            new AddressType() { Desc = "Work" },
            new AddressType() { Desc = "Home" },
            new AddressType() { Desc = "Office" },
            new AddressType() { Desc = "Head Office" },
            new AddressType() { Desc = "Factory" }
            );

            context.CustomerTypes.AddOrUpdate(
                new CustomerType() { Desc = "Business" },
                new CustomerType() { Desc = "Company" },
                new CustomerType() { Desc = "Individual" },
                new CustomerType() { Desc = "Sole Trader" }
            );

            context.Countries.AddOrUpdate(c=>c.Id,
                new Country() { Name = "Australia" },
                new Country() { Name = "USA" }
            );

            context.Cities.AddOrUpdate(
                new City() { CityName = "Sydney"},
                new City() { CityName = "Brisbane"},
                new City() { CityName = "Melbourne"},
                new City() { CityName = "Adelaide" },
                new City() { CityName = "Perth"},
                new City() { CityName = "Darwin" },
                new City() { CityName = "Sydney" }
            );



            //context.DeliveryStatuses.AddOrUpdate(
            //    new DeliveryStatus() { DeliveryStatusCode = 1, DeliveryStatusDesc = "In Transit" },
            //    new DeliveryStatus() { DeliveryStatusCode = 1, DeliveryStatusDesc = "In Progress" },
            //    new DeliveryStatus() { DeliveryStatusCode = 1, DeliveryStatusDesc = "Waiting on Third Party" },
            //    new DeliveryStatus() { DeliveryStatusCode = 1, DeliveryStatusDesc = "Delivered" },
            //    new DeliveryStatus() { DeliveryStatusCode = 1, DeliveryStatusDesc = "Missing" },
            //    new DeliveryStatus() { DeliveryStatusCode = 1, DeliveryStatusDesc = "Stolen" }
            //);

            //context.JobOrderStatuses.AddOrUpdate(
            //    new JobOrderStatus() { StatusDesc = "Loaded" },
            //    new JobOrderStatus() { StatusDesc = "Undercoated" },
            //    new JobOrderStatus() { StatusDesc = "Topcoated" },
            //    new JobOrderStatus() { StatusDesc = "QC" },
            //    new JobOrderStatus() { StatusDesc = "RS" },
            //    new JobOrderStatus() { StatusDesc = "QCReady" },
            //    new JobOrderStatus() { StatusDesc = "Delivered" },
            //    new JobOrderStatus() { StatusDesc = "Added" }
            //);

            //context.Addresses.AddOrUpdate(x => x.Id,
                //new Address() { AddressLine1 = "22 shlong Crescent", AddressLine2 = ""}
            //);

            //context.Suppliers.AddOrUpdate(s=>s.Id,
                //new Supplier() { Name = "Self Supplier", Description = "Self supplied", ContactName = "Test Contact", ContactEmail = "test@test.com", ContactPhone = "12345678910" , AddressId = 1}
            //);

            context.ProductCategories.AddOrUpdate(i=>i.Id,
                new ProductCategory() { Name = "Services" },
                new ProductCategory() { Name = "Painting" },
                new ProductCategory() { Name = "Glossing" },
                new ProductCategory() { Name = "Wood work" },
                new ProductCategory() { Name = "Glueing" },
                new ProductCategory() { Name = "Labour" },
                new ProductCategory() { Name = "Misc" }
            );

            //context.Products.AddOrUpdate(p=>p.Id,
            //    new Product() { Name = "Product 1", SupplierId = 1, ProductCategoryId = 1, CostPrice = 300.36f },
            //    new Product() { Name = "Product 2", SupplierId = 1, ProductCategoryId = 2, CostPrice = 3256.36f },
            //    new Product() { Name = "Product 3", SupplierId = 1, ProductCategoryId = 6, CostPrice = 500.50f }
            //);

            context.PaymentTypes.AddOrUpdate(
                new PaymentType() { Name = "Cash" },
                new PaymentType() { Name = "Credit" },
                new PaymentType() { Name = "Cheque" },
                new PaymentType() { Name = "Bank Cheque" },
                new PaymentType() { Name = "Money Order" },
                new PaymentType() { Name = "EFTPOS" },
                new PaymentType() { Name = "PayWave" },
                new PaymentType() { Name = "Paypal" }
            );

            #endregion

        }
    }
}
