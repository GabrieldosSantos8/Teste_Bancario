using Microsoft.EntityFrameworkCore;

namespace TesteItau
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Adicione DbSet<T> para suas entidades aqui
        // public DbSet<SeuModelo> Modelos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Ativo> Ativos { get; set; }
        public DbSet<Cotacao> Cotacoes { get; set; }
        public DbSet<Operacao> Operacoes { get; set; }
        public DbSet<Posicao> Posicoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Usuario>().ToTable("tb_usuarios");
            modelBuilder.Entity<Ativo>().ToTable("tb_ativos");
            modelBuilder.Entity<Cotacao>().ToTable("tb_cotacao");
            modelBuilder.Entity<Operacao>().ToTable("tb_operacoes");
            modelBuilder.Entity<Posicao>().ToTable("tb_posicao");
        }
    }
}
