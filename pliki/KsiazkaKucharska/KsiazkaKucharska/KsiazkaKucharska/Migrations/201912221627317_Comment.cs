namespace KsiazkaKucharska.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Comment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comment", "DanieglowneID", "dbo.Danieglowne");
            DropForeignKey("dbo.Comment", "PrzepisID", "dbo.Przepis");
            DropForeignKey("dbo.Comment", "ZupaID", "dbo.Zupa");
            DropIndex("dbo.Comment", new[] { "DanieglowneID" });
            DropIndex("dbo.Comment", new[] { "ZupaID" });
            DropIndex("dbo.Comment", new[] { "PrzepisID" });
            AlterColumn("dbo.Comment", "DanieglowneID", c => c.Int(nullable: true));
            AlterColumn("dbo.Comment", "ZupaID", c => c.Int(nullable: true));
            AlterColumn("dbo.Comment", "PrzepisID", c => c.Int(nullable: true));
            AlterColumn("dbo.Comment", "Datadodania", c => c.DateTime(nullable: true));
            CreateIndex("dbo.Comment", "DanieglowneID");
            CreateIndex("dbo.Comment", "ZupaID");
            CreateIndex("dbo.Comment", "PrzepisID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comment", "ZupaID", "dbo.Zupa");
            DropForeignKey("dbo.Comment", "PrzepisID", "dbo.Przepis");
            DropForeignKey("dbo.Comment", "DanieglowneID", "dbo.Danieglowne");
            DropIndex("dbo.Comment", new[] { "PrzepisID" });
            DropIndex("dbo.Comment", new[] { "ZupaID" });
            DropIndex("dbo.Comment", new[] { "DanieglowneID" });
            AlterColumn("dbo.Comment", "Datadodania", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Comment", "PrzepisID", c => c.Int(nullable: false));
            AlterColumn("dbo.Comment", "ZupaID", c => c.Int(nullable: false));
            AlterColumn("dbo.Comment", "DanieglowneID", c => c.Int(nullable: false));
            CreateIndex("dbo.Comment", "PrzepisID");
            CreateIndex("dbo.Comment", "ZupaID");
            CreateIndex("dbo.Comment", "DanieglowneID");
            AddForeignKey("dbo.Comment", "ZupaID", "dbo.Zupa", "ZupaID", cascadeDelete: true);
            AddForeignKey("dbo.Comment", "PrzepisID", "dbo.Przepis", "PrzepisID", cascadeDelete: true);
            AddForeignKey("dbo.Comment", "DanieglowneID", "dbo.Danieglowne", "DanieglowneID", cascadeDelete: true);
        }
    }
}
