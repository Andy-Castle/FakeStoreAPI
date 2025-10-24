using FakeStoreAPI.Model;

namespace FakeStoreAPI.Services
{
    public interface IProductos
    {
        Task<List<Producto>> GetAllProductsAsync();

        Task<List<ProductoPublic>> GetAllProductsPublicAsync(string moneda);


    }
}
