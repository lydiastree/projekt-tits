namespace KsiazkaKucharska.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Przepis : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Przepis", "Ktododal", c => c.String());
            AlterColumn("dbo.Zupa", "Ktododal", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Zupa", "Ktododal", c => c.String(nullable: false));
            AlterColumn("dbo.Przepis", "Ktododal", c => c.String(nullable: false));
        }
    }
}
