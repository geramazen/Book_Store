namespace Book_Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WatchersCountUpdated : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "WatchersCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "WatchersCount", c => c.Int());
        }
    }
}
