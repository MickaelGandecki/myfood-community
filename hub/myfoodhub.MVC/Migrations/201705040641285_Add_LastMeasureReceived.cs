namespace myfoodapp.Hub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_LastMeasureReceived : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductionUnits", "lastMeasureReceived", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductionUnits", "lastMeasureReceived");
        }
    }
}
