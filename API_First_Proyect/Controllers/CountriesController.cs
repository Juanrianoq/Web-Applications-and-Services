using API_First_Proyect.DAL.Entitites;
using API_First_Proyect.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_First_Proyect.Controllers
{
     [ApiController]
     [Route("api/[controller]")] // Esta es la primera parte de la URL de esta API.
                                 // URL = api/countries
     public class CountriesController : Controller
     {
          private readonly ICountryService _countryService;
          public CountriesController(ICountryService countryService)
          {
               _countryService = countryService;
          }

          // En un controlador, los métodos se llaman "Acciones" o Actions en ingles.
          // Todo Endpoint retorna un ActionResult, osea que retorna el resultado de una accion.

          [HttpGet, ActionName("GetAll")]
          [Route("GetAll")] // Aquí concateno la URL inicial, URL = api/countries/get
          public async Task<ActionResult<IEnumerable<Country>>> GetCountriesAsync()
          {
               var countries = await _countryService.GetCountriesAsync(); // Aquí estoy yendo a mi capa de "Domain" para traer la lista de paises.

               if (countries == null || !countries.Any())
               {
                    return NotFound(); // NotFound = 404 HTTP Status Code.
               }

               return Ok(countries); // Ok = 200 HTTP Status Code.
          }

          [HttpPost, ActionName("Create")]
          [Route("Create")]
          public async Task<ActionResult> CreateCountryAsync(Country country)
          {
               try
               {
                    var createdCountry = await _countryService.CreateCountryAsync(country);
                    if (createdCountry == null)
                    {
                         return NotFound(); // NotFound = 404 HTTP Status Code
                    }

                    return Ok(createdCountry); // Ok = 200 HTTP Status Code
               }
               catch (Exception ex)
               {
                    if (ex.Message.Contains("duplicate"))
                    {
                         return Conflict(string.Format("El país {0} ya existe.", country.Name));
                    }
                    return Conflict(ex.Message); // Conflict = 409 HTTP Status Code
               }
          }

          [HttpGet, ActionName("GetById")]
          [Route("GetById/{id}")]
          public async Task<ActionResult<Country>> GetCountryByIdAsync(Guid id)
          {
               if (id == null) return BadRequest("Id es un valor obligatorio");

               var country = await _countryService.GetCountryByIdAsync(id); // Aquí estoy yendo a mi capa de "Domain" para traer la lista de paises.
               if (country == null) return NotFound();
               return Ok(country);
          }

          [HttpGet, ActionName("GetByName")]
          [Route("GetByName/{name}")]
          public async Task<ActionResult<Country>> GetCountryByNameAsync(string name)
          {
               if (name == null) return BadRequest("El nombre del país es un valor obligatorio");

               var country = await _countryService.GetCountryByNameAsync(name); // Aquí estoy yendo a mi capa de "Domain" para traer la lista de paises.
               if (country == null) return NotFound();
               return Ok(country);
          }

          [HttpPut, ActionName("Edit")]
          [Route("Edit")]
          public async Task<ActionResult<Country>> EditCountryAsync(Country country)
          {
               try
               {
                    var editedCountry = await _countryService.EditCountryAsync(country);
                    return Ok(editedCountry);
               }
               catch (Exception ex)
               {
                    if (ex.Message.Contains("duplicate"))
                         return Conflict(string.Format("{0} ya existe.", country.Name));

                    return Conflict(ex.Message); // Conflict = 409 HTTP Status Code
               }
          }

          [HttpPut, ActionName("Delete")]
          [Route("Delete")]
          public async Task<ActionResult<Country>> DeleteCountryAsync(Guid id)
          {
               if (id == null) return BadRequest("El campo ID es obligatorio!");

               var deletedCountry = await _countryService.DeleteCountryAsync(id);
               if (deletedCountry == null) return NotFound("País no encontrado!");

               return Ok(deletedCountry);
          }
     }
}
