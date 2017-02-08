namespace SimpleBlog_EF.DataAccessLayer.AppUserMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppUserDBUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("AppUsers.User", "PasswordHash", c => c.String(nullable: false));
            DropColumn("AppUsers.User", "Password_Hash");
        }
        
        public override void Down()
        {
            AddColumn("AppUsers.User", "Password_Hash", c => c.String(nullable: false));
            DropColumn("AppUsers.User", "PasswordHash");
        }
    }
}
