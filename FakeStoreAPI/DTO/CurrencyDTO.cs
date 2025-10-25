using System.ComponentModel.DataAnnotations;

/*
 * Data Transfer Object, para que
 * no expongamos la estructura de los datos
 */
namespace FakeStoreAPI.DTO
{
    public class CurrencyDTO
    {
        [Required(ErrorMessage ="El campo Moneda es obligatorio")]
        public string Moneda { get; set; } = string.Empty;

        [Required(ErrorMessage ="El campo Equivalente es obligatorio")]
        [Range(0.0, double.MaxValue, ErrorMessage ="El campo debe ser un número válido")]
        public double Equivalente { get; set; }
    }
}
