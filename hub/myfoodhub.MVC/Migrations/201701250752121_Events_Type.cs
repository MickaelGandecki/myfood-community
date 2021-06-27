namespace myfoodapp.Hub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Events_Type : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventTypes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        name = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Events", "eventType_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Events", "eventType_Id");
            AddForeignKey("dbo.Events", "eventType_Id", "dbo.EventTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "eventType_Id", "dbo.EventTypes");
            DropIndex("dbo.Events", new[] { "eventType_Id" });
            DropColumn("dbo.Events", "eventType_Id");
            DropTable("dbo.EventTypes");
        }
    }
}
