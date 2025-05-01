using System.ComponentModel.DataAnnotations;

namespace FormsApp.Models
{
    public class Product
    {
        [Display(Name = "Ürün ID")]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Ürün adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Ürün adı en fazla 100 karakter olabilir.")]
        [Display(Name = "Ürün Adı")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Fiyat alanı zorunludur.")]
        [Range(0.01, 100000, ErrorMessage = "Fiyat 0.01 ile 100000 arasında olmalıdır.")]
        [Display(Name = "Ürün Fiyatı")]
        public decimal Price { get; set; }

        [Display(Name = "Ürün Resmi")]
        public string Image { get; set; } = string.Empty;

        [Display(Name = "Aktif mi?")]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Kategori seçimi zorunludur.")]
        [Range(1, int.MaxValue, ErrorMessage = "Lütfen geçerli bir kategori seçin.")]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }
        
    }
}
