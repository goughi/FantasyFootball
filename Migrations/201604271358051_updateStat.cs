namespace testfan2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateStat : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlayerRoundStats", "PlayerID", "dbo.Players");
            DropIndex("dbo.PlayerRoundStats", new[] { "PlayerID" });
            AlterColumn("dbo.PlayerRoundStats", "PlayerID", c => c.Int(nullable: false));
            CreateIndex("dbo.PlayerRoundStats", "PlayerID");
            AddForeignKey("dbo.PlayerRoundStats", "PlayerID", "dbo.Players", "PlayerID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlayerRoundStats", "PlayerID", "dbo.Players");
            DropIndex("dbo.PlayerRoundStats", new[] { "PlayerID" });
            AlterColumn("dbo.PlayerRoundStats", "PlayerID", c => c.Int());
            CreateIndex("dbo.PlayerRoundStats", "PlayerID");
            AddForeignKey("dbo.PlayerRoundStats", "PlayerID", "dbo.Players", "PlayerID");
        }
    }
}
