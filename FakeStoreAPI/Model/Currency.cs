using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/*
 * Este es el modelo para las monedas
 */
namespace FakeStoreAPI.Model
{
    [Table("Currency")]
    public class Currency
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Moneda { get; set; } = string.Empty;

        [Required]
        public double Equivalente { get; set; }
    }
}
