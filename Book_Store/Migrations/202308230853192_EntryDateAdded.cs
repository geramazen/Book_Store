namespace Book_Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntryDateAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "EntryDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "EntryDate");
        }
    }
}
