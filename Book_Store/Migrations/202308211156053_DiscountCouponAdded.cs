namespace Book_Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DiscountCouponAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DiscountCoupons",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        percentage = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Orders", "DiscountCoupon", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "DiscountCoupon");
            DropTable("dbo.DiscountCoupons");
        }
    }
}
