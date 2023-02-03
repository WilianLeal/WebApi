using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WebApi.Data.Data
{
    public partial class WebApiDbContext : DbContext
    {
        public WebApiDbContext()
        {
        }

        public WebApiDbContext(DbContextOptions<WebApiDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Produtos> Produtos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-SDSQ0C8\\SQLEXPRESS;Initial Catalog=WebApiDb;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produtos>(entity =>
            {
                entity.HasKey(e => e.IdProduto)
                    .HasName("PK__Produtos__5EEDF7C32171F8AE");

                entity.Property(e => e.IdProduto).HasColumnName("idProduto");

                entity.Property(e => e.CnpjFornecedor)
                    .HasMaxLength(18)
                    .IsUnicode(false);

                entity.Property(e => e.DataFabricacao)
                    .HasColumnName("dataFabricacao")
                    .HasColumnType("date");

                entity.Property(e => e.DataValidade)
                    .HasColumnName("dataValidade")
                    .HasColumnType("date");

                entity.Property(e => e.DescricaoFornecedor)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DescricaoProduto)
                    .IsRequired()
                    .HasColumnName("descricaoProduto")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SituacaoProduto).HasColumnName("situacaoProduto");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
