namespace myfoodapp.Hub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEventTypesPicture : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EventTypes", "svgPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EventTypes", "svgPath");
        }
    }
}
