using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
