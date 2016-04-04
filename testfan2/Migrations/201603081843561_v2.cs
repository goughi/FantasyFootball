namespace testfan2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Player", "FantasyTeam_TeamID", "dbo.FantasyTeam");
            DropIndex("dbo.Player", new[] { "FantasyTeam_TeamID" });
            CreateTable(
                "dbo.PlayerFantasyTeam",
                c => new
                    {
                        Player_PlayerID = c.Int(nullable: false),
                        FantasyTeam_TeamID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Player_PlayerID, t.FantasyTeam_TeamID })
                .ForeignKey("dbo.Player", t => t.Player_PlayerID, cascadeDelete: true)
                .ForeignKey("dbo.FantasyTeam", t => t.FantasyTeam_TeamID, cascadeDelete: true)
                .Index(t => t.Player_PlayerID)
                .Index(t => t.FantasyTeam_TeamID);
            
            DropColumn("dbo.Player", "FantasyTeam_TeamID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Player", "FantasyTeam_TeamID", c => c.Int());
            DropForeignKey("dbo.PlayerFantasyTeam", "FantasyTeam_TeamID", "dbo.FantasyTeam");
            DropForeignKey("dbo.PlayerFantasyTeam", "Player_PlayerID", "dbo.Player");
            DropIndex("dbo.PlayerFantasyTeam", new[] { "FantasyTeam_TeamID" });
            DropIndex("dbo.PlayerFantasyTeam", new[] { "Player_PlayerID" });
            DropTable("dbo.PlayerFantasyTeam");
            CreateIndex("dbo.Player", "FantasyTeam_TeamID");
            AddForeignKey("dbo.Player", "FantasyTeam_TeamID", "dbo.FantasyTeam", "TeamID");
        }
    }
}
