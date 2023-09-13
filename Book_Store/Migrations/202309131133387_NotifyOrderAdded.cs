namespace Book_Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotifyOrderAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NotifyOrders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BookName = c.String(),
                        MobileNumber = c.String(),
                        BID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.NotifyOrders");
        }
    }
}
