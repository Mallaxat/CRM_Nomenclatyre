namespace Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tab_Article",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Named = c.String(nullable: false),
                        Sort = c.String(nullable: false),
                        ManagerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tab_Manager", t => t.ManagerId, cascadeDelete: true)
                .Index(t => t.ManagerId);
            
            CreateTable(
                "dbo.tab_Manager",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.tab_Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.tab_Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tab_Manager", "UserId", "dbo.tab_Users");
            DropForeignKey("dbo.tab_Article", "ManagerId", "dbo.tab_Manager");
            DropIndex("dbo.tab_Manager", new[] { "UserId" });
            DropIndex("dbo.tab_Article", new[] { "ManagerId" });
            DropTable("dbo.tab_Users");
            DropTable("dbo.tab_Manager");
            DropTable("dbo.tab_Article");
        }
    }
}
