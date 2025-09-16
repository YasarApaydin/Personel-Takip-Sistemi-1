using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YuzTanimaTakip.Models;

public partial class Raporlar
{
    [Key]
    public Guid RaporId { get; set; }

    public Guid? KullaniciId { get; set; }

    public string? RaporAdi { get; set; }

    public TimeOnly? ToplamCalismaSuresi { get; set; }

    public TimeOnly? ToplamMolaSuresi { get; set; }

    public DateTime? OlusturulmaTarihi { get; set; }

    public virtual Kullanici? Kullanici { get; set; }
}
