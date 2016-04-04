namespace testfan2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixture6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fixture", "HomeTeamScore", c => c.Int(nullable: false));
            AddColumn("dbo.Fixture", "AwayTeamScore", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Fixture", "AwayTeamScore");
            DropColumn("dbo.Fixture", "HomeTeamScore");
        }
    }
}
