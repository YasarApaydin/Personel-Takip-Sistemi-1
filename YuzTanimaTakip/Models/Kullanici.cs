using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YuzTanimaTakip.Models;

public partial class Kullanici:IdentityUser<Guid>
{



    public Guid? KullaniciTurId { get; set; }

    public string? Ad { get; set; }


    public string? Soyad { get; set; }

    public string? Foto { get; set; }






    public bool? AktifMi { get; set; }

    public DateTime? OlusturulmaTarihi { get; set; }

    public virtual ICollection<CalisanKayitlari> CalisanKayitlaris { get; set; } = new List<CalisanKayitlari>();

    public virtual ICollection<GecmisKayitlar> GecmisKayitlars { get; set; } = new List<GecmisKayitlar>();


    public virtual KullaniciTurleri? KullaniciTur { get; set; }

    public virtual ICollection<Raporlar> Raporlars { get; set; } = new List<Raporlar>();
}
