using FakeStoreAPI.Custom;
using FakeStoreAPI.Data;
using FakeStoreAPI.DTO;
using FakeStoreAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

/*
 * Controlador para el registro y login de usuario
 */
namespace FakeStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous] //Permite a cualquier usuario sin estar autenticado usar los metodos del controlador
    [ApiController]
    public class AccesoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly Utilities _utilities;

        public AccesoController(ApplicationDbContext context, Utilities utilities)
        {
            _context = context;
            _utilities = utilities;
        }

        [HttpPost]
        [Route("Registrarse")]
        public async Task<IActionResult> Registrarse(UsuarioDTO objeto)
        {
            var modeloUsuario = new Usuario
            {
                Nombre = objeto.Nombre, 
                Correo = objeto.Correo, 
                Clave = _utilities.encriptarSHA256(objeto.Clave), 
            };

            await _context.Usuarios.AddAsync(modeloUsuario);
            await _context.SaveChangesAsync(); 

            if (modeloUsuario.IdUsuario != 0)
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true });
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false });
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDTO objeto)
        {
            var usuarioEncontrado = await _context.Usuarios
                .Where(u => u.Correo == objeto.Correo && u.Clave == _utilities.encriptarSHA256(objeto.Clave))
                .FirstOrDefaultAsync();

            if (usuarioEncontrado == null)
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false, token = "" });
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true, token = $"Bearer " + _utilities.generarJWT(usuarioEncontrado) });
            }
        }
    }
}
