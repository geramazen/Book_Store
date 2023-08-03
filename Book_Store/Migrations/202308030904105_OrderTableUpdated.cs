namespace Book_Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderTableUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "BookName", c => c.String());
            AddColumn("dbo.Orders", "PublisherName", c => c.String());
            AddColumn("dbo.Orders", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Amount");
            DropColumn("dbo.Orders", "PublisherName");
            DropColumn("dbo.Orders", "BookName");
        }
    }
}
