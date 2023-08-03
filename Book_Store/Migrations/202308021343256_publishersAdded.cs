namespace Book_Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class publishersAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Publishers",
                c => new
                    {
                        PID = c.Int(nullable: false, identity: true),
                        PName = c.String(),
                    })
                .PrimaryKey(t => t.PID);
            
            AddColumn("dbo.Books", "Publisher_PID", c => c.Int());
            CreateIndex("dbo.Books", "Publisher_PID");
            AddForeignKey("dbo.Books", "Publisher_PID", "dbo.Publishers", "PID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "Publisher_PID", "dbo.Publishers");
            DropIndex("dbo.Books", new[] { "Publisher_PID" });
            DropColumn("dbo.Books", "Publisher_PID");
            DropTable("dbo.Publishers");
        }
    }
}
