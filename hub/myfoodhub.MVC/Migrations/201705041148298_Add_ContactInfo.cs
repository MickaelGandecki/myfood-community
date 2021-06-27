namespace myfoodapp.Hub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ContactInfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PreferedMoments",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        name = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ProductionUnitOwners", "contactMail", c => c.String());
            AddColumn("dbo.ProductionUnitOwners", "phoneNumber", c => c.String());
            AddColumn("dbo.ProductionUnitOwners", "preferedMoment_Id", c => c.Int());
            CreateIndex("dbo.ProductionUnitOwners", "preferedMoment_Id");
            AddForeignKey("dbo.ProductionUnitOwners", "preferedMoment_Id", "dbo.PreferedMoments", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductionUnitOwners", "preferedMoment_Id", "dbo.PreferedMoments");
            DropIndex("dbo.ProductionUnitOwners", new[] { "preferedMoment_Id" });
            DropColumn("dbo.ProductionUnitOwners", "preferedMoment_Id");
            DropColumn("dbo.ProductionUnitOwners", "phoneNumber");
            DropColumn("dbo.ProductionUnitOwners", "contactMail");
            DropTable("dbo.PreferedMoments");
        }
    }
}
