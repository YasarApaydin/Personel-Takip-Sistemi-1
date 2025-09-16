using System.ComponentModel.DataAnnotations;

namespace YuzTanimaTakip.ViewModels
{
    public class YeniCalisanViewModel
    {

        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Ad zorunludur.")]
        public string Ad { get; set; }
        [Required(ErrorMessage = "Soyad zorunludur.")]
        public string Soyad { get; set; }

        [Required(ErrorMessage = "Fotoğraf seçilmelidir.")]
        public IFormFile FotoDosya { get; set; }

        [Required(ErrorMessage = "Email zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon zorunludur.")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        public string Telefon { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur.")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
        [DataType(DataType.Password)]
        public string Sifre { get; set; }

        [Required(ErrorMessage = "Şifre tekrarı zorunludur.")]
        [Compare("Sifre", ErrorMessage = "Şifreler eşleşmiyor.")]
        [DataType(DataType.Password)]
        public string SifreTekrar { get; set; }

    }
}
