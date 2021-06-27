namespace myfoodapp.Hub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_User_Language : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        name = c.String(),
                        description = c.String(),
                        imagePath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ProductionUnitOwners", "language_Id", c => c.Int());
            CreateIndex("dbo.ProductionUnitOwners", "language_Id");
            AddForeignKey("dbo.ProductionUnitOwners", "language_Id", "dbo.Languages", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductionUnitOwners", "language_Id", "dbo.Languages");
            DropIndex("dbo.ProductionUnitOwners", new[] { "language_Id" });
            DropColumn("dbo.ProductionUnitOwners", "language_Id");
            DropTable("dbo.Languages");
        }
    }
}
