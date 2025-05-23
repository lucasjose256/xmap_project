namespace xmap_project.Data;
using Microsoft.EntityFrameworkCore;
using xmap_project.Modules;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            
        }
        
        public DbSet<User> users { get; set; }
        
        public DbSet<Processo> process { get; set; }
        
        public DbSet<Raia> raia { get; set; }
        
        public DbSet<Atividade> atividade { get; set; }
        
        public DbSet<MetaDados> metaDados { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1:N Raia -> Atividades
            modelBuilder.Entity<Atividade>()
                .HasOne(a => a.Raia)
                .WithMany(r => r.Atividades)
                .HasForeignKey(a => a.raiaId)
                .OnDelete(DeleteBehavior.Cascade);

            // 1:1 Atividade -> MetaDados
            modelBuilder.Entity<MetaDados>()
                .HasOne(m => m.atividade)
                .WithOne(a=>a.MetaDados) // ou .WithOne(a => a.MetaDados) se quiser navegar de Atividade para MetaDados
                .HasForeignKey<MetaDados>(m => m.atividadeId);
            // .OnDelete(DeleteBehavior.Cascade); 
        }


    }

