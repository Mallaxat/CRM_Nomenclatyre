namespace CRM_Nomenclatyre.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProfitUnitArt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UnitArts", "Profit", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UnitArts", "Profit");
        }
    }
}
