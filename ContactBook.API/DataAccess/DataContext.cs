using ContactBook.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactBook.API.DataAccess
{
    public class DataContext : DbContext //: IdentityDbContext<User, Role, Guid>
    {
        //    public virtual DbSet<Permission> Permissions { get; set; }
        //    public virtual DbSet<RolePermission> RolePermissions { get; set; }
        //    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        // Views
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasAnnotation("Relational:Collation", "utf8mb4_unicode_ci");

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey("Id");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.ToTable("Contacts");
                entity.HasKey("Id");
            });
        }
    }

}
