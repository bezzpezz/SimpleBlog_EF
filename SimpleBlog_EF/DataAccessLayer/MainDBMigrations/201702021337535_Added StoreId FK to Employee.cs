namespace SimpleBlog_EF.DataAccessLayer.MainDBMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedStoreIdFKtoEmployee : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("MainDB.Employee", "ManagerId", "MainDB.Employee");
            DropForeignKey("MainDB.Store", "ManagerEmployeeId", "MainDB.Employee");
            AddColumn("MainDB.Employee", "StoreId", c => c.Int(nullable: false));
            CreateIndex("MainDB.Employee", "StoreId");
            AddForeignKey("MainDB.Employee", "StoreId", "MainDB.Store", "StoreId", cascadeDelete: true);
            AddForeignKey("MainDB.Employee", "ManagerId", "MainDB.Employee", "Id", cascadeDelete: true);
            AddForeignKey("MainDB.Store", "ManagerEmployeeId", "MainDB.Employee", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("MainDB.Store", "ManagerEmployeeId", "MainDB.Employee");
            DropForeignKey("MainDB.Employee", "ManagerId", "MainDB.Employee");
            DropForeignKey("MainDB.Employee", "StoreId", "MainDB.Store");
            DropIndex("MainDB.Employee", new[] { "StoreId" });
            DropColumn("MainDB.Employee", "StoreId");
            AddForeignKey("MainDB.Store", "ManagerEmployeeId", "MainDB.Employee", "Id", cascadeDelete: true);
            AddForeignKey("MainDB.Employee", "ManagerId", "MainDB.Employee", "Id");
        }
    }
}
