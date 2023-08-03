namespace Book_Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderTableUpdated1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "MobileNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "MobileNumber", c => c.Int(nullable: false));
        }
    }
}
