namespace CRM_Nomenclatyre.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbUpdate_1906 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tab_UnitArt", "Id", "dbo.tab_Article");
            DropIndex("dbo.tab_UnitArt", new[] { "Id" });
            DropPrimaryKey("dbo.tab_UnitArt");

            RenameTable(name: "dbo.tab_Article", newName: "Articles");
            RenameTable(name: "dbo.tab_Manager", newName: "Managers");
            RenameTable(name: "dbo.tab_Users", newName: "Users");
            RenameTable(name: "dbo.tab_TypeTovar", newName: "TypeTovars");
            RenameTable(name: "dbo.tab_UnitArt", newName: "UnitArts");
            RenameTable(name: "dbo.tab_TypeCommission", newName: "TypeCommissions");

            AddColumn("dbo.Articles", "Unit_Id", c => c.Int());

            AlterColumn("dbo.UnitArts", "Id", c => c.Int(nullable: false, identity: true));

            AddPrimaryKey("dbo.UnitArts", "Id");
            CreateIndex("dbo.Articles", "Unit_Id");
            AddForeignKey("dbo.Articles", "Unit_Id", "dbo.UnitArts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "Unit_Id", "dbo.UnitArts");
            DropIndex("dbo.Articles", new[] { "Unit_Id" });
            DropPrimaryKey("dbo.UnitArts");
            AlterColumn("dbo.UnitArts", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.Articles", "Unit_Id");
            AddPrimaryKey("dbo.UnitArts", "Id");
            CreateIndex("dbo.UnitArts", "Id");
            AddForeignKey("dbo.tab_UnitArt", "Id", "dbo.tab_Article", "Id");
            RenameTable(name: "dbo.TypeCommissions", newName: "tab_TypeCommission");
            RenameTable(name: "dbo.UnitArts", newName: "tab_UnitArt");
            RenameTable(name: "dbo.TypeTovars", newName: "tab_TypeTovar");
            RenameTable(name: "dbo.Users", newName: "tab_Users");
            RenameTable(name: "dbo.Managers", newName: "tab_Manager");
            RenameTable(name: "dbo.Articles", newName: "tab_Article");
        }
    }
}
