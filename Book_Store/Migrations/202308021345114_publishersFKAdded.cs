namespace Book_Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class publishersFKAdded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "Publisher_PID", "dbo.Publishers");
            DropIndex("dbo.Books", new[] { "Publisher_PID" });
            RenameColumn(table: "dbo.Books", name: "Publisher_PID", newName: "PID");
            AlterColumn("dbo.Books", "PID", c => c.Int(nullable: false));
            CreateIndex("dbo.Books", "PID");
            AddForeignKey("dbo.Books", "PID", "dbo.Publishers", "PID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "PID", "dbo.Publishers");
            DropIndex("dbo.Books", new[] { "PID" });
            AlterColumn("dbo.Books", "PID", c => c.Int());
            RenameColumn(table: "dbo.Books", name: "PID", newName: "Publisher_PID");
            CreateIndex("dbo.Books", "Publisher_PID");
            AddForeignKey("dbo.Books", "Publisher_PID", "dbo.Publishers", "PID");
        }
    }
}
