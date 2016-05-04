namespace testfan2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customer", "CustomerSurname", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Customer", "CustomerFirstName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Customer", "CustomerEmail", c => c.String(nullable: false));
            AlterColumn("dbo.Customer", "CustomerPhoneNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customer", "CustomerPhoneNumber", c => c.String());
            AlterColumn("dbo.Customer", "CustomerEmail", c => c.String());
            AlterColumn("dbo.Customer", "CustomerFirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Customer", "CustomerSurname", c => c.String(nullable: false));
        }
    }
}
