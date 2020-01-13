namespace KsiazkaKucharska.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Danieglowne",
                c => new
                    {
                        DanieglowneID = c.Int(nullable: false, identity: true),
                        Nazwa = c.String(nullable: false),
                        Skladniki = c.String(nullable: false),
                        Wykonanie = c.String(nullable: false),
                        Iloscporcji = c.Int(nullable: false),
                        Ktododal = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.DanieglowneID);
            
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        CommentID = c.Int(nullable: false, identity: true),
                        DanieglowneID = c.Int(nullable: false),
                        ZupaID = c.Int(nullable: false),
                        PrzepisID = c.Int(nullable: false),
                        Nazwauzyt = c.String(nullable: false),
                        Komentarz = c.String(nullable: false),
                        Datadodania = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CommentID)
                .ForeignKey("dbo.Danieglowne", t => t.DanieglowneID, cascadeDelete: true)
                .ForeignKey("dbo.Przepis", t => t.PrzepisID, cascadeDelete: true)
                .ForeignKey("dbo.Zupa", t => t.ZupaID, cascadeDelete: true)
                .Index(t => t.DanieglowneID)
                .Index(t => t.ZupaID)
                .Index(t => t.PrzepisID);
            
            CreateTable(
                "dbo.Przepis",
                c => new
                    {
                        PrzepisID = c.Int(nullable: false, identity: true),
                        Nazwa = c.String(nullable: false),
                        Skladniki = c.String(nullable: false),
                        Wykonanie = c.String(nullable: false),
                        Kalorie = c.Int(nullable: false),
                        Ktododal = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PrzepisID);
            
            CreateTable(
                "dbo.Zupa",
                c => new
                    {
                        ZupaID = c.Int(nullable: false, identity: true),
                        Nazwa = c.String(nullable: false),
                        Skladniki = c.String(nullable: false),
                        Wykonanie = c.String(nullable: false),
                        Iloscporcji = c.Int(nullable: false),
                        Ktododal = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ZupaID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comment", "ZupaID", "dbo.Zupa");
            DropForeignKey("dbo.Comment", "PrzepisID", "dbo.Przepis");
            DropForeignKey("dbo.Comment", "DanieglowneID", "dbo.Danieglowne");
            DropIndex("dbo.Comment", new[] { "PrzepisID" });
            DropIndex("dbo.Comment", new[] { "ZupaID" });
            DropIndex("dbo.Comment", new[] { "DanieglowneID" });
            DropTable("dbo.Zupa");
            DropTable("dbo.Przepis");
            DropTable("dbo.Comment");
            DropTable("dbo.Danieglowne");
        }
    }
}
