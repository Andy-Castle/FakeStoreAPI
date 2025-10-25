namespace FakeStoreAPI.Model
{

    /*
     * Este es el modelo para la API con los cambios solicitados
     */
    public class ProductoPublic
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public double Price { get; set; }

        public double PublicPrice { get; set; }

        public double Currency { get; set; }

        public string? Description { get; set; }

        public string? Category { get; set; }

        public string? Image { get; set; }
    }
}
