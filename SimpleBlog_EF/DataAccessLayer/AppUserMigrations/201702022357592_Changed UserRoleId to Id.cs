namespace SimpleBlog_EF.DataAccessLayer.AppUserMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedUserRoleIdtoId : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("AppUsers.UserRole");
            DropColumn("AppUsers.UserRole", "UserRoleId");
            AddColumn("AppUsers.UserRole", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("AppUsers.UserRole", "Id");
        }
        
        public override void Down()
        {
            AddColumn("AppUsers.UserRole", "UserRoleId", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("AppUsers.UserRole");
            DropColumn("AppUsers.UserRole", "Id");
            AddPrimaryKey("AppUsers.UserRole", "UserRoleId");
        }
    }
}
