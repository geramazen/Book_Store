namespace Book_Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notNullDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "EntryDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "EntryDate", c => c.DateTime());
        }
    }
}
