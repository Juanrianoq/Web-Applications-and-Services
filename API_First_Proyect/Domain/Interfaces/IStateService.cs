using API_First_Proyect.DAL.Entities;
using API_First_Proyect.DAL.Entitites;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace API_First_Proyect.Domain.Interfaces
{
      public interface IStateService
      {
          Task<IEnumerable<State>> GetStatesByCountryIdAsync(Guid countryId);
          Task<State> CreateStateAsync(State state, Guid countryId);
          Task<State> GetStateByIdAsync(Guid id);
          Task<State> EditStateAsync(State state, Guid id);
          Task<State> DeleteStateAsync(Guid id);
      }
}
