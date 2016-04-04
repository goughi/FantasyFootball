namespace testfan2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixtureUpdate2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fixture", "gamePlayed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Fixture", "gamePlayed");
        }
    }
}
