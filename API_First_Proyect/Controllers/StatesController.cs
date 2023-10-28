using API_First_Proyect.DAL.Entities;
using API_First_Proyect.DAL.Entitites;
using API_First_Proyect.Domain.Interfaces;
using API_First_Proyect.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_First_Proyect.Controllers
{
     [ApiController]
     [Route("api/[controller]")]
     public class StatesController : Controller
     {
          private readonly IStateService _stateService;

          public StatesController(IStateService stateService)
          {
               _stateService = stateService; 
          }


          [HttpGet, ActionName("GetAll")]
          [Route("GetAll")]
          public async Task<ActionResult<IEnumerable<State>>> GetStatesByCountryIdAsync(Guid countryId)
          {
               var states = await _stateService.GetStatesByCountryIdAsync(countryId); 
               if (states == null || !states.Any()) return NotFound();

               return Ok(states);
          }


          [HttpPost, ActionName("Create")]
          [Route("Create")]
          public async Task<ActionResult> CreateStateAsync(State state, Guid countryId)
          {
               try
               {
                    var createdCountry = await _stateService.CreateStateAsync(state, countryId);
                    if (createdCountry == null) return NotFound();

                    return Ok(createdCountry);
               }
               catch (Exception ex)
               {
                    if (ex.Message.Contains("duplicate"))
                    {
                         return Conflict(string.Format("El estado {0} ya existe.", state.Name));
                    }
                    return Conflict(ex.Message);
               }
          }


          [HttpGet, ActionName("GetById")]
          [Route("GetById/{id}")]
          public async Task<ActionResult<State>> GetStateByIdAsync(Guid id)
          {
               if (id == null) return BadRequest("Id es un valor obligatorio");

               var state = await _stateService.GetStateByIdAsync(id); // Aquí estoy yendo a mi capa de "Domain" para traer la lista de paises.
               if (state == null) return NotFound();
               return Ok(state);
          }


          [HttpPut, ActionName("Edit")]
          [Route("Edit")]
          public async Task<ActionResult<State>> EditStateAsync(State state, Guid id)
          {
               try
               {
                    var editedState = await _stateService.EditStateAsync(state, id);
                    return Ok(editedState);
               }
               catch (Exception ex)
               {
                    if (ex.Message.Contains("duplicate"))
                         return Conflict(string.Format("{0} ya existe.", state.Name));

                    return Conflict(ex.Message);
               }
          }


          [HttpPut, ActionName("Delete")]
          [Route("Delete")]
          public async Task<ActionResult<State>> DeleteStateAsync(Guid id)
          {
               if (id == null) return BadRequest("El campo ID es obligatorio!");

               var deletedState = await _stateService.DeleteStateAsync(id);
               if (deletedState == null) return NotFound("Estado no encontrado!");

               return Ok(deletedState);
          }
     }
}
