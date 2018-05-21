using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using CupsApp.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CupsApp.Context
{
    public class CupContext : DbContext
    {
        public CupContext() : base("CupContext")
        {
            Database.SetInitializer<CupContext>(new CreateDatabaseIfNotExists<CupContext>());
        }
        public DbSet<Cup> Cups { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CupImage> CupImages { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Cup>()
               .HasOptional(s => s.CupImage)
               .WithRequired(ad => ad.Cup);
        }
    }
}