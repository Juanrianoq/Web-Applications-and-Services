using API_First_Proyect.DAL;
using API_First_Proyect.DAL.Entities;
using API_First_Proyect.DAL.Entitites;
using API_First_Proyect.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace API_First_Proyect.Domain.Services
{
     public class StateService : IStateService
     {
          private readonly DataBaseContext _context;

          public StateService(DataBaseContext context) // Inyecto la dependencia, que me conecta con el contexto de la base de datos.
          {
               _context = context;
          }
          public async Task<IEnumerable<State>> GetStatesByCountryIdAsync(Guid countryId)
          {
               return await _context.States
                    .Where(s => s.CountryId == countryId)
                    .ToListAsync();
          }
          public async Task<State> CreateStateAsync(State state, Guid countryId)
          {
               try
               {
                    state.Id = Guid.NewGuid();
                    state.CreatedDate = DateTime.Now;
                    state.CountryId = countryId;
                    state.Country = await _context.Countries.FirstOrDefaultAsync(c => c.Id == countryId);
                    state.ModifiedDate = null;

                    _context.States.Add(state);
                    await _context.SaveChangesAsync(); 

                    return state;
               }
               catch (DbUpdateException ex)
               {
                    // Esta excepción me captura un mensaje cuando el pais ya existe.
                    throw new Exception(ex.InnerException?.Message ?? ex.Message);
               }
          }
          public async Task<State> GetStateByIdAsync(Guid id)
          {
               return await _context.States.FirstOrDefaultAsync(s => s.Id == id);
          }

          public async Task<State> EditStateAsync(State state, Guid id)
          {
               try
               {
                    state.ModifiedDate = DateTime.Now;

                    _context.States.Update(state); 
                    await _context.SaveChangesAsync(); 

                    return state;
               }
               catch (DbUpdateException ex)
               {
                    // Esta excepción me captura un mensaje cuando el pais ya existe.
                    throw new Exception(ex.InnerException?.Message ?? ex.Message);
               }
          }

          public async Task<State> DeleteStateAsync(Guid id)
          {
               try
               {
                    var state = await _context.States.FirstOrDefaultAsync(s => s.Id == id);
                    if (state == null) return null; // Si el país no existe, entonces me retorna un NULL.

                    _context.States.Remove(state);
                    await _context.SaveChangesAsync();

                    return state;
               }
               catch (DbUpdateException ex)
               {
                    // Esta excepción me captura un mensaje cuando el pais ya existe.
                    throw new Exception(ex.InnerException?.Message ?? ex.Message);
               }
               
          }
     }
}
