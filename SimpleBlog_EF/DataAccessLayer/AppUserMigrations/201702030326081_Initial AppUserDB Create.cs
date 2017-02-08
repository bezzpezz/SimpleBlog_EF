namespace SimpleBlog_EF.DataAccessLayer.AppUserMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialAppUserDBCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "AppUsers.Role",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "AppUsers.UserRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("AppUsers.Role", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("AppUsers.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "AppUsers.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Avatar = c.String(),
                        Password_Hash = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("AppUsers.UserRole", "UserId", "AppUsers.User");
            DropForeignKey("AppUsers.UserRole", "RoleId", "AppUsers.Role");
            DropIndex("AppUsers.UserRole", new[] { "RoleId" });
            DropIndex("AppUsers.UserRole", new[] { "UserId" });
            DropTable("AppUsers.User");
            DropTable("AppUsers.UserRole");
            DropTable("AppUsers.Role");
        }
    }
}
