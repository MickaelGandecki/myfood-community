namespace myfoodapp.Hub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFahrenheitSupport : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductionUnitOwners", "hasFahrenheitSetting", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductionUnitOwners", "hasFahrenheitSetting");
        }
    }
}
