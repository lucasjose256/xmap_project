using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace xmap_project.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            
            // Coloque aqui a string de conexão com seu banco no Docker
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=meubanco;Username=eproc;Password=coritiba2025");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}