using System.ComponentModel.DataAnnotations;

namespace API_First_Proyect.DAL.Entitites
{
      public class AuditBase
      {
            [Key] // DataAnnotation: Me sirve para definir que está propiedad ID es un PK.
            [Required] // Para campos obligatorios, o sea que deben tener un valor (no se permiten NULLs).
            public virtual Guid Id { get; set; } // Será la PK de todas las tablas de mi BD.
            public virtual DateTime? CreatedDate { get; set; }
            public virtual DateTime? ModifiedDate { get; set; } 
      }
}
