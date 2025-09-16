using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.Contracts;
using System.Net.Http.Headers;
using YuzTanimaTakip.Data;
using YuzTanimaTakip.Models;
using YuzTanimaTakip.ViewModels;

namespace YuzTanimaTakip.Controllers
{
    public class YoneticiController : Controller
    {
        private readonly UserManager<Kullanici> userManager;
        private readonly YuzTanimaContext dbContext;
     

        public YoneticiController(UserManager<Kullanici> _userManager, YuzTanimaContext _dbContext, IAuthenticationService authenticationService)
        {
            dbContext = _dbContext;
            userManager = _userManager;
        
        }

        [Authorize]
        public IActionResult Index()
        {
            KullaniciAdiAyarla();
            return View();
        }




        [Authorize]
        public async Task<IActionResult> Yonetici()
        {
            KullaniciAdiAyarla();
            Guid userRolId = Guid.Parse("3d3241b4-c7bb-4111-ad9c-dab1dc407423");

            var personeller = await dbContext.Users
                .Where(k => k.KullaniciTurId == userRolId && k.AktifMi== true)
                .Select(k => new PersonelYoneticiViewModel
                {
                    Id = k.Id,
                    Ad = k.Ad,
                    Soyad = k.Soyad,
                    Email = k.Email,
                    Telefon = k.PhoneNumber
                })
                .ToListAsync();

            return View(personeller);
        }

        [Authorize]
        public IActionResult Camera()
        {
            KullaniciAdiAyarla();
            return View();
        }



        [Authorize]
        public IActionResult PersonelBilgi()
        {
            KullaniciAdiAyarla();
            return View();
        }



        public async Task<IActionResult> Logout()
        {
          
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

         
            HttpContext.Session.Clear();

            
            foreach (var cookie in Request.Cookies)
            {
                Response.Cookies.Delete(cookie.Key);
            }

         
            return RedirectToAction("Anasayfa", "Home"); 
        }

        private void KullaniciAdiAyarla()
        {
            var kullaniciAdi = HttpContext.Session.GetString("KullaniciAdi");
            if (!string.IsNullOrEmpty(kullaniciAdi))
            {
                ViewData["UserFullName"] = kullaniciAdi;
            }
        }


        public IActionResult Add()
        {
            return View();
        }




        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var kullanici = await dbContext.Kullanicis.FindAsync(id);
            if (kullanici == null)
            {
                TempData["Hata"] = "Kullanıcı bulunamadı.";
                return RedirectToAction("Yonetici");
            }

            dbContext.Kullanicis.Remove(kullanici);
            await dbContext.SaveChangesAsync();

            TempData["Basari"] = "Kullanıcı başarıyla silindi.";
            return RedirectToAction("Yonetici");
        }


        [HttpPost]
        public async Task<IActionResult> Add(YeniCalisanViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var mevcut = await dbContext.Kullanicis.FirstOrDefaultAsync(x => x.Email == model.Email);
            if (mevcut != null)
            {
                ModelState.AddModelError("", "Bu e-posta adresi zaten kayıtlı.");
                return View(model);
            }

            // Raspberry Pi'ye veri gönderimi (fotoğraf)
            if (model.FotoDosya != null && model.FotoDosya.Length > 0)
            {
                using var content = new MultipartFormDataContent();
                content.Add(new StringContent(model.Id.ToString()), "kullanici_id");
                content.Add(new StringContent($"{model.Ad} {model.Soyad}"), "adsoyad");

                using var stream = new MemoryStream();
                await model.FotoDosya.CopyToAsync(stream);
                var fileBytes = stream.ToArray();
                var fileContent = new ByteArrayContent(fileBytes);
                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(model.FotoDosya.ContentType);

                var fileName = $"{model.Id}{Path.GetExtension(model.FotoDosya.FileName)}";
                content.Add(fileContent, "foto", fileName);

                using var client = new HttpClient();
                var response = await client.PostAsync("", content);

                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("", "Raspberry Pi'ye fotoğraf yüklenemedi.");
                    return View(model);
                }

                TempData["SuccessMessage"] = "Fotoğraf sisteme yüklendi.";
            }

            var passwordHasher = new PasswordHasher<Kullanici>();
            var yeniKullanici = new Kullanici
            {
                Id = model.Id,
                UserName = model.Email,
                KullaniciTurId= Guid.Parse("3D3241B4-C7BB-4111-AD9C-DAB1DC407423"),
                NormalizedUserName = model.Email.ToUpperInvariant(),
                Ad = model.Ad,
                Soyad = model.Soyad,
                Email = model.Email,
                PhoneNumber = model.Telefon,
                PhoneNumberConfirmed = false,
                EmailConfirmed = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                AktifMi = true,
                OlusturulmaTarihi = DateTime.UtcNow
            };
            yeniKullanici.PasswordHash = passwordHasher.HashPassword(yeniKullanici, model.Sifre);

            dbContext.Kullanicis.Add(yeniKullanici);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("PersonelBilgi", "Yonetici");
        }














        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(Guid id)
        {
            KullaniciAdiAyarla();

            var kullanici = await dbContext.Kullanicis.FindAsync(id);
            if (kullanici == null)
            {
                TempData["Hata"] = "Kullanıcı bulunamadı.";
                return RedirectToAction("Yonetici");
            }

            var model = new YeniCalisanViewModel
            {
                Id = kullanici.Id,
                Ad = kullanici.Ad,
                Soyad = kullanici.Soyad,
                Email = kullanici.Email,
                Telefon = kullanici.PhoneNumber
              
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(YeniCalisanViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var kullanici = await dbContext.Kullanicis.FindAsync(model.Id);
            if (kullanici == null)
            {
                TempData["Hata"] = "Kullanıcı bulunamadı.";
                return RedirectToAction("Yonetici");
            }

            // Email değiştiyse ve yeni email başka bir kullanıcıda var mı kontrol et
            if (kullanici.Email != model.Email)
            {
                var emailVarmi = await dbContext.Kullanicis.AnyAsync(x => x.Email == model.Email && x.Id != model.Id);
                if (emailVarmi)
                {
                    ModelState.AddModelError("", "Bu e-posta adresi başka bir kullanıcı tarafından kullanılıyor.");
                    return View(model);
                }
            }

            // Kullanıcı bilgilerini güncelle
            kullanici.Ad = model.Ad;
            kullanici.Soyad = model.Soyad;
            kullanici.Email = model.Email;
            kullanici.UserName = model.Email;
            kullanici.NormalizedUserName = model.Email.ToUpperInvariant();
            kullanici.PhoneNumber = model.Telefon;

            // Şifre güncelleme isteğe bağlı (eğer modelde şifre varsa ve boş değilse)
            if (!string.IsNullOrWhiteSpace(model.Sifre))
            {
                var passwordHasher = new PasswordHasher<Kullanici>();
                kullanici.PasswordHash = passwordHasher.HashPassword(kullanici, model.Sifre);
            }

            // Fotoğraf güncelleme (isteğe bağlı)
            if (model.FotoDosya != null && model.FotoDosya.Length > 0)
            {
                using var content = new MultipartFormDataContent();
                content.Add(new StringContent(model.Id.ToString()), "kullanici_id");
                content.Add(new StringContent($"{model.Ad} {model.Soyad}"), "adsoyad");

                using var stream = new MemoryStream();
                await model.FotoDosya.CopyToAsync(stream);
                var fileBytes = stream.ToArray();
                var fileContent = new ByteArrayContent(fileBytes);
                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(model.FotoDosya.ContentType);

                var fileName = $"{model.Id}{Path.GetExtension(model.FotoDosya.FileName)}";
                content.Add(fileContent, "foto", fileName);

                using var client = new HttpClient();
                var response = await client.PostAsync("", content);

                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("", "Raspberry Pi'ye fotoğraf yüklenemedi.");
                    return View(model);
                }
            }

            dbContext.Kullanicis.Update(kullanici);
            await dbContext.SaveChangesAsync();

            TempData["Basari"] = "Kullanıcı başarıyla güncellendi.";
            return RedirectToAction("Yonetici");
        }





    }
}
