namespace SimpleBlog_EF.DataAccessLayer.AppUserMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedbacktheAppUserTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("AppUsers.User", "Role_RoleId", "AppUsers.Role");
            DropIndex("AppUsers.User", new[] { "Role_RoleId" });
            DropColumn("AppUsers.User", "Role_RoleId");
        }
        
        public override void Down()
        {
            AddColumn("AppUsers.User", "Role_RoleId", c => c.Int());
            CreateIndex("AppUsers.User", "Role_RoleId");
            AddForeignKey("AppUsers.User", "Role_RoleId", "AppUsers.Role", "RoleId");
        }
    }
}
