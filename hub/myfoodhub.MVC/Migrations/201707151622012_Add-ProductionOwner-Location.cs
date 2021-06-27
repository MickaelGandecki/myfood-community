namespace myfoodapp.Hub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductionOwnerLocation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductionUnitOwners", "location", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductionUnitOwners", "location");
        }
    }
}
