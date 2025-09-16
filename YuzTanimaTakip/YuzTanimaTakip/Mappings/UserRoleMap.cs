using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using YuzTanimaTakip.Models;

namespace YuzTanimaTakip.Mappings
{
    public class UserRoleMap : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {
            // Primary key
            builder.HasKey(r => new { r.UserId, r.RoleId });

            // Maps to the AspNetUserRoles table
            builder.ToTable("AspNetUserRoles");
            builder.HasData(
                new AppUserRole
                {
                    UserId = Guid.Parse("C76AC66E-11A9-4728-B8D2-1C188AC591E5"),
                    RoleId = Guid.Parse("6BC5E969-6C00-4055-B396-CC4BAAB0C63A")
                },
                new AppUserRole
                {
                    UserId = Guid.Parse("2BE8C118-5067-4CFE-BE99-A811CDF6C8A5"),
                    RoleId = Guid.Parse("E7DF287E-C1C0-48B6-9C06-A125049F63ED")
                },
                  new AppUserRole
                  {
                      UserId = Guid.Parse("D56BB1D9-0B12-4D99-AB77-2A7D1A27A5D6"),
                      RoleId = Guid.Parse("3D3241B4-C7BB-4111-AD9C-DAB1DC407423")
                  }

                );

        }
    }
}
