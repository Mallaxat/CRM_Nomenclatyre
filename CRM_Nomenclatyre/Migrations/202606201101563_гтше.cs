namespace CRM_Nomenclatyre.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class гтше : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Articles", "Unit_Id", "dbo.UnitArts");
            DropIndex("dbo.Articles", new[] { "Unit_Id" });

            // Временное поле: какой Article владеет этой UnitArt
            AddColumn("dbo.UnitArts", "ArticleId", c => c.Int(nullable: true));

            // Берём Articles.Id по старой связи Articles.Unit_Id -> UnitArts.Id
            Sql(@"
        UPDATE ua
        SET ArticleId = a.Id
        FROM dbo.UnitArts ua
        INNER JOIN dbo.Articles a ON a.Unit_Id = ua.Id
    ");

            DropPrimaryKey("dbo.UnitArts");
            DropColumn("dbo.UnitArts", "Id");

            RenameColumn("dbo.UnitArts", "ArticleId", "Id");
            AlterColumn("dbo.UnitArts", "Id", c => c.Int(nullable: false));

            AddPrimaryKey("dbo.UnitArts", "Id");

            AddForeignKey(
                "dbo.UnitArts",
                "Id",
                "dbo.Articles",
                "Id",
                cascadeDelete: true);

            DropColumn("dbo.Articles", "Unit_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "Unit_Id", c => c.Int());
            DropIndex("dbo.UnitArts", new[] { "Id" });
            DropPrimaryKey("dbo.UnitArts");
            AlterColumn("dbo.UnitArts", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.UnitArts", "Id");
            RenameColumn(table: "dbo.UnitArts", name: "Id", newName: "Unit_Id");
            AddColumn("dbo.UnitArts", "Id", c => c.Int(nullable: false, identity: true));
            CreateIndex("dbo.Articles", "Unit_Id");
            AddForeignKey("dbo.Articles", "Unit_Id", "dbo.UnitArts", "Id");
        }
    }
}
