namespace testfan2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v4 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PlayerFantasyTeam", newName: "FantasyTeamPlayer");
            RenameColumn(table: "dbo.FantasyTeamPlayer", name: "Player_PlayerID", newName: "PlayerID");
            RenameColumn(table: "dbo.FantasyTeamPlayer", name: "FantasyTeam_TeamID", newName: "TeamID");
            RenameIndex(table: "dbo.FantasyTeamPlayer", name: "IX_FantasyTeam_TeamID", newName: "IX_TeamID");
            RenameIndex(table: "dbo.FantasyTeamPlayer", name: "IX_Player_PlayerID", newName: "IX_PlayerID");
            DropPrimaryKey("dbo.FantasyTeamPlayer");
            AlterColumn("dbo.FantasyTeam", "TeamName", c => c.String(nullable: false, maxLength: 50));
            AddPrimaryKey("dbo.FantasyTeamPlayer", new[] { "TeamID", "PlayerID" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.FantasyTeamPlayer");
            AlterColumn("dbo.FantasyTeam", "TeamName", c => c.String(nullable: false, maxLength: 52));
            AddPrimaryKey("dbo.FantasyTeamPlayer", new[] { "Player_PlayerID", "FantasyTeam_TeamID" });
            RenameIndex(table: "dbo.FantasyTeamPlayer", name: "IX_PlayerID", newName: "IX_Player_PlayerID");
            RenameIndex(table: "dbo.FantasyTeamPlayer", name: "IX_TeamID", newName: "IX_FantasyTeam_TeamID");
            RenameColumn(table: "dbo.FantasyTeamPlayer", name: "TeamID", newName: "FantasyTeam_TeamID");
            RenameColumn(table: "dbo.FantasyTeamPlayer", name: "PlayerID", newName: "Player_PlayerID");
            RenameTable(name: "dbo.FantasyTeamPlayer", newName: "PlayerFantasyTeam");
        }
    }
}
