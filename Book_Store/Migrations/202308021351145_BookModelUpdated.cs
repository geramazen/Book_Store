namespace Book_Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookModelUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "VolumesNum", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "VolumesNum");
        }
    }
}
