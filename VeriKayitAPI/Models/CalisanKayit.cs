using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeriKayitAPI.Models
{
    [Table("CalisanKayitlaris")]
    public class CalisanKayit
    {
        [Key]
        public Guid KayitId { get; set; }  // KayitId, otomatik GUID değeri atanacak
       public Guid KullaniciId { get; set; }
        public DateTime? GirisSaati { get; set; }  // GirisSaati, datetime2(7)
        public DateTime? CikisSaati { get; set; }  // CikisSaati, datetime2(7)
        public TimeSpan? MolaSuresi { get; set; }  // MolaSuresi, time(7)
        public DateTime KayitTarihi { get; set; }  // KayitTarihi, datetime2(7)
    }
}
