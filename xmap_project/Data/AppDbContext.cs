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
        public DbSet<Process> process { get; set; }
        
        public DbSet<Atividade> atividade { get; set; }
        
        public DbSet<MetaDados> metaDados { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1:N Process -> Atividades
            modelBuilder.Entity<Atividade>()
                .HasOne(a => a.process)
                .WithMany(p => p.atividades)
                .HasForeignKey(a => a.processId);

            // 1:1 Atividade -> MetaDados
            modelBuilder.Entity<MetaDados>()
                .HasOne(m => m.atividade)
                .WithOne(a => a.metaDados)
                .HasForeignKey<MetaDados>(m => m.atividadeId);
        }


    }

