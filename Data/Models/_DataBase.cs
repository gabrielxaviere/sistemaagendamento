using Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Models
{
    public partial class DataBase : DbContext
    {
        public DataBase()
        {
        }

        public DataBase(DbContextOptions<DataBase> options)
            : base(options)
        {
        }

        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<Usuarios> Consultas { get; set; }
        public virtual DbSet<ControleAcessos> ControleAcessos { get; set; }
        public virtual DbSet<ControleAcessosPermissao> ControleAcessosPermissao { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(conn.connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Sobrenome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Email)
                   .IsRequired()
                   .HasMaxLength(100)
                   .IsUnicode(false)
                   .HasDefaultValueSql("('')");

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.HasOne(d => d.Especialidades)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdEspecialidade)
                    .HasConstraintName("FK_Usuarios_Especialidades");
            });

            modelBuilder.Entity<Consultas>(entity =>
            {
                entity.HasOne(d => d.Usuarios)
                    .WithMany(p => p.Consultas)
                    .HasForeignKey(d => d.IdPaciente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Consultas_Usuarios");
            });

            modelBuilder.Entity<Especialidades>(entity =>
            {
                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<ControleAcessos>(entity =>
            {
                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Icone)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Ordem).HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<ControleAcessosPermissao>(entity =>
            {
                entity.HasOne(d => d.Id_ControleAcessosNavigation)
                    .WithMany(p => p.ControleAcessosPermissao)
                    .HasForeignKey(d => d.Id_ControleAcessos)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ControleAcessosPermissao_ControleAcessos");

                entity.HasOne(d => d.Id_UsuariosNavigation)
                    .WithMany(p => p.ControleAcessosPermissao)
                    .HasForeignKey(d => d.Id_Usuarios)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ControleAcessosPermissao_Usuarios");
            });
        }
    }
}
