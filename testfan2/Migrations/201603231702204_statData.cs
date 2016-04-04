namespace testfan2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class statData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlayerRoundStat", "CleanSheet", c => c.Int(nullable: false));
            AddColumn("dbo.PlayerRoundStat", "GoalsConceded", c => c.Int(nullable: false));
            AddColumn("dbo.PlayerRoundStat", "IsWin", c => c.Boolean(nullable: false));
            AddColumn("dbo.PlayerRoundStat", "ManOfTheMatch", c => c.Boolean(nullable: false));
            DropColumn("dbo.PlayerRoundStat", "OwnGoal");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlayerRoundStat", "OwnGoal", c => c.Int(nullable: false));
            DropColumn("dbo.PlayerRoundStat", "ManOfTheMatch");
            DropColumn("dbo.PlayerRoundStat", "IsWin");
            DropColumn("dbo.PlayerRoundStat", "GoalsConceded");
            DropColumn("dbo.PlayerRoundStat", "CleanSheet");
        }
    }
}
