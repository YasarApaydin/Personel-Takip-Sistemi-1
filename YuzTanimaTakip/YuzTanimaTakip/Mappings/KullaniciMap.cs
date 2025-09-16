using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YuzTanimaTakip.Models;

namespace YuzTanimaTakip.Mappings
{
    public class KullaniciMap : IEntityTypeConfiguration<Kullanici>
    {
        public void Configure(EntityTypeBuilder<Kullanici> builder)
        {

            // Primary key
            builder.HasKey(u => u.Id);

            // Indexes for "normalized" username and email, to allow efficient lookups
            builder.HasIndex(u => u.NormalizedUserName).HasDatabaseName("UserNameIndex").IsUnique();
            builder.HasIndex(u => u.NormalizedEmail).HasDatabaseName("EmailIndex");

            // Maps to the AspNetUsers table
            builder.ToTable("Kullanici");

            // A concurrency token for use with the optimistic concurrency checking
            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            builder.Property(u => u.UserName).HasMaxLength(100);
            builder.Property(u => u.NormalizedUserName).HasMaxLength(256);
            builder.Property(u => u.Email).HasMaxLength(100);
            builder.Property(u => u.NormalizedEmail).HasMaxLength(256);

            // The relationships between User and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each User can have many UserClaims
            builder.HasMany<AppUserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

            // Each User can have many UserLogins
            builder.HasMany<AppUserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

            // Each User can have many UserTokens
            builder.HasMany<AppUserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

            // Each User can have many entries in the UserRole join table
            builder.HasMany<AppUserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();






            var superAdmin = new Kullanici
            {
                Id = Guid.Parse("C76AC66E-11A9-4728-B8D2-1C188AC591E5"),
                UserName = "emirhanyasar@gmail.com",
                NormalizedUserName = "EMIRHANYASAR@GMAIL.COM",
                Email = "emirhanyasar@gmail.com",
                NormalizedEmail = "EMIRHANYASAR@GMAIL.COM",
                Ad = "Ali",
                Soyad = "Altunar",
                PhoneNumber = "+905415062981",
              
                PhoneNumberConfirmed = false,
                EmailConfirmed = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                Foto = "profil_resmi.jpg",
                AktifMi = true,
                OlusturulmaTarihi = DateTime.UtcNow,
                KullaniciTurId = Guid.Parse("3D3241B4-C7BB-4111-AD9C-DAB1DC407423")

            };

            superAdmin.PasswordHash = CreatePasswordHash(superAdmin, "123456");



            var admin = new Kullanici
            {
                Id = Guid.Parse("2BE8C118-5067-4CFE-BE99-A811CDF6C8A5"),
                UserName = "apaydinyasar5@gmail.com",
                NormalizedUserName = "APAYDINYASAR5@GMAIL.COM",
                Email = "apaydinyasar5@gmail.com",
                NormalizedEmail = "APAYDINYASAR5@GMAIL.COM",
                Ad = "Emirhan",
                Soyad = "Tanrıverdı",
                PhoneNumber = "+905415062981",

                PhoneNumberConfirmed = false,
                EmailConfirmed = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                Foto = "profil_resmi.jpg",
                AktifMi = true,
                OlusturulmaTarihi = DateTime.UtcNow,
                KullaniciTurId = Guid.Parse("E7DF287E-C1C0-48B6-9C06-A125049F63ED")

            };

            admin.PasswordHash = CreatePasswordHash(admin, "123456");


            var user = new Kullanici
            {
                Id = Guid.Parse("D56BB1D9-0B12-4D99-AB77-2A7D1A27A5D6"),
                UserName = "apaydinyasar0@gmail.com",
                NormalizedUserName = "APAYDINYASAR0@GMAIL.COM",
                Email = "apaydinyasar0@gmail.com",
                NormalizedEmail = "APAYDINYASAR0@GMAIL.COM",
                Ad = "Yaşar",
                Soyad = "Apaydın",
                PhoneNumber = "+905412345678",
                PhoneNumberConfirmed = false,
                EmailConfirmed = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                Foto = "profil_resmi.jpg",
                AktifMi = true,
                OlusturulmaTarihi = DateTime.UtcNow,
                KullaniciTurId = Guid.Parse("3D3241B4-C7BB-4111-AD9C-DAB1DC407423")
            };

          
            user.PasswordHash = CreatePasswordHash(user, "Yasar5251@");

            // Kullanıcıyı ekle
            builder.HasData(superAdmin, admin, user);






           

        }

        private string CreatePasswordHash(Kullanici user, string password)
        {
            var passwordHasher = new PasswordHasher<Kullanici>();
            return passwordHasher.HashPassword(user, password);

        }

    }
}
