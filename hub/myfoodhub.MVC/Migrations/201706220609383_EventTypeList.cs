namespace myfoodapp.Hub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventTypeList : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventTypeItems",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        name = c.String(),
                        description = c.String(),
                        isRestrictedForAdmin = c.Boolean(),
                        order = c.Int(nullable: false),
                        eventType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EventTypes", t => t.eventType_Id)
                .Index(t => t.eventType_Id);
            
            AddColumn("dbo.Events", "createdBy", c => c.String());
            AddColumn("dbo.Events", "isOpen", c => c.Boolean());
            AddColumn("dbo.EventTypes", "isDisplayedForUser", c => c.Boolean(nullable: false));
            AddColumn("dbo.EventTypes", "order", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventTypeItems", "eventType_Id", "dbo.EventTypes");
            DropIndex("dbo.EventTypeItems", new[] { "eventType_Id" });
            DropColumn("dbo.EventTypes", "order");
            DropColumn("dbo.EventTypes", "isDisplayedForUser");
            DropColumn("dbo.Events", "isOpen");
            DropColumn("dbo.Events", "createdBy");
            DropTable("dbo.EventTypeItems");
        }
    }
}
