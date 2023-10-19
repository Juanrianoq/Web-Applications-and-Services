using API_First_Proyect.DAL.Entitites;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace API_First_Proyect.Domain.Interfaces
{
      public interface ICountryService
      {
          Task<IEnumerable<Country>> GetCountriesAsync();
          Task<Country> CreateCountryAsync(Country country);
          Task<Country> GetCountryByIdAsync(Guid id);
          Task<Country> GetCountryByNameAsync(string name);
          Task<Country> EditCountryAsync(Country country);
          Task<Country> DeleteCountryAsync(Guid id);
      }
}
