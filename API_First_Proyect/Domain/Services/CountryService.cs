using API_First_Proyect.DAL;
using API_First_Proyect.DAL.Entitites;
using API_First_Proyect.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace API_First_Proyect.Domain.Services
{
     public class CountryService : ICountryService
     {
          private readonly DataBaseContext _context;

          public CountryService(DataBaseContext context) // Inyecto la dependencia, que me conecta con el contexto de la base de datos.
          {
               _context = context;
          }
          public async Task<IEnumerable<Country>> GetCountriesAsync()
          {
               return await _context.Countries.ToListAsync(); // Traer todos los datos que tengo en la tabla "Countries".
               
          }
          public async Task<Country> CreateCountryAsync(Country country)
          {
               try
               {
                    country.Id = Guid.NewGuid();
                    country.CreatedDate = DateTime.Now;
                    country.ModifiedDate = null;

                    _context.Countries.Add(country); // Guardando el objeto "country" en el contexto de mi BD.
                    await _context.SaveChangesAsync(); // Aquí estoy yendo a la BD para hacer el insert en la tabla Contries.

                    return country;
               }
               catch (DbUpdateException ex)
               {
                    // Esta excepción me captura un mensaje cuando el pais ya existe.
                    throw new Exception(ex.InnerException?.Message ?? ex.Message);
               }
          }
          public async Task<Country> GetCountryByIdAsync(Guid id)
          {
               return await _context.Countries.FirstOrDefaultAsync(c => c.Id == id); // Este me manda el elemento que tenga esa Id, pero si no existe, retorna un objeto vacio.
          }

          public async Task<Country> GetCountryByNameAsync(string name)
          {
               return await _context.Countries.FirstOrDefaultAsync(c => c.Name == name);
          }

          public async Task<Country> EditCountryAsync(Country country)
          {
               try
               {
                    country.ModifiedDate = DateTime.Now;

                    _context.Countries.Update(country); // Actualizo el objeto "country" en el contexto de mi BD.
                    await _context.SaveChangesAsync(); // Guardo los cambios realizados en el DbContext.

                    return country;
               }
               catch (DbUpdateException ex)
               {
                    // Esta excepción me captura un mensaje cuando el pais ya existe.
                    throw new Exception(ex.InnerException?.Message ?? ex.Message);
               }
          }

          public async Task<Country> DeleteCountryAsync(Guid id)
          {
               try
               {
                    // Aquí con el ID que traigo desde el controller, estoy recuperando el país que luego voy a eliminar.
                    var country = await _context.Countries.FirstOrDefaultAsync(c => c.Id == id);
                    if (country == null) return null; // Si el país no existe, entonces me retorna un NULL.

                    _context.Countries.Remove(country);
                    await _context.SaveChangesAsync();

                    return country;
               }
               catch (DbUpdateException ex)
               {
                    // Esta excepción me captura un mensaje cuando el pais ya existe.
                    throw new Exception(ex.InnerException?.Message ?? ex.Message);
               }
               
          }
     }
}
