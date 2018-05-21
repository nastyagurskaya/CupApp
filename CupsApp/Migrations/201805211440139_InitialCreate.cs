namespace CupsApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        CountryName = c.String(),
                    })
                .PrimaryKey(t => t.CountryId);
            
            CreateTable(
                "dbo.Cup",
                c => new
                    {
                        CupId = c.Int(nullable: false, identity: true),
                        Capacity = c.Double(nullable: false),
                        CupType = c.Int(),
                        CountryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CupId)
                .ForeignKey("dbo.Country", t => t.CountryID, cascadeDelete: true)
                .Index(t => t.CountryID);
            
            CreateTable(
                "dbo.CupImage",
                c => new
                    {
                        CupImageID = c.Int(nullable: false),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.CupImageID)
                .ForeignKey("dbo.Cup", t => t.CupImageID)
                .Index(t => t.CupImageID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CupImage", "CupImageID", "dbo.Cup");
            DropForeignKey("dbo.Cup", "CountryID", "dbo.Country");
            DropIndex("dbo.CupImage", new[] { "CupImageID" });
            DropIndex("dbo.Cup", new[] { "CountryID" });
            DropTable("dbo.CupImage");
            DropTable("dbo.Cup");
            DropTable("dbo.Country");
        }
    }
}
