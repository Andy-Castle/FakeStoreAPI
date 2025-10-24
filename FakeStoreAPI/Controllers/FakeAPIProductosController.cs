using FakeStoreAPI.Model;
using FakeStoreAPI.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FakeStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakeAPIProductosController : ControllerBase
    {
        private readonly IProductos _productoService;

        public FakeAPIProductosController(IProductos productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<IEnumerable<Producto>> GetAllProductos()
        {
            return await _productoService.GetAllProductsAsync();
        }

        [HttpGet("GetAllProductosPublic")]
        public async Task<IActionResult> GetAllProductosPublic(string? moneda)
        {
            try
            {
                var monedaExiste = string.IsNullOrEmpty(moneda) ? "MXN" : moneda;
                var productos = await _productoService.GetAllProductsPublicAsync(monedaExiste);
                return Ok(productos);

            }
            catch(Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }

        }
    }
}
