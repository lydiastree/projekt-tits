namespace KsiazkaKucharska.Migrations
{
    using context;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<KsiazkaKucharska.context.KsiazkaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "KsiazkaKucharska.context.KsiazkaContext";
        }

        protected override void Seed(KsiazkaKucharska.context.KsiazkaContext context)
        {
            KsiazkakucharskaInitializer.SeedKsiazkaData(context);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
