using Microsoft.AspNetCore.Mvc;
using YuzTanimaTakip.Data;
using YuzTanimaTakip.Helpers;
using YuzTanimaTakip.Models;
using YuzTanimaTakip.Services;
using YuzTanimaTakip.ViewModels;

namespace YuzTanimaTakip.Controllers
{
    public class HomeController : Controller
    {
        private readonly YuzTanimaContext context;
        private readonly EmailService emailService;

        public HomeController(YuzTanimaContext _context, EmailService _emailService)
        {
            emailService = _emailService;
            context = _context;
        }

       
        public IActionResult Anasayfa()
        {
            KullaniciAdiAyarla();
            return View();
        }

        private void KullaniciAdiAyarla()
        {
            var kullaniciAdi = HttpContext.Session.GetString("KullaniciAdi");
            if (!string.IsNullOrEmpty(kullaniciAdi))
            {
                ViewData["UserFullName"] = kullaniciAdi;
            }
        }




    }
}
