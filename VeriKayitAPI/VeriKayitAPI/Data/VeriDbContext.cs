using Microsoft.EntityFrameworkCore;
using VeriKayitAPI.Models;

namespace VeriKayitAPI.Data
{
    public class VeriDbContext : DbContext
    {
        public VeriDbContext(DbContextOptions<VeriDbContext> options) : base(options) { }

        public DbSet<CalisanKayit> CalisanKayitlaris { get; set; }
    }
}
