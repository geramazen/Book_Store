namespace Book_Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UsersRateAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UsersRates",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UID = c.Int(nullable: false),
                        BID = c.Int(nullable: false),
                        Rate = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UsersRates");
        }
    }
}
