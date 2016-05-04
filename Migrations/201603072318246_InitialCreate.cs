namespace testfan2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        CustomerSurname = c.String(nullable: false),
                        CustomerFirstName = c.String(nullable: false),
                        CustomerEmail = c.String(),
                        CustomerPhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            CreateTable(
                "dbo.FantasyLeague",
                c => new
                    {
                        FantasyLeagueId = c.Int(nullable: false, identity: true),
                        FantasyLeagueName = c.String(),
                    })
                .PrimaryKey(t => t.FantasyLeagueId);
            
            CreateTable(
                "dbo.FantasyTeam",
                c => new
                    {
                        TeamID = c.Int(nullable: false, identity: true),
                        TeamName = c.String(nullable: false, maxLength: 52),
                        CustomerId = c.Int(nullable: false),
                        FantasyLeagueID = c.Int(),
                        totalValue = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.TeamID)
                .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.FantasyLeague", t => t.FantasyLeagueID)
                .Index(t => t.CustomerId)
                .Index(t => t.FantasyLeagueID);
            
            CreateTable(
                "dbo.Player",
                c => new
                    {
                        PlayerID = c.Int(nullable: false),
                        PlayerSurname = c.String(nullable: false),
                        PlayerFirstname = c.String(nullable: false),
                        Position = c.Int(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        PlayerValue = c.Double(nullable: false),
                        NationCode = c.String(nullable: false, maxLength: 128),
                        FantasyTeam_TeamID = c.Int(),
                    })
                .PrimaryKey(t => t.PlayerID)
                .ForeignKey("dbo.NationTeam", t => t.NationCode, cascadeDelete: true)
                .ForeignKey("dbo.FantasyTeam", t => t.FantasyTeam_TeamID)
                .Index(t => t.NationCode)
                .Index(t => t.FantasyTeam_TeamID);
            
            CreateTable(
                "dbo.NationTeam",
                c => new
                    {
                        NationCode = c.String(nullable: false, maxLength: 128),
                        Nation = c.Int(nullable: false),
                        TeamGoalWeight = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NationCode);
            
            CreateTable(
                "dbo.Fixture",
                c => new
                    {
                        HomeTeamNationCode = c.String(maxLength: 128),
                        AwayTeamNationCode = c.String(maxLength: 128),
                        FixtureId = c.Int(nullable: false),
                        Venue = c.Int(nullable: false),
                        AwayTeamScore = c.Int(),
                        RoundStage = c.Int(nullable: false),
                        NationTeam_NationCode = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.FixtureId)
                .ForeignKey("dbo.NationTeam", t => t.AwayTeamNationCode)
                .ForeignKey("dbo.NationTeam", t => t.HomeTeamNationCode)
                .ForeignKey("dbo.NationTeam", t => t.NationTeam_NationCode)
                .Index(t => t.HomeTeamNationCode)
                .Index(t => t.AwayTeamNationCode)
                .Index(t => t.NationTeam_NationCode);
            
            CreateTable(
                "dbo.PlayerRoundStat",
                c => new
                    {
                        PlayerRoundStatID = c.Int(nullable: false, identity: true),
                        FixtureId = c.Int(),
                        PlayerID = c.Int(),
                        MinutesPlayed = c.Int(nullable: false),
                        OwnGoal = c.Int(nullable: false),
                        goalScored = c.Int(nullable: false),
                        YellowCarded = c.Boolean(nullable: false),
                        RedCarded = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PlayerRoundStatID)
                .ForeignKey("dbo.Fixture", t => t.FixtureId)
                .ForeignKey("dbo.Player", t => t.PlayerID)
                .Index(t => t.FixtureId)
                .Index(t => t.PlayerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlayerRoundStat", "PlayerID", "dbo.Player");
            DropForeignKey("dbo.PlayerRoundStat", "FixtureId", "dbo.Fixture");
            DropForeignKey("dbo.Player", "FantasyTeam_TeamID", "dbo.FantasyTeam");
            DropForeignKey("dbo.Player", "NationCode", "dbo.NationTeam");
            DropForeignKey("dbo.Fixture", "NationTeam_NationCode", "dbo.NationTeam");
            DropForeignKey("dbo.Fixture", "HomeTeamNationCode", "dbo.NationTeam");
            DropForeignKey("dbo.Fixture", "AwayTeamNationCode", "dbo.NationTeam");
            DropForeignKey("dbo.FantasyTeam", "FantasyLeagueID", "dbo.FantasyLeague");
            DropForeignKey("dbo.FantasyTeam", "CustomerId", "dbo.Customer");
            DropIndex("dbo.PlayerRoundStat", new[] { "PlayerID" });
            DropIndex("dbo.PlayerRoundStat", new[] { "FixtureId" });
            DropIndex("dbo.Fixture", new[] { "NationTeam_NationCode" });
            DropIndex("dbo.Fixture", new[] { "AwayTeamNationCode" });
            DropIndex("dbo.Fixture", new[] { "HomeTeamNationCode" });
            DropIndex("dbo.Player", new[] { "FantasyTeam_TeamID" });
            DropIndex("dbo.Player", new[] { "NationCode" });
            DropIndex("dbo.FantasyTeam", new[] { "FantasyLeagueID" });
            DropIndex("dbo.FantasyTeam", new[] { "CustomerId" });
            DropTable("dbo.PlayerRoundStat");
            DropTable("dbo.Fixture");
            DropTable("dbo.NationTeam");
            DropTable("dbo.Player");
            DropTable("dbo.FantasyTeam");
            DropTable("dbo.FantasyLeague");
            DropTable("dbo.Customer");
        }
    }
}
