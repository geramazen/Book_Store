namespace Book_Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderDateAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "OrderDate");
        }
    }
}
