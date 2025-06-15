using Microsoft.EntityFrameworkCore;
using SistemaMamoreGranito.Models;

namespace SistemaMamoreGranito.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario>? Usuarios { get; set; }
        public DbSet<Bloco>? Blocos { get; set; }
        public DbSet<Chapa>? Chapas { get; set; }
        public DbSet<ProcessoSerragem>? ProcessosSerragem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações adicionais do modelo podem ser adicionadas aqui
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Configuração da relação entre Chapa e Bloco
            modelBuilder.Entity<Chapa>()
                .HasOne(c => c.BlocoOrigem)
                .WithMany()
                .HasForeignKey(c => c.BlocoId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            // Configuração da relação entre ProcessoSerragem e Bloco
            modelBuilder.Entity<ProcessoSerragem>()
                .HasOne(p => p.Bloco)
                .WithMany()
                .HasForeignKey(p => p.BlocoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
} 