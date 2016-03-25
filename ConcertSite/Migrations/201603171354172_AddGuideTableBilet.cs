namespace ConcertSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGuideTableBilet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bilets", "guidBilet", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bilets", "guidBilet");
        }
    }
}
