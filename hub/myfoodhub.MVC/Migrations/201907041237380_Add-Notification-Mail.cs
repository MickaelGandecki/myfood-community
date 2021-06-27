namespace myfoodapp.Hub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotificationMail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductionUnitOwners", "isMailNotificationActivated", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductionUnitOwners", "isMailNotificationActivated");
        }
    }
}
