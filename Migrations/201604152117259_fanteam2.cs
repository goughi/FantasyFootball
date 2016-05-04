namespace testfan2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fanteam2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FantasyTeams", "IsConfirmed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FantasyTeams", "IsConfirmed");
        }
    }
}
