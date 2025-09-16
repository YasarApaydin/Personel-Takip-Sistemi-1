using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YuzTanimaTakip.Models;

namespace YuzTanimaTakip.Mappings
{
    public class RoleMap : IEntityTypeConfiguration<KullaniciTurleri>
    {
        public void Configure(EntityTypeBuilder<KullaniciTurleri> builder)
        {
            // Primary key
            builder.HasKey(r => r.Id);

            // Index for "normalized" role name to allow efficient lookups
            builder.HasIndex(r => r.NormalizedName).HasDatabaseName("RoleNameIndex").IsUnique();

            // Maps to the AspNetRoles table
            builder.ToTable("Role");

            // A concurrency token for use with the optimistic concurrency checking
            builder.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            builder.Property(u => u.Name).HasMaxLength(256);
            builder.Property(u => u.NormalizedName).HasMaxLength(256);

            // The relationships between Role and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each Role can have many entries in the UserRole join table
            builder.HasMany<AppUserRole>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();

            // Each Role can have many associated RoleClaims
            builder.HasMany<AppRoleClaim>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();



            builder.HasData(
                
                new KullaniciTurleri
                {
                    Id = Guid.Parse("E7DF287E-C1C0-48B6-9C06-A125049F63ED"),
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                 
                },
                new KullaniciTurleri
                {
                    Id = Guid.Parse("6BC5E969-6C00-4055-B396-CC4BAAB0C63A"),
                    Name = "SuperAdmin",
                    NormalizedName = "SUPERADMIN",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                
                },

                new KullaniciTurleri
                {
                    Id = Guid.Parse("3D3241B4-C7BB-4111-AD9C-DAB1DC407423"),
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                   
                }


                
                );
        }
    }
}
