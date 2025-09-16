using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YuzTanimaTakip.Models;

public partial class MesaiSaatleri
{
    [Key]
    public Guid MesaiId { get; set; }

    public Guid KullaniciId { get; set; }

    public DateOnly Gun { get; set; }

    public TimeOnly BaslangicSaati { get; set; }

    public TimeOnly BitisSaati { get; set; }

    public TimeOnly ToplamCalismaSuresi { get; set; }

    public DateTime OlusturmaTarihi { get; set; }

    public virtual Kullanici Kullanici { get; set; } = null!;
}
