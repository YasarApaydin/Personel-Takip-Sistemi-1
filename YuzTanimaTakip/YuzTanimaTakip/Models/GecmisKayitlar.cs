using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YuzTanimaTakip.Models;

public partial class GecmisKayitlar
{
    [Key]
    public Guid GecmisKayitId { get; set; }

    public Guid? KullaniciId { get; set; }

    public DateTime? GirisZamani { get; set; }

    public DateTime? CikisZamani { get; set; }

    public TimeOnly? MolaSuresi { get; set; }

    public virtual Kullanici? Kullanici { get; set; }
}
