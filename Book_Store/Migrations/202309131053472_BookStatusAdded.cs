namespace Book_Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookStatusAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "BookStaus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "BookStaus");
        }
    }
}
