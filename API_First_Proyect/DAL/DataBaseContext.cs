using API_First_Proyect.DAL.Entities;
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
               // Evitar dublicados en "Countries".
               base.OnModelCreating(modelBuilder);
               modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique();

               // Evita duplicados si el "Country" ya tiene un "State" con ese nombre.
               base.OnModelCreating(modelBuilder);
               modelBuilder.Entity<State>().HasIndex("Name", "CountryId").IsUnique();
          }

          public DbSet<Country> Countries { get; set; } // Tabla "Countries".
          public DbSet<State> States { get; set; } // Tabla "States".

     }
}
