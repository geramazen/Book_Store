namespace Book_Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBupdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Books", "AvailableCopies", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "role");
            DropColumn("dbo.Users", "image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "image", c => c.Binary());
            AddColumn("dbo.Users", "role", c => c.String());
            DropColumn("dbo.Books", "AvailableCopies");
            DropColumn("dbo.Books", "Price");
        }
    }
}
