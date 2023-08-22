namespace Book_Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RatingBookUpdated : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Rate", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "Rate", c => c.Int());
        }
    }
}
