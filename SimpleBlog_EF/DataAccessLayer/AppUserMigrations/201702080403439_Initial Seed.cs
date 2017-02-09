namespace SimpleBlog_EF.DataAccessLayer.AppUserMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialSeed : DbMigration
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
                "AppUsers.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Avatar = c.String(),
                        PasswordHash = c.String(nullable: false),
                        Role_RoleId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("AppUsers.Role", t => t.Role_RoleId)
                .Index(t => t.Role_RoleId);
            
            CreateTable(
                "AppUsers.UserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("AppUsers.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("AppUsers.Role", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("AppUsers.User", "Role_RoleId", "AppUsers.Role");
            DropForeignKey("AppUsers.UserRoles", "RoleId", "AppUsers.Role");
            DropForeignKey("AppUsers.UserRoles", "UserId", "AppUsers.User");
            DropIndex("AppUsers.UserRoles", new[] { "RoleId" });
            DropIndex("AppUsers.UserRoles", new[] { "UserId" });
            DropIndex("AppUsers.User", new[] { "Role_RoleId" });
            DropTable("AppUsers.UserRoles");
            DropTable("AppUsers.User");
            DropTable("AppUsers.Role");
        }
    }
}
