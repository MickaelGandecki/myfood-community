namespace myfoodapp.Hub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOffice365Field : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductionUnitOwners", "office365Account", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductionUnitOwners", "office365Account");
        }
    }
}
