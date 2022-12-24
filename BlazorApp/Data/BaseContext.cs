using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BlazorApp.Data
{
    public partial class BaseContext : DbContext
    {
        public BaseContext()
        {
        }

        public BaseContext(DbContextOptions<BaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Core> Cores { get; set; }
        public virtual DbSet<GameServer> GameServers { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<ValidRole> ValidRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;database=laba_base;uid=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.3.13-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8");

            modelBuilder.Entity<Core>(entity =>
            {
                entity.ToTable("core");

                entity.Property(e => e.CoreId)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("core_id");

                entity.Property(e => e.GameVersion)
                    .HasMaxLength(10)
                    .HasColumnName("game_version");

                entity.Property(e => e.Name)
                    .HasMaxLength(40)
                    .HasColumnName("name");

                entity.Property(e => e.Version)
                    .HasMaxLength(10)
                    .HasColumnName("version");
            });

            modelBuilder.Entity<GameServer>(entity =>
            {
                entity.HasKey(e => e.ServerId)
                    .HasName("PRIMARY");

                entity.ToTable("game_server");

                entity.HasIndex(e => e.CoreId, "core_id");

                entity.Property(e => e.ServerId)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("server_id");

                entity.Property(e => e.CoreId)
                    .HasColumnType("int(11)")
                    .HasColumnName("core_id");

                entity.Property(e => e.Discrib).HasColumnName("discrib");

                entity.Property(e => e.Ip)
                    .HasMaxLength(10)
                    .HasColumnName("ip")
                    .IsFixedLength(true);

                entity.Property(e => e.Name)
                    .HasMaxLength(40)
                    .HasColumnName("name");

                entity.Property(e => e.Port)
                    .HasColumnType("int(11)")
                    .HasColumnName("port");

                entity.HasOne(d => d.Core)
                    .WithMany(p => p.GameServers)
                    .HasForeignKey(d => d.CoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("game_server_ibfk_1");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("permission");

                entity.HasIndex(e => e.PlayerId, "player_id");

                entity.HasIndex(e => e.RoleId, "role_id");

                entity.HasIndex(e => e.ServerId, "server_id");

                entity.Property(e => e.PermissionId)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("permission_id");

                entity.Property(e => e.PermissionName)
                    .HasMaxLength(40)
                    .HasColumnName("permission");

                entity.Property(e => e.PlayerId)
                    .HasColumnType("int(11)")
                    .HasColumnName("player_id");

                entity.Property(e => e.RoleId)
                    .HasColumnType("int(11)")
                    .HasColumnName("role_id");

                entity.Property(e => e.ServerId)
                    .HasColumnType("int(11)")
                    .HasColumnName("server_id");

                entity.Property(e => e.Value)
                    .HasMaxLength(30)
                    .HasColumnName("value");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.PlayerId)
                    .HasConstraintName("permission_ibfk_1");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("permission_ibfk_2");

                entity.HasOne(d => d.Server)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.ServerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("permission_ibfk_3");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("player");

                entity.Property(e => e.PlayerId)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("player_id");

                entity.Property(e => e.Balance)
                    .HasColumnType("int(11)")
                    .HasColumnName("balance");

                entity.Property(e => e.Login)
                    .HasMaxLength(30)
                    .HasColumnName("login");

                entity.Property(e => e.Nikname)
                    .HasMaxLength(40)
                    .HasColumnName("nikname");

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(50)
                    .HasColumnName("password_hash");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.HasIndex(e => e.InheritedId, "inherited_id");

                entity.Property(e => e.RoleId)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("role_id");

                entity.Property(e => e.InheritedId)
                    .HasColumnType("int(11)")
                    .HasColumnName("inherited_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasColumnType("int(11)")
                    .HasColumnName("price");

                entity.HasOne(d => d.Inherited)
                    .WithMany(p => p.InverseInherited)
                    .HasForeignKey(d => d.InheritedId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("role_ibfk_1");
            });

            modelBuilder.Entity<ValidRole>(entity =>
            {
                entity.ToTable("valid_role");

                entity.HasIndex(e => e.PlayerId, "player_id");

                entity.HasIndex(e => e.RoleId, "role_id");

                entity.Property(e => e.ValidRoleId)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("valid_role_id");

                entity.Property(e => e.ExpirationDate)
                    .HasMaxLength(6)
                    .HasColumnName("expiration_date");

                entity.Property(e => e.PlayerId)
                    .HasColumnType("int(11)")
                    .HasColumnName("player_id");

                entity.Property(e => e.ReceivingDate)
                    .HasMaxLength(6)
                    .HasColumnName("receiving_date");

                entity.Property(e => e.RoleId)
                    .HasColumnType("int(11)")
                    .HasColumnName("role_id");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.ValidRoles)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("valid_role_ibfk_1");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.ValidRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("valid_role_ibfk_2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
