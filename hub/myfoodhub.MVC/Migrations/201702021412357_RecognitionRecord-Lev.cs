namespace myfoodapp.Hub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecognitionRecordLev : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HealthLevels",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        name = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductionLevels",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        name = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RecognitionRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        date = c.DateTime(nullable: false),
                        imagePath = c.String(nullable: false),
                        token = c.String(nullable: false),
                        dailyProduction = c.Double(nullable: false),
                        healthLevel_Id = c.Int(nullable: false),
                        productionLevel_Id = c.Int(nullable: false),
                        productionUnit_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HealthLevels", t => t.healthLevel_Id, cascadeDelete: true)
                .ForeignKey("dbo.ProductionLevels", t => t.productionLevel_Id, cascadeDelete: true)
                .ForeignKey("dbo.ProductionUnits", t => t.productionUnit_Id, cascadeDelete: true)
                .Index(t => t.healthLevel_Id)
                .Index(t => t.productionLevel_Id)
                .Index(t => t.productionUnit_Id);
            
            AddColumn("dbo.Options", "imagePath", c => c.String());
            AddColumn("dbo.ProductionUnitTypes", "imagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RecognitionRecords", "productionUnit_Id", "dbo.ProductionUnits");
            DropForeignKey("dbo.RecognitionRecords", "productionLevel_Id", "dbo.ProductionLevels");
            DropForeignKey("dbo.RecognitionRecords", "healthLevel_Id", "dbo.HealthLevels");
            DropIndex("dbo.RecognitionRecords", new[] { "productionUnit_Id" });
            DropIndex("dbo.RecognitionRecords", new[] { "productionLevel_Id" });
            DropIndex("dbo.RecognitionRecords", new[] { "healthLevel_Id" });
            DropColumn("dbo.ProductionUnitTypes", "imagePath");
            DropColumn("dbo.Options", "imagePath");
            DropTable("dbo.RecognitionRecords");
            DropTable("dbo.ProductionLevels");
            DropTable("dbo.HealthLevels");
        }
    }
}
