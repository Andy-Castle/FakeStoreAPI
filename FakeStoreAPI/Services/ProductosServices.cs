using System.Net;
using System.Text.Json;
using FakeStoreAPI.Controllers;
using FakeStoreAPI.Interfaces;
using FakeStoreAPI.Model;

namespace FakeStoreAPI.Services
{
    public class ProductosServices : IProductos
    {

        private static readonly HttpClient httpClient;

        private readonly ICurrency _currency;

        static ProductosServices()
        {
            httpClient = new HttpClient()
            {
                BaseAddress =  new Uri("https://fakestoreapi.com/products")
            };
        }

        public ProductosServices(ICurrency currency)
        {
            _currency = currency;
        }


        public async Task<List<Producto>> GetAllProductsAsync()
        {
            try
            {
                var url = string.Format("products");
                var resultado = new List<Producto>();
                var respuesta = await httpClient.GetAsync(url);

                if (respuesta.IsSuccessStatusCode)
                {
                    var stringRespuesta = await respuesta.Content.ReadAsStringAsync();
                    var productosRespuesta = JsonSerializer.Deserialize<List<Producto>>(stringRespuesta, new JsonSerializerOptions()
                    {

                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,

                    });
                  


                    resultado.AddRange(productosRespuesta!);


                }
                else
                {
                    if (respuesta.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new Exception("Productos no encontrados");
                    }
                    else
                    {
                        throw new Exception("Fallo al comunicarse con la api");
                    }
                       
                }

                    return resultado;

            }
            catch (HttpRequestException ex)
            {
                throw new Exception("La solicitud HTPP ha fallado" + ex.Message);
            }
        }

        public async Task<List<ProductoPublic>> GetAllProductsPublicAsync(string? moneda = "USD")
        {
            try
            {
                var productosList = await GetAllProductsAsync();
                var productosPublic = new List<ProductoPublic>();
                var currency = await _currency.GetCurrencyByNameAsync(moneda!);

                if (currency == null)
                {
                    throw new Exception("No se encontro moneda para hacer el calculo");
                }


                foreach (var producto in productosList)
                {
                    productosPublic.Add(new ProductoPublic
                    {
                        Id = producto.Id,
                        Title = producto.Title,
                        Price = producto.Price,
                        PublicPrice = Math.Round(producto.Price * 1.10, 2)  ,
                        Currency = Math.Round(producto.Price * currency!.Equivalente, 2),
                        Description = producto.Description,
                        Category = producto.Category,
                        Image = producto.Image,
                    });
                    
                }

                return productosPublic;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("La solicitud HTPP ha fallado" + ex.Message);
            }
        }
    }
}
