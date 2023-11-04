using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using ProjetoMEI.API.Entities;

namespace ProjetoMEI.API.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseSqlite("DataSource=app.db;Cache=Shared");
        
    }
}
