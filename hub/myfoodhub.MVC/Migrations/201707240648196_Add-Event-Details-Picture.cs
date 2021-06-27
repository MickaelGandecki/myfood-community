namespace myfoodapp.Hub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEventDetailsPicture : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "details", c => c.String(unicode: false, storeType: "text"));
            AddColumn("dbo.Events", "picture", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "picture");
            DropColumn("dbo.Events", "details");
        }
    }
}
