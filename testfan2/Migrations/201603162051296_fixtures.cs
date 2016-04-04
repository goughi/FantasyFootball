namespace testfan2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixtures : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Fixture", "AwayTeamScore");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Fixture", "AwayTeamScore", c => c.Int());
        }
    }
}
