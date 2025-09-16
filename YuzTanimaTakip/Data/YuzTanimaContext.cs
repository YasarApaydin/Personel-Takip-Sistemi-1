using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Reflection;
using YuzTanimaTakip.Models;

namespace YuzTanimaTakip.Data
{
    public class YuzTanimaContext:IdentityDbContext<Kullanici, KullaniciTurleri, Guid, AppUserClaim, AppUserRole, AppUserLogin, AppRoleClaim, AppUserToken>
    {
        public YuzTanimaContext()
        {
            
        }

        public YuzTanimaContext(DbContextOptions<YuzTanimaContext> options)
       : base(options)
        {
        }

        public virtual DbSet<CalisanKayitlari> CalisanKayitlaris { get; set; }
        public virtual DbSet<GecmisKayitlar> GecmisKayitlars { get; set; }
        public virtual DbSet<Kullanici> Kullanicis { get; set; }
        public virtual DbSet<KullaniciTurleri> KullaniciTurleris { get; set; }
        public virtual DbSet<MesaiSaatleri> MesaiSaatleris { get; set; }
        public virtual DbSet<MolaTurleri> MolaTurleris { get; set; }
        public virtual DbSet<Molalar> Molalars { get; set; }
        public virtual DbSet<Raporlar> Raporlars { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
