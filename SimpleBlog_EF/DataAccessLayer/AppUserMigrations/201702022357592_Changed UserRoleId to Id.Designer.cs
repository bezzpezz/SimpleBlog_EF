// <auto-generated />
namespace SimpleBlog_EF.DataAccessLayer.AppUserMigrations
{
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    [GeneratedCode("EntityFramework.Migrations", "6.1.3-40302")]
    public sealed partial class ChangedUserRoleIdtoId : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(ChangedUserRoleIdtoId));
        
        string IMigrationMetadata.Id
        {
            get { return "201702022357592_Changed UserRoleId to Id"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}
