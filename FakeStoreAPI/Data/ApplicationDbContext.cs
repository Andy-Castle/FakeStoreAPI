using FakeStoreAPI.Model;
using Microsoft.EntityFrameworkCore;

/*
 * Aqui en donde definimos como se conectara y que tablas
 * estaran disponibles para la base de datos, esto mediante
 * el paquete en EntityFrameworkCore
 */
namespace FakeStoreAPI.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Currency> Currencys { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
