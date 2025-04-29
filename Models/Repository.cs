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
        
        public static List<Product> Products
        {
            get { 
                return _products; 
                }
        }
        public static List<Category> Categories
        {
            get { 
                return _categories; 
                }
        }
    }
}