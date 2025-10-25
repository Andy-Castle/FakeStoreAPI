using FakeStoreAPI.Model;
using FakeStoreAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/*
 * Controlador para obtener los productos de la FakeStoreAPI
 */
namespace FakeStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize] //Esto hace que cualquier cliente que llame a este controlador este autenticado
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
