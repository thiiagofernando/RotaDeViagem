using Microsoft.EntityFrameworkCore;
using RotaDeViagemApi.Models;

namespace RotaDeViagemApi.Data
{
    public class RotaDbContext : DbContext
    {
        public RotaDbContext(DbContextOptions<RotaDbContext> options) : base(options) { }

        public DbSet<Rota> Rotas { get; set; }
    }
}
