namespace Book_Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        AID = c.Int(nullable: false, identity: true),
                        FName = c.String(),
                        LName = c.String(),
                    })
                .PrimaryKey(t => t.AID);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        title = c.String(),
                        description = c.String(),
                        image = c.String(),
                        AID = c.Int(nullable: false),
                        CID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Authors", t => t.AID, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CID, cascadeDelete: true)
                .Index(t => t.AID)
                .Index(t => t.CID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CID = c.Int(nullable: false, identity: true),
                        CName = c.String(),
                    })
                .PrimaryKey(t => t.CID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        EMAIL = c.String(),
                        Password = c.String(),
                        role = c.String(),
                        image = c.Binary(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "CID", "dbo.Categories");
            DropForeignKey("dbo.Books", "AID", "dbo.Authors");
            DropIndex("dbo.Books", new[] { "CID" });
            DropIndex("dbo.Books", new[] { "AID" });
            DropTable("dbo.Users");
            DropTable("dbo.Categories");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}
