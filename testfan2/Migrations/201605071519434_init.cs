namespace testfan2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FantasyLeagues",
                c => new
                    {
                        FantasyLeagueId = c.Int(nullable: false, identity: true),
                        FantasyLeagueName = c.String(),
                    })
                .PrimaryKey(t => t.FantasyLeagueId);
            
            CreateTable(
                "dbo.FantasyTeams",
                c => new
                    {
                        UserId = c.String(maxLength: 128),
                        FantasyLeagueID = c.Int(nullable: false),
                        TeamID = c.Int(nullable: false, identity: true),
                        TeamName = c.String(nullable: false, maxLength: 50),
                        IsConfirmed = c.Boolean(nullable: false),
                        FirstRoundScore = c.Int(nullable: false),
                        SecondRoundScore = c.Int(nullable: false),
                        totalValue = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.TeamID)
                .ForeignKey("dbo.FantasyLeagues", t => t.FantasyLeagueID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.FantasyLeagueID);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        PlayerID = c.Int(nullable: false),
                        PlayerSurname = c.String(nullable: false),
                        PlayerFirstname = c.String(nullable: false),
                        Position = c.Int(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        PlayerValue = c.Double(nullable: false),
                        GoalWeight = c.Double(nullable: false),
                        NationCode = c.String(nullable: false, maxLength: 128),
                        Fixture_FixtureId = c.Int(),
                        Fixture_FixtureId1 = c.Int(),
                        Fixture_FixtureId2 = c.Int(),
                        Fixture_FixtureId3 = c.Int(),
                    })
                .PrimaryKey(t => t.PlayerID)
                .ForeignKey("dbo.Fixtures", t => t.Fixture_FixtureId)
                .ForeignKey("dbo.Fixtures", t => t.Fixture_FixtureId1)
                .ForeignKey("dbo.Fixtures", t => t.Fixture_FixtureId2)
                .ForeignKey("dbo.Fixtures", t => t.Fixture_FixtureId3)
                .ForeignKey("dbo.NationTeams", t => t.NationCode, cascadeDelete: true)
                .Index(t => t.NationCode)
                .Index(t => t.Fixture_FixtureId)
                .Index(t => t.Fixture_FixtureId1)
                .Index(t => t.Fixture_FixtureId2)
                .Index(t => t.Fixture_FixtureId3);
            
            CreateTable(
                "dbo.NationTeams",
                c => new
                    {
                        NationCode = c.String(nullable: false, maxLength: 128),
                        Nation = c.Int(nullable: false),
                        TeamGoalWeight = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NationCode);
            
            CreateTable(
                "dbo.Fixtures",
                c => new
                    {
                        HomeTeamNationCode = c.String(nullable: false, maxLength: 128),
                        AwayTeamNationCode = c.String(nullable: false, maxLength: 128),
                        FixtureId = c.Int(nullable: false),
                        Venue = c.Int(nullable: false),
                        RoundStage = c.Int(nullable: false),
                        HomeTeamScore = c.Int(nullable: false),
                        AwayTeamScore = c.Int(nullable: false),
                        gamePlayed = c.Boolean(nullable: false),
                        NationTeam_NationCode = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.FixtureId)
                .ForeignKey("dbo.NationTeams", t => t.AwayTeamNationCode, cascadeDelete: false)
                .ForeignKey("dbo.NationTeams", t => t.HomeTeamNationCode, cascadeDelete: false)
                .ForeignKey("dbo.NationTeams", t => t.NationTeam_NationCode)
                .Index(t => t.HomeTeamNationCode)
                .Index(t => t.AwayTeamNationCode)
                .Index(t => t.NationTeam_NationCode);
            
            CreateTable(
                "dbo.PlayerRoundStats",
                c => new
                    {
                        PlayerRoundStatID = c.Int(nullable: false, identity: true),
                        FixtureId = c.Int(),
                        PlayerID = c.Int(nullable: false),
                        MinutesPlayed = c.Int(nullable: false),
                        CleanSheet = c.Boolean(nullable: false),
                        GoalsConceded = c.Int(nullable: false),
                        goalScored = c.Int(nullable: false),
                        YellowCarded = c.Boolean(nullable: false),
                        RedCarded = c.Boolean(nullable: false),
                        IsWin = c.Boolean(nullable: false),
                        ManOfTheMatch = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PlayerRoundStatID)
                .ForeignKey("dbo.Fixtures", t => t.FixtureId)
                .ForeignKey("dbo.Players", t => t.PlayerID, cascadeDelete: true)
                .Index(t => t.FixtureId)
                .Index(t => t.PlayerID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CustomerSurname = c.String(nullable: false, maxLength: 50),
                        CustomerFirstName = c.String(nullable: false, maxLength: 50),
                        SupportingNation = c.Int(nullable: false),
                        BirthDate = c.DateTime(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.FantasyTeamPlayer",
                c => new
                    {
                        TeamID = c.Int(nullable: false),
                        PlayerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TeamID, t.PlayerID })
                .ForeignKey("dbo.FantasyTeams", t => t.TeamID, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.PlayerID, cascadeDelete: true)
                .Index(t => t.TeamID)
                .Index(t => t.PlayerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.FantasyTeams", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FantasyTeamPlayer", "PlayerID", "dbo.Players");
            DropForeignKey("dbo.FantasyTeamPlayer", "TeamID", "dbo.FantasyTeams");
            DropForeignKey("dbo.Players", "NationCode", "dbo.NationTeams");
            DropForeignKey("dbo.Fixtures", "NationTeam_NationCode", "dbo.NationTeams");
            DropForeignKey("dbo.Players", "Fixture_FixtureId3", "dbo.Fixtures");
            DropForeignKey("dbo.Players", "Fixture_FixtureId2", "dbo.Fixtures");
            DropForeignKey("dbo.PlayerRoundStats", "PlayerID", "dbo.Players");
            DropForeignKey("dbo.PlayerRoundStats", "FixtureId", "dbo.Fixtures");
            DropForeignKey("dbo.Players", "Fixture_FixtureId1", "dbo.Fixtures");
            DropForeignKey("dbo.Fixtures", "HomeTeamNationCode", "dbo.NationTeams");
            DropForeignKey("dbo.Players", "Fixture_FixtureId", "dbo.Fixtures");
            DropForeignKey("dbo.Fixtures", "AwayTeamNationCode", "dbo.NationTeams");
            DropForeignKey("dbo.FantasyTeams", "FantasyLeagueID", "dbo.FantasyLeagues");
            DropIndex("dbo.FantasyTeamPlayer", new[] { "PlayerID" });
            DropIndex("dbo.FantasyTeamPlayer", new[] { "TeamID" });
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.PlayerRoundStats", new[] { "PlayerID" });
            DropIndex("dbo.PlayerRoundStats", new[] { "FixtureId" });
            DropIndex("dbo.Fixtures", new[] { "NationTeam_NationCode" });
            DropIndex("dbo.Fixtures", new[] { "AwayTeamNationCode" });
            DropIndex("dbo.Fixtures", new[] { "HomeTeamNationCode" });
            DropIndex("dbo.Players", new[] { "Fixture_FixtureId3" });
            DropIndex("dbo.Players", new[] { "Fixture_FixtureId2" });
            DropIndex("dbo.Players", new[] { "Fixture_FixtureId1" });
            DropIndex("dbo.Players", new[] { "Fixture_FixtureId" });
            DropIndex("dbo.Players", new[] { "NationCode" });
            DropIndex("dbo.FantasyTeams", new[] { "FantasyLeagueID" });
            DropIndex("dbo.FantasyTeams", new[] { "UserId" });
            DropTable("dbo.FantasyTeamPlayer");
            DropTable("dbo.Roles");
            DropTable("dbo.UserRoles");
            DropTable("dbo.UserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.PlayerRoundStats");
            DropTable("dbo.Fixtures");
            DropTable("dbo.NationTeams");
            DropTable("dbo.Players");
            DropTable("dbo.FantasyTeams");
            DropTable("dbo.FantasyLeagues");
        }
    }
}
