namespace SimpleBlog_EF.DataAccessLayer.MainDBMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterPostsandtagstogetpivottable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("MainDB.TagPost", "Post_Id", "MainDB.Post");
            DropForeignKey("MainDB.TagPost", "Tag_Id", "MainDB.Tag");
            RenameColumn(table: "MainDB.TagPost", name: "Tag_Id", newName: "Tag_TagId");
            RenameColumn(table: "MainDB.TagPost", name: "Post_Id", newName: "Post_PostId");
            RenameIndex(table: "MainDB.TagPost", name: "IX_Tag_Id", newName: "IX_Tag_TagId");
            RenameIndex(table: "MainDB.TagPost", name: "IX_Post_Id", newName: "IX_Post_PostId");
            DropPrimaryKey("MainDB.Post");
            DropPrimaryKey("MainDB.Tag");
            AddColumn("MainDB.Post", "PostId", c => c.Int(nullable: false, identity: true));
            AddColumn("MainDB.Tag", "TagId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("MainDB.Post", "PostId");
            AddPrimaryKey("MainDB.Tag", "TagId");
            AddForeignKey("MainDB.TagPost", "Post_PostId", "MainDB.Post", "PostId", cascadeDelete: true);
            AddForeignKey("MainDB.TagPost", "Tag_TagId", "MainDB.Tag", "TagId", cascadeDelete: true);
            DropColumn("MainDB.Post", "Id");
            DropColumn("MainDB.Tag", "Id");
        }
        
        public override void Down()
        {
            AddColumn("MainDB.Tag", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("MainDB.Post", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("MainDB.TagPost", "Tag_TagId", "MainDB.Tag");
            DropForeignKey("MainDB.TagPost", "Post_PostId", "MainDB.Post");
            DropPrimaryKey("MainDB.Tag");
            DropPrimaryKey("MainDB.Post");
            DropColumn("MainDB.Tag", "TagId");
            DropColumn("MainDB.Post", "PostId");
            AddPrimaryKey("MainDB.Tag", "Id");
            AddPrimaryKey("MainDB.Post", "Id");
            RenameIndex(table: "MainDB.TagPost", name: "IX_Post_PostId", newName: "IX_Post_Id");
            RenameIndex(table: "MainDB.TagPost", name: "IX_Tag_TagId", newName: "IX_Tag_Id");
            RenameColumn(table: "MainDB.TagPost", name: "Post_PostId", newName: "Post_Id");
            RenameColumn(table: "MainDB.TagPost", name: "Tag_TagId", newName: "Tag_Id");
            AddForeignKey("MainDB.TagPost", "Tag_Id", "MainDB.Tag", "Id", cascadeDelete: true);
            AddForeignKey("MainDB.TagPost", "Post_Id", "MainDB.Post", "Id", cascadeDelete: true);
        }
    }
}
