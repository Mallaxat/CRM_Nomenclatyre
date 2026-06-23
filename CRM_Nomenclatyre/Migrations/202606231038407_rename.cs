namespace CRM_Nomenclatyre.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rename : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.TypeCommissions", name: "SortId", newName: "TovarId");
            RenameIndex(table: "dbo.TypeCommissions", name: "IX_TypeCommission_SortId", newName: "IX_TypeCommission_TovarId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.TypeCommissions", name: "IX_TypeCommission_TovarId", newName: "IX_TypeCommission_SortId");
            RenameColumn(table: "dbo.TypeCommissions", name: "TovarId", newName: "SortId");
        }
    }
}
