namespace Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.tab_Article", newName: "Articles");
            RenameTable(name: "dbo.tab_Manager", newName: "Managers");
            RenameTable(name: "dbo.tab_Users", newName: "Users");
            CreateTable(
                "dbo.TypeTovars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UnitArts",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        CostPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Logistics = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Comission = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Profit = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.TypeCommissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TovarId = c.Int(nullable: false),
                        NameValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TypeTovars", t => t.TovarId, cascadeDelete: true)
                .Index(t => t.TovarId, unique: true, name: "IX_TypeCommission_TovarId");
            
            AddColumn("dbo.Articles", "TypeTovarID", c => c.Int(nullable: false));
            AddColumn("dbo.Articles", "Size", c => c.String());
            AddColumn("dbo.Articles", "Barcod", c => c.String());
            AddColumn("dbo.Articles", "Count", c => c.Int(nullable: false));
            AddColumn("dbo.Articles", "Articul", c => c.String());
            CreateIndex("dbo.Articles", "TypeTovarID");
            AddForeignKey("dbo.Articles", "TypeTovarID", "dbo.TypeTovars", "Id", cascadeDelete: true);
            DropColumn("dbo.Articles", "Sort");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "Sort", c => c.String(nullable: false));
            DropForeignKey("dbo.TypeCommissions", "TovarId", "dbo.TypeTovars");
            DropForeignKey("dbo.UnitArts", "Id", "dbo.Articles");
            DropForeignKey("dbo.Articles", "TypeTovarID", "dbo.TypeTovars");
            DropIndex("dbo.TypeCommissions", "IX_TypeCommission_TovarId");
            DropIndex("dbo.UnitArts", new[] { "Id" });
            DropIndex("dbo.Articles", new[] { "TypeTovarID" });
            DropColumn("dbo.Articles", "Articul");
            DropColumn("dbo.Articles", "Count");
            DropColumn("dbo.Articles", "Barcod");
            DropColumn("dbo.Articles", "Size");
            DropColumn("dbo.Articles", "TypeTovarID");
            DropTable("dbo.TypeCommissions");
            DropTable("dbo.UnitArts");
            DropTable("dbo.TypeTovars");
            RenameTable(name: "dbo.Users", newName: "tab_Users");
            RenameTable(name: "dbo.Managers", newName: "tab_Manager");
            RenameTable(name: "dbo.Articles", newName: "tab_Article");
        }
    }
}
