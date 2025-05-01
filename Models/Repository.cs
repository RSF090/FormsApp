using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormsApp.Models
{
    public class Repository
    {
        public static readonly List<Product> _products = new List<Product>();
        public static readonly List<Category> _categories = new List<Category>();
        
        static Repository(){
            _categories.Add(new Category { CategoryId = 1, Name = "Telefon" });
            _categories.Add(new Category { CategoryId = 2, Name = "Bilgisayar" });

            _products.Add(new Product { ProductID = 1, Name = "Iphone 13", Price = 30000, Image = "13.jpg", IsActive = true, CategoryId = 1 });
            _products.Add(new Product { ProductID = 2, Name = "Iphone 14", Price = 40000, Image = "14.jpg", IsActive = true, CategoryId = 1 });
            _products.Add(new Product { ProductID = 3, Name = "Iphone 15", Price = 50000, Image = "15.jpg", IsActive = true, CategoryId = 1 });
            _products.Add(new Product { ProductID = 4, Name = "Iphone 16", Price = 60000, Image = "16.jpg", IsActive = true, CategoryId = 1 });
            _products.Add(new Product { ProductID = 5, Name = "Samsung S24", Price = 30000, Image = "24.jpg", IsActive = true, CategoryId = 1 });
            _products.Add(new Product { ProductID = 6, Name = "Samsung S25", Price = 40000, Image = "s25.jpg", IsActive = true, CategoryId = 1 });
            _products.Add(new Product { ProductID = 7, Name = "Rog", Price = 60000, Image = "rog1.jpg", IsActive = true, CategoryId = 2 });
            _products.Add(new Product { ProductID = 8, Name = "Tuf", Price = 45000, Image = "tuf1.jpg", IsActive = true, CategoryId = 2 });
            _products.Add(new Product { ProductID = 9, Name = "MSI", Price = 55000, Image = "msı1.jpg", IsActive = true, CategoryId = 2 });
        }
        
        public static List<Product> Products
        {
            get { 
                return _products; 
                }
        }
        public static void CreateProduct(Product entity){
            _products.Add(entity);
        }
        public static List<Category> Categories
        {
            get { 
                return _categories; 
                }
        }
        public static void EditProduct(Product updatedProduct){
            {
                var entity = _products.FirstOrDefault(x => x.ProductID == updatedProduct.ProductID);
                if (entity != null)
                {
                    entity.Name = updatedProduct.Name;
                    entity.Price = updatedProduct.Price;
                    entity.Image = updatedProduct.Image;
                    entity.IsActive = updatedProduct.IsActive;
                    entity.CategoryId = updatedProduct.CategoryId;
                }
            }
    }
}

}