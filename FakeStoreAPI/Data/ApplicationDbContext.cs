using FakeStoreAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace FakeStoreAPI.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Currency> Currencys { get; set; }
    }
}
