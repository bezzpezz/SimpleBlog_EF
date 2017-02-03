namespace SimpleBlog_EF.DataAccessLayer.AppUserMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserAvatar : DbMigration
    {
        public override void Up()
        {
            AddColumn("AppUsers.User", "Avatar", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("AppUsers.User", "Avatar");
        }
    }
}
