using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using YuzTanimaTakip.Models;
using YuzTanimaTakip.Services;
using YuzTanimaTakip.ViewModels;

public class AccountController : Controller
{
    private readonly IConfiguration config;
    private readonly UserManager<Kullanici> userManager;
    private readonly SignInManager<Kullanici> signInManager;
    private readonly EmailService emailService;

    public AccountController(IConfiguration _config, UserManager<Kullanici> _userManager, SignInManager<Kullanici> _signInManager, EmailService _emailService)
    {
        config = _config;
        userManager = _userManager;
        signInManager = _signInManager;
        emailService = _emailService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var kullanici = await userManager.FindByEmailAsync(model.Email);
            if (kullanici != null)
            {
                var result = await signInManager.PasswordSignInAsync(kullanici, model.Sifre, false, false);
                if (result.Succeeded)
                {
                    string confirmationLink = Url.Action("ConfirmEmail", "Account",
                        new { userId = kullanici.Id, token = await userManager.GenerateEmailConfirmationTokenAsync(kullanici) },
                        protocol: Request.Scheme)!;

                    string emailBody = $@"
<html>
<head>
    <style>
        body {{
            font-family: Arial, sans-serif;
            color: #333;
            margin: 0;
            padding: 0;
            background-color: #f7f7f7;
        }}
        .email-container {{
            width: 100%;
            max-width: 600px;
            margin: 0 auto;
            background-color: #ffffff;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
        }}
        .email-header {{
            text-align: center;
            margin-bottom: 20px;
        }}
        .email-header h1 {{
            color: #4CAF50;
            font-size: 24px;
        }}
        .email-body {{
            font-size: 16px;
            line-height: 1.6;
            color: #555;
        }}
        .cta-button-container {{
            text-align: center;
            margin-top: 20px;
        }}
        .cta-button {{
            display: inline-block;
            padding: 12px 20px;
            background-color: #4CAF50;
            color: #fff;
            text-decoration: none;
            font-size: 16px;
            border-radius: 5px;
            text-align: center;
        }}
        .cta-button:hover {{
            background-color: #45a049;
        }}
        .footer {{
            margin-top: 30px;
            text-align: center;
            font-size: 14px;
            color: #888;
        }}
    </style>
</head>
<body>
    <div class='email-container'>
        <div class='email-header'>
            <h1>Hesap Doğrulama</h1>
        </div>
        <div class='email-body'>
            <p>Merhaba {kullanici.Ad} {kullanici.Soyad},</p>
            <p>Hesabınızı doğrulamak için aşağıdaki butona tıklayın:</p>
        </div>
        <div class='cta-button-container'>
            <a href='{confirmationLink}' class='cta-button'>Hesabımı Doğrula</a>
        </div>
        <div class='footer'>
            <p>Bu e-posta, sadece hesabınızı doğrulamak amacıyla gönderilmiştir.</p>
            <p>Yardım için bizimle iletişime geçebilirsiniz.</p>
        </div>
    </div>
</body>
</html>
";

                    try
                    {
                        await emailService.SendEmailAsync(kullanici.Email!, "Hesap Doğrulama", emailBody);
                        return RedirectToAction("EmailSent", "Account");
                    }
                    catch (Exception ex)
                    {
                     
                        ModelState.AddModelError("", "E-posta gönderimi başarısız. Lütfen tekrar deneyin.");
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Geçersiz e-posta veya şifre.");
                }
            }
        }
        return View(model);
    }
    [HttpGet]
    public async Task<IActionResult> ConfirmEmail(Guid userId, string token)
    {
        var kullanici = await userManager.FindByIdAsync(userId.ToString());

        if (kullanici != null)
        {
            var result = await userManager.ConfirmEmailAsync(kullanici, token);
            if (result.Succeeded)
            {
                // Session'ı ayarlıyoruz
                HttpContext.Session.SetString("KullaniciAdi", kullanici.Ad!);
                HttpContext.Session.SetString("KullaniciTur", kullanici.KullaniciTurId.ToString()!);

                // Kullanıcıyı oturum açtırıyoruz
                await signInManager.SignInAsync(kullanici, isPersistent: false);

                // Kullanıcı tipine göre yönlendirme
                if (kullanici.KullaniciTurId.ToString() == "e7df287e-c1c0-48b6-9c06-a125049f63ed")
                {
                    return RedirectToAction("Index", "Admin", new { area = "Admin" });
                }
                else if (kullanici.KullaniciTurId.ToString() == "3d3241b4-c7bb-4111-ad9c-dab1dc407423")
                {
                    return RedirectToAction("Index", "Yonetici");
                }

                return RedirectToAction("Anasayfa", "Home");
            }
        }

        return View("Error");
    }

    public IActionResult EmailSent()
    {
        return View();
    }
}
