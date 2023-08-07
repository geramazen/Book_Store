namespace Book_Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShippingCostAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShippingCosts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ShippingCosts");
        }
    }
}
