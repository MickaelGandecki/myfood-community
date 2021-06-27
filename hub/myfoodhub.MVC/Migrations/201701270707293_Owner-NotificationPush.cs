namespace myfoodapp.Hub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OwnerNotificationPush : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductionUnitOwners", "notificationPushKey", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductionUnitOwners", "notificationPushKey");
        }
    }
}
