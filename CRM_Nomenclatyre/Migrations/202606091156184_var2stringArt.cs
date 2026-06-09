namespace CRM_Nomenclatyre.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class var2stringArt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tab_Article", "Barcod", c => c.String());
            AlterColumn("dbo.tab_Article", "Articul", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tab_Article", "Articul", c => c.Int(nullable: false));
            AlterColumn("dbo.tab_Article", "Barcod", c => c.Int(nullable: false));
        }
    }
}
