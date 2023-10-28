using API_First_Proyect.DAL.Entitites;
using System.ComponentModel.DataAnnotations;

namespace API_First_Proyect.DAL.Entities
{
     public class State : AuditBase
     {
          [Display(Name = "Estado/Departamento")]
          [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
          [Required(ErrorMessage = "El campo {0} es obligatorio!")]
          public string Name { get; set; }


          [Display(Name = "País")]
          public Country? Country { get; set; } // Relación con "Country".

          [Display(Name = "Id País")]
          public Guid CountryId { get; set; } // Foreign Key
     }
}
