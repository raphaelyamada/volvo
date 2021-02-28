using Microsoft.EntityFrameworkCore;
using Volvo.Cadastro.Models;

namespace Volvo.Cadastro.Data
{
    public class CadastroContext: DbContext
    {
        public CadastroContext()
        { }
        public CadastroContext(DbContextOptions<CadastroContext> options)
            : base(options)
        { }
        public virtual DbSet<Caminhao> Caminhoes { get; set; }
        public virtual DbSet<Modelo> Modelos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfiguraModelo(modelBuilder);
            ConfiguraCaminhao(modelBuilder);

            modelBuilder.Entity<Modelo>().HasData(
                new Modelo { IdModelo = 1, DescricaoModelo = "FH" },
                new Modelo { IdModelo = 2, DescricaoModelo = "FM" }
            );
        }

        private void ConfiguraModelo(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Modelo>(e=>
            {
                e.ToTable("tbModelo");
                e.HasKey(m => m.IdModelo).HasName("idModelo");
                e.Property(m => m.IdModelo).HasColumnName("idModelo").ValueGeneratedOnAdd();
                e.Property(m => m.DescricaoModelo).HasColumnName("descricao").HasMaxLength(5);
            });
        }

        private void ConfiguraCaminhao(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Caminhao>(e =>
            {
                e.ToTable("tbCaminhao");
                e.HasKey(c => c.IdCaminhao).HasName("idCaminhao");
                e.Property(c => c.IdCaminhao).HasColumnName("idCaminhao").ValueGeneratedOnAdd();
                e.Property(c => c.AnoFabricacao).HasColumnName("anoFabricacao");
                e.Property(c => c.AnoModelo).HasColumnName("anoModelo");
                e.HasOne(c => c.Modelo);

            });
        }

    }
}
