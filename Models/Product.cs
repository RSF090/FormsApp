using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FormsApp.Models
{
    public class Product
    {
        [Display(Name="Ürün ID")]
        public int ProductID { get; set; }
        [Display(Name="Ürün Adı")]
        public string Name { get; set; } = string.Empty;
       [Display(Name="Ürün Fiyatı")]
        public decimal Price { get; set; }
        [Display(Name="Ürün Resmi")]
        public string Image { get; set; }= string.Empty;
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }
    }
}