namespace myfoodapp.Hub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        date = c.DateTime(nullable: false),
                        description = c.String(nullable: false),
                        productionUnit_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductionUnits", t => t.productionUnit_Id, cascadeDelete: true)
                .Index(t => t.productionUnit_Id);
            
            CreateTable(
                "dbo.ProductionUnits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        startDate = c.DateTime(nullable: false),
                        reference = c.String(nullable: false),
                        locationLatitude = c.Double(nullable: false),
                        locationLongitude = c.Double(nullable: false),
                        info = c.String(),
                        version = c.String(),
                        picturePath = c.String(),
                        hydroponicType_Id = c.Int(nullable: false),
                        owner_Id = c.Int(nullable: false),
                        productionUnitStatus_Id = c.Int(nullable: false),
                        productionUnitType_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HydroponicTypes", t => t.hydroponicType_Id, cascadeDelete: true)
                .ForeignKey("dbo.ProductionUnitOwners", t => t.owner_Id, cascadeDelete: true)
                .ForeignKey("dbo.ProductionUnitStatus", t => t.productionUnitStatus_Id, cascadeDelete: true)
                .ForeignKey("dbo.ProductionUnitTypes", t => t.productionUnitType_Id, cascadeDelete: true)
                .Index(t => t.hydroponicType_Id)
                .Index(t => t.owner_Id)
                .Index(t => t.productionUnitStatus_Id)
                .Index(t => t.productionUnitType_Id);
            
            CreateTable(
                "dbo.HydroponicTypes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        name = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Measures",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        captureDate = c.DateTime(nullable: false),
                        value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        productionUnit_Id = c.Int(nullable: false),
                        sensor_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductionUnits", t => t.productionUnit_Id, cascadeDelete: true)
                .ForeignKey("dbo.SensorTypes", t => t.sensor_Id, cascadeDelete: true)
                .Index(t => t.productionUnit_Id)
                .Index(t => t.sensor_Id);
            
            CreateTable(
                "dbo.SensorTypes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        name = c.String(nullable: false),
                        description = c.String(),
                        lastCalibration = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Options",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        name = c.String(),
                        description = c.String(),
                        ProductionUnit_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductionUnits", t => t.ProductionUnit_Id)
                .Index(t => t.ProductionUnit_Id);
            
            CreateTable(
                "dbo.ProductionUnitOwners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        pioneerCitizenNumber = c.Int(nullable: false),
                        pioneerCitizenName = c.String(),
                        user_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.user_Id)
                .Index(t => t.user_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ProductionUnitStatus",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        name = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductionUnitTypes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        name = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GardeningTypes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        name = c.String(nullable: false),
                        description = c.String(),
                        imagePath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        date = c.DateTime(nullable: false),
                        type = c.String(),
                        description = c.String(),
                        stackCall = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        date = c.DateTime(nullable: false),
                        content = c.String(nullable: false),
                        device = c.String(nullable: false),
                        info = c.String(),
                        messageType_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MessageTypes", t => t.messageType_Id, cascadeDelete: true)
                .Index(t => t.messageType_Id);
            
            CreateTable(
                "dbo.MessageTypes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        name = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Months",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        name = c.String(nullable: false),
                        order = c.Int(nullable: false),
                        season_Id = c.Int(nullable: false),
                        Recipes_Id = c.Long(),
                        Recipes_Id1 = c.Long(),
                        Recipes_Id2 = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Seasons", t => t.season_Id, cascadeDelete: true)
                .ForeignKey("dbo.Recipes", t => t.Recipes_Id)
                .ForeignKey("dbo.Recipes", t => t.Recipes_Id1)
                .ForeignKey("dbo.Recipes", t => t.Recipes_Id2)
                .Index(t => t.season_Id)
                .Index(t => t.Recipes_Id)
                .Index(t => t.Recipes_Id1)
                .Index(t => t.Recipes_Id2);
            
            CreateTable(
                "dbo.Seasons",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OptionLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        option_Id = c.Int(),
                        productionUnit_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Options", t => t.option_Id)
                .ForeignKey("dbo.ProductionUnits", t => t.productionUnit_Id)
                .Index(t => t.option_Id)
                .Index(t => t.productionUnit_Id);
            
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        description = c.String(nullable: false),
                        advice = c.String(),
                        daysOfGermination = c.Int(nullable: false),
                        daysOfHarvestFromSowing = c.Int(nullable: false),
                        minimumSpaceBetweenPlantInTower = c.Int(nullable: false),
                        proteinPercentage = c.Int(nullable: false),
                        idealMinTemperature = c.Decimal(nullable: false, precision: 18, scale: 2),
                        idealMaxTemperature = c.Decimal(nullable: false, precision: 18, scale: 2),
                        acceptableMaxTemperature = c.Decimal(nullable: false, precision: 18, scale: 2),
                        acceptableMinTemperature = c.Decimal(nullable: false, precision: 18, scale: 2),
                        reference = c.String(nullable: false),
                        picturePath = c.String(nullable: false),
                        gardeningType_Id = c.Int(nullable: false),
                        wateringLevel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GardeningTypes", t => t.gardeningType_Id, cascadeDelete: true)
                .ForeignKey("dbo.WateringLevels", t => t.wateringLevel_Id)
                .Index(t => t.gardeningType_Id)
                .Index(t => t.wateringLevel_Id);
            
            CreateTable(
                "dbo.WateringLevels",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        name = c.String(nullable: false),
                        description = c.String(),
                        imagePath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Recipes", "wateringLevel_Id", "dbo.WateringLevels");
            DropForeignKey("dbo.Months", "Recipes_Id2", "dbo.Recipes");
            DropForeignKey("dbo.Months", "Recipes_Id1", "dbo.Recipes");
            DropForeignKey("dbo.Months", "Recipes_Id", "dbo.Recipes");
            DropForeignKey("dbo.Recipes", "gardeningType_Id", "dbo.GardeningTypes");
            DropForeignKey("dbo.OptionLists", "productionUnit_Id", "dbo.ProductionUnits");
            DropForeignKey("dbo.OptionLists", "option_Id", "dbo.Options");
            DropForeignKey("dbo.Months", "season_Id", "dbo.Seasons");
            DropForeignKey("dbo.Messages", "messageType_Id", "dbo.MessageTypes");
            DropForeignKey("dbo.Events", "productionUnit_Id", "dbo.ProductionUnits");
            DropForeignKey("dbo.ProductionUnits", "productionUnitType_Id", "dbo.ProductionUnitTypes");
            DropForeignKey("dbo.ProductionUnits", "productionUnitStatus_Id", "dbo.ProductionUnitStatus");
            DropForeignKey("dbo.ProductionUnits", "owner_Id", "dbo.ProductionUnitOwners");
            DropForeignKey("dbo.ProductionUnitOwners", "user_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Options", "ProductionUnit_Id", "dbo.ProductionUnits");
            DropForeignKey("dbo.Measures", "sensor_Id", "dbo.SensorTypes");
            DropForeignKey("dbo.Measures", "productionUnit_Id", "dbo.ProductionUnits");
            DropForeignKey("dbo.ProductionUnits", "hydroponicType_Id", "dbo.HydroponicTypes");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Recipes", new[] { "wateringLevel_Id" });
            DropIndex("dbo.Recipes", new[] { "gardeningType_Id" });
            DropIndex("dbo.OptionLists", new[] { "productionUnit_Id" });
            DropIndex("dbo.OptionLists", new[] { "option_Id" });
            DropIndex("dbo.Months", new[] { "Recipes_Id2" });
            DropIndex("dbo.Months", new[] { "Recipes_Id1" });
            DropIndex("dbo.Months", new[] { "Recipes_Id" });
            DropIndex("dbo.Months", new[] { "season_Id" });
            DropIndex("dbo.Messages", new[] { "messageType_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.ProductionUnitOwners", new[] { "user_Id" });
            DropIndex("dbo.Options", new[] { "ProductionUnit_Id" });
            DropIndex("dbo.Measures", new[] { "sensor_Id" });
            DropIndex("dbo.Measures", new[] { "productionUnit_Id" });
            DropIndex("dbo.ProductionUnits", new[] { "productionUnitType_Id" });
            DropIndex("dbo.ProductionUnits", new[] { "productionUnitStatus_Id" });
            DropIndex("dbo.ProductionUnits", new[] { "owner_Id" });
            DropIndex("dbo.ProductionUnits", new[] { "hydroponicType_Id" });
            DropIndex("dbo.Events", new[] { "productionUnit_Id" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.WateringLevels");
            DropTable("dbo.Recipes");
            DropTable("dbo.OptionLists");
            DropTable("dbo.Seasons");
            DropTable("dbo.Months");
            DropTable("dbo.MessageTypes");
            DropTable("dbo.Messages");
            DropTable("dbo.Logs");
            DropTable("dbo.GardeningTypes");
            DropTable("dbo.ProductionUnitTypes");
            DropTable("dbo.ProductionUnitStatus");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.ProductionUnitOwners");
            DropTable("dbo.Options");
            DropTable("dbo.SensorTypes");
            DropTable("dbo.Measures");
            DropTable("dbo.HydroponicTypes");
            DropTable("dbo.ProductionUnits");
            DropTable("dbo.Events");
        }
    }
}
