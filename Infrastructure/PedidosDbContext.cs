using Microsoft.EntityFrameworkCore;
using EmbaladorPedidosApi.Domain.Entities;

namespace EmbaladorPedidosApi.Infrastructure
{
    public class PedidosDbContext : DbContext
    {
        public PedidosDbContext(DbContextOptions<PedidosDbContext> options)
            : base(options)
        {
        }
        public DbSet<Caixa> Caixas { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Caixa>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Largura).HasColumnType("decimal(18,2)");
                entity.Property(c => c.Altura).HasColumnType("decimal(18,2)");
                entity.Property(c => c.Profundidade).HasColumnType("decimal(18,2)");
            });
            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(p => p.Id);
            });
            modelBuilder.Entity<Produto>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Nome).IsRequired();
                entity.Property(p => p.Largura).HasColumnType("decimal(18,2)");
                entity.Property(p => p.Altura).HasColumnType("decimal(18,2)");
                entity.Property(p => p.Profundidade).HasColumnType("decimal(18,2)");
                entity.HasOne(p => p.Pedido)
                      .WithMany(p => p.Produtos)
                      .HasForeignKey(p => p.PedidoId);
            });

            modelBuilder.Entity<Caixa>().HasData(
                new Caixa { Id = 1, Nome = "Caixa  Pequena", Largura = 20, Altura = 20, Profundidade = 20 },
                new Caixa { Id = 2, Nome = "Caixa  Média", Largura = 30, Altura = 30, Profundidade = 30 },
                new Caixa { Id = 3, Nome = "Caixa  Grande", Largura = 40, Altura = 40, Profundidade = 40 }
            );
        }
    }
}
