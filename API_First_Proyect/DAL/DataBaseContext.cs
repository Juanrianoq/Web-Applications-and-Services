using API_First_Proyect.DAL.Entitites;
using Microsoft.EntityFrameworkCore;

namespace API_First_Proyect.DAL
{
      public class DataBaseContext : DbContext
      {
            // Este constructor sirve para conectarse a la BD, con unas opciones de configuración
            // Que ya están definidas internamente.
            public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
            {

            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                  base.OnModelCreating(modelBuilder);
                  modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique();
            }

            public DbSet<Country> Countries { get; set; } // Esta linea toma la clase "Country" y la convierte en una tabla llamada Countries.

      }
}
