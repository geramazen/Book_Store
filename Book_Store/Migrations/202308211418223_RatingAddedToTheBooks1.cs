namespace Book_Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RatingAddedToTheBooks1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Rate", c => c.Int());
            AlterColumn("dbo.Books", "NumberOfRates", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "NumberOfRates", c => c.Int(nullable: false));
            AlterColumn("dbo.Books", "Rate", c => c.Int(nullable: false));
        }
    }
}
