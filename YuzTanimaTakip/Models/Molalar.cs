using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YuzTanimaTakip.Models;

public partial class Molalar
{
    [Key]
    public Guid MolaId { get; set; }

    public Guid? KayitId { get; set; }

    public Guid MolaTurId { get; set; }

    public DateTime? MolaBaslangic { get; set; }

    public DateTime? MolaBitis { get; set; }

    public virtual CalisanKayitlari? Kayit { get; set; }

    public virtual MolaTurleri MolaTur { get; set; } = null!;
}
