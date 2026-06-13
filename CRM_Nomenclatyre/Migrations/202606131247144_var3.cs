namespace CRM_Nomenclatyre.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class var3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tab_TypeTovar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.tab_Article", "Sort", c => c.Int(nullable: false));
            CreateIndex("dbo.tab_Article", "Sort");
            AddForeignKey("dbo.tab_Article", "Sort", "dbo.tab_TypeTovar", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tab_Article", "Sort", "dbo.tab_TypeTovar");
            DropIndex("dbo.tab_Article", new[] { "Sort" });
            AlterColumn("dbo.tab_Article", "Sort", c => c.String(nullable: false));
            DropTable("dbo.tab_TypeTovar");
        }
    }
}
