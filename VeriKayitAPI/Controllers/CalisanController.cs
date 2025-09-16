using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeriKayitAPI.Data;
using VeriKayitAPI.Models;

namespace VeriKayitAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalisanController : ControllerBase
    {
        private readonly VeriDbContext _context;

        public CalisanController(VeriDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> KayitEkle([FromBody] CalisanKayit veri)
        {
            if (veri == null)
            {
                return BadRequest("Veri boş olamaz.");
            }

            // Gelen veriyi konsola yazdır
            Console.WriteLine($"Gelen Veri: {veri.KayitId}, {veri.GirisSaati}, {veri.CikisSaati}, {veri.MolaSuresi}");

            // Veritabanında var mı kontrol et
            var mevcutKayit = await _context.CalisanKayitlaris
                                             .FirstOrDefaultAsync(c => c.KayitId == veri.KayitId);

            if (mevcutKayit != null)
            {
                // Veri varsa güncelle
                mevcutKayit.KullaniciId = veri.KullaniciId;
                mevcutKayit.GirisSaati = veri.GirisSaati ?? DateTime.Now;
                mevcutKayit.CikisSaati = veri.CikisSaati;
                mevcutKayit.MolaSuresi = veri.MolaSuresi;
                mevcutKayit.KayitTarihi = DateTime.Now;

                _context.CalisanKayitlaris.Update(mevcutKayit);
                await _context.SaveChangesAsync();

                return Ok(new { mesaj = "Kayıt başarılı bir şekilde güncellendi." });
            }
            else
            {
                // Veri yoksa yeni kayıt ekle
                var yeniKayit = new CalisanKayit
                {
                    KayitId = Guid.NewGuid(),  // Yeni bir GUID oluştur
                  KullaniciId = veri.KullaniciId,
                    GirisSaati = veri.GirisSaati ?? DateTime.Now,  // GirisSaati, null ise şu anki tarih
                    CikisSaati = veri.CikisSaati,
                    MolaSuresi = veri.MolaSuresi,
                    KayitTarihi = DateTime.Now
                };

                _context.CalisanKayitlaris.Add(yeniKayit);
                await _context.SaveChangesAsync();

                return Ok(new { mesaj = "Yeni kayıt başarılı bir şekilde eklendi." });
            }
        }

    }
}
