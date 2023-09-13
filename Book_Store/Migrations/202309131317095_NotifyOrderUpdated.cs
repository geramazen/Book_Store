namespace Book_Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotifyOrderUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NotifyOrders", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NotifyOrders", "Status");
        }
    }
}
