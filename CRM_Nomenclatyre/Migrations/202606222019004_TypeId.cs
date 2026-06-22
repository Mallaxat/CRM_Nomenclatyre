namespace CRM_Nomenclatyre.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TypeId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Articles", name: "Sort", newName: "TypeTovarID");
            RenameIndex(table: "dbo.Articles", name: "IX_Sort", newName: "IX_TypeTovarID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Articles", name: "IX_TypeTovarID", newName: "IX_Sort");
            RenameColumn(table: "dbo.Articles", name: "TypeTovarID", newName: "Sort");
        }
    }
}
