using API_First_Proyect.DAL.Entities;
using API_First_Proyect.DAL.Entitites;

namespace API_First_Proyect.DAL
{
     public class SeederDB
     {
          private readonly DataBaseContext _context;

          public SeederDB(DataBaseContext context)
          {
               _context = context;
          }

          // Creamos el método "SeederAsync" (Funciona como un método MAIN para inicializar las tablas de la BD)
          public async Task SeederAsync()
          {
               // Agregar método propio de EF que hace la función del comando "update-database".
               await _context.Database.EnsureCreatedAsync();

               await PopulateCountriesAsync();
               await _context.SaveChangesAsync();
          }

          #region "Private Methods"

          private async Task PopulateCountriesAsync()
          {
               if (!_context.Countries.Any())
               {
                    _context.Countries.Add(new Country
                    {
                         CreatedDate = DateTime.Now,
                         Name = "Colombia",
                         States = new List<State>()
                         {
                              new State
                              {
                                   CreatedDate = DateTime.Now,
                                   Name = "Antioquia"
                              },

                              new State
                              {
                                   CreatedDate = DateTime.Now,
                                   Name = "Cundinamarca"
                              }
                         }
                    });

                    _context.Countries.Add(new Country
                    {
                         CreatedDate = DateTime.Now,
                         Name = "Argentina",
                         States = new List<State>()
                         {
                              new State
                              {
                                   CreatedDate = DateTime.Now,
                                   Name = "Buenos Aires"
                              },
                         }
                    });
               }
          }

          #endregion
     }
}
