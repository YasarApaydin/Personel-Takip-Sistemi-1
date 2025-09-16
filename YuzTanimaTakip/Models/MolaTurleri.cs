using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YuzTanimaTakip.Models;

public partial class MolaTurleri
{
    [Key]
    public Guid MolaTurId { get; set; }

    public string MolaTurAdi { get; set; } = null!;

    public int Sira { get; set; }

    public virtual ICollection<Molalar> Molalars { get; set; } = new List<Molalar>();
}
