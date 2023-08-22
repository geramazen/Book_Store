namespace Book_Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RatingAddedToTheBooks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Rate", c => c.Int(nullable: true));
            AddColumn("dbo.Books", "NumberOfRates", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "NumberOfRates");
            DropColumn("dbo.Books", "Rate");
        }
    }
}
