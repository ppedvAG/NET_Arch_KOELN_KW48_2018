using ppedv.TastyMoon.DomainModel;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ppedv.TastyMoon.Data.EF
{
    public class EfContext : DbContext
    {
        public DbSet<Rezept> Rezepte { get; set; }
        public DbSet<KaffeeMaschinenTyp> KaffeeMaschinenTypen { get; set; }

        public EfContext(string conString) : base(conString)
        {
            //nicht mehr DB erstellen
          //  Database.SetInitializer(new NullDatabaseInitializer<EfContext>());
        }

        public EfContext() : this("Server=.;Database=TastyMoon_Dev;Trusted_Connection=true;")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //System.Data.Entity.ModelConfiguration.Conventions
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();


            modelBuilder.Entity<KaffeeMaschinenTyp>()
                        .Property(x => x.Hersteller)
                        .HasColumnName("ManufacturerName")
                        .HasMaxLength(36)
                        .IsRequired();

        }
    }
}
