namespace CRM_Nomenclatyre.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class var1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tab_Article", "Size", c => c.String());
            AddColumn("dbo.tab_Article", "Barcod", c => c.Int(nullable: false, defaultValue: 0));
            AddColumn("dbo.tab_Article", "Count", c => c.Int(nullable: false, defaultValue: 0));
            AddColumn("dbo.tab_Article", "Articul", c => c.Int(nullable: false, defaultValue: 0));

        }
        
        public override void Down()
        {
            DropColumn("dbo.tab_Article", "Articul");
            DropColumn("dbo.tab_Article", "Count");
            DropColumn("dbo.tab_Article", "Barcod");
            DropColumn("dbo.tab_Article", "Size");
        }
    }
}
