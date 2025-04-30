using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormsApp.Models
// Sayfaya gönderilen veriler paketlenerek gönderilir.
{
      public class ProductViewModel
{
    public List<Product> Products { get; set; }=null!;
    public List<Category> Categories { get; set; }=null!;
    public string? SelectedCategory { get; set; }
}
}