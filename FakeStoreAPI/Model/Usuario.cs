using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

/*
 * Este es el modelo para los usuarios y poder hacer
 * registro y login, que usa data annotations para validaciones
 * y mapeo a la base de datos
 */
namespace FakeStoreAPI.Model
{
    public class Usuario
    {
        [Key]
        [Column("IdUsuario")]
        public int IdUsuario { get; set; }

        [Column("Nombre")]
        [StringLength(50)] 
        [Unicode(false)]   
        public string? Nombre { get; set; }

        [Column("Correo")]
        [EmailAddress]
        [StringLength(50)]
        [Unicode(false)]
        public string? Correo { get; set; }

        [Column("Clave")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Clave { get; set; }
    }
}
