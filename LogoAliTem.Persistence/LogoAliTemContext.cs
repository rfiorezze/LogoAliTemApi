using EntityFramework.Exceptions.PostgreSQL;
using LogoAliTem.Domain;
using LogoAliTem.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LogoAliTem.Persistence
{
    public class LogoAliTemContext : IdentityDbContext<User, Role, int,
                                                        IdentityUserClaim<int>, UserRole,
                                                        IdentityUserLogin<int>, IdentityRoleClaim<int>,
                                                        IdentityUserToken<int>>
    {
        public LogoAliTemContext(DbContextOptions<LogoAliTemContext> options) : base(options) { }

        public DbSet<Motorista> Motoristas { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<ReboqueSolicitacao> Reboques { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            modelBuilder
                .Entity<Motorista>()
                .HasIndex(m => m.Celular)
                .IsUnique();

            modelBuilder
                .Entity<Motorista>()
                .HasMany(m => m.Veiculos)
                .WithOne(v => v.Motorista)
                .HasForeignKey(v => v.MotoristaId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseExceptionProcessor();
        }
    }
}
