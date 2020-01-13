using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using KsiazkaKucharska.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace KsiazkaKucharska.context
{
    public class KsiazkaContext : DbContext
    {
        public KsiazkaContext() : base("KsiazkaKucharska")
        {

        }

        static KsiazkaContext()
        {
            Database.SetInitializer<KsiazkaContext>(new KsiazkakucharskaInitializer());
        }



        public DbSet<Comment> Komentarze { get; set; }
        public DbSet<Danieglowne> Dania { get; set; }
        public DbSet<Przepis> Przepisy { get; set; }
        public DbSet<Zupa> Zupy { get; set; }





        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}