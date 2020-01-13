namespace KsiazkaKucharska.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Danieglowne : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Danieglowne", "Ktododal", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Danieglowne", "Ktododal", c => c.String(nullable: false));
        }
    }
}
