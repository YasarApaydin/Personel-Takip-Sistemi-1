using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YuzTanimaTakip.Models;

public partial class CalisanKayitlari
{
    [Key]
    public Guid KayitId { get; set; }

    public Guid? KullaniciId { get; set; }

    public DateTime? GirisSaati { get; set; }

    public DateTime? CikisSaati { get; set; }

    public TimeOnly? MolaSuresi { get; set; }

    public DateTime? KayitTarihi { get; set; }

    public virtual Kullanici? Kullanici { get; set; }

    public virtual ICollection<Molalar> Molalars { get; set; } = new List<Molalar>();
}
