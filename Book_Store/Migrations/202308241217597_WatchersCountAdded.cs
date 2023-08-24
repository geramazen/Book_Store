namespace Book_Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WatchersCountAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "WatchersCount", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "WatchersCount");
        }
    }
}
