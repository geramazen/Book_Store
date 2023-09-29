namespace Book_Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderuserid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "UserID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "UserID");
        }
    }
}
