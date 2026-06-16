namespace CRM_Nomenclatyre.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class var4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tab_UnitArt",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        CostPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Logistics = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Comission = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tab_Article", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.tab_TypeCommission",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SortId = c.Int(nullable: false),
                        NameValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tab_TypeTovar", t => t.SortId, cascadeDelete: true)
                .Index(t => t.SortId, unique: true, name: "IX_TypeCommission_SortId");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tab_TypeCommission", "SortId", "dbo.tab_TypeTovar");
            DropForeignKey("dbo.tab_UnitArt", "Id", "dbo.tab_Article");
            DropIndex("dbo.tab_TypeCommission", "IX_TypeCommission_SortId");
            DropIndex("dbo.tab_UnitArt", new[] { "Id" });
            DropTable("dbo.tab_TypeCommission");
            DropTable("dbo.tab_UnitArt");
        }
    }
}
