namespace testfan2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class player2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Player", "GoalWeight", c => c.Double(nullable: false));
            AddColumn("dbo.Player", "Fixture_FixtureId", c => c.Int());
            AddColumn("dbo.Player", "Fixture_FixtureId1", c => c.Int());
            AddColumn("dbo.Player", "Fixture_FixtureId2", c => c.Int());
            AddColumn("dbo.Player", "Fixture_FixtureId3", c => c.Int());
            CreateIndex("dbo.Player", "Fixture_FixtureId");
            CreateIndex("dbo.Player", "Fixture_FixtureId1");
            CreateIndex("dbo.Player", "Fixture_FixtureId2");
            CreateIndex("dbo.Player", "Fixture_FixtureId3");
            AddForeignKey("dbo.Player", "Fixture_FixtureId", "dbo.Fixture", "FixtureId");
            AddForeignKey("dbo.Player", "Fixture_FixtureId1", "dbo.Fixture", "FixtureId");
            AddForeignKey("dbo.Player", "Fixture_FixtureId2", "dbo.Fixture", "FixtureId");
            AddForeignKey("dbo.Player", "Fixture_FixtureId3", "dbo.Fixture", "FixtureId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Player", "Fixture_FixtureId3", "dbo.Fixture");
            DropForeignKey("dbo.Player", "Fixture_FixtureId2", "dbo.Fixture");
            DropForeignKey("dbo.Player", "Fixture_FixtureId1", "dbo.Fixture");
            DropForeignKey("dbo.Player", "Fixture_FixtureId", "dbo.Fixture");
            DropIndex("dbo.Player", new[] { "Fixture_FixtureId3" });
            DropIndex("dbo.Player", new[] { "Fixture_FixtureId2" });
            DropIndex("dbo.Player", new[] { "Fixture_FixtureId1" });
            DropIndex("dbo.Player", new[] { "Fixture_FixtureId" });
            DropColumn("dbo.Player", "Fixture_FixtureId3");
            DropColumn("dbo.Player", "Fixture_FixtureId2");
            DropColumn("dbo.Player", "Fixture_FixtureId1");
            DropColumn("dbo.Player", "Fixture_FixtureId");
            DropColumn("dbo.Player", "GoalWeight");
        }
    }
}
