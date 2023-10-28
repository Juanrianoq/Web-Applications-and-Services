using API_First_Proyect.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace API_First_Proyect.DAL.Entitites
{
     public class Country : AuditBase
     {
          [Display(Name = "País")] // Sirve para pintar el nombre, en el front.
          [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
          [Required(ErrorMessage = "El campo {0} es obligatorio!")]
          public string Name { get; set; }
          

          [Display(Name = "Estados")]
          public ICollection<State>? States { get; set; }  // Relación con "State".
     }
}
