using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FormsApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FormsApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

  public IActionResult Index(string searchString, string category)
    
{
    var products = Repository.Products;

    if (!string.IsNullOrEmpty(searchString))
    {
        ViewBag.SearchString = searchString;
        products = products.Where(p => p.Name!.ToLower().Contains(searchString.ToLower())).ToList();
    }
    if(!string.IsNullOrEmpty(category) && category != "0"){
        products = products.Where(p => p.CategoryId == int.Parse(category)).ToList();
    }
    
    // ViewBag.Categories = new SelectList(Repository.Categories,"CategoryId","Name",category);
    var model = new ProductViewModel {
        Products = products,
        Categories = Repository.Categories,
        SelectedCategory = category
    };
    return View(model);
}
[HttpGet]
public IActionResult Create()
{
    ViewBag.Categories = Repository.Categories;
    return View();
}

[HttpPost]
public async Task<IActionResult> Create(Product model, IFormFile imageFile)
{
    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png"};
    var extension = Path.GetExtension(imageFile.FileName);
    var randomFileName = $"{Guid.NewGuid()}{extension}";
    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName);
    if (imageFile != null)
    {
        if (!allowedExtensions.Contains(extension))
        {
            ModelState.AddModelError("", "Sadece .jpg, .jpeg ve .png uzantılı dosyalar yüklenebilir."); 
        }

        if (ModelState.IsValid)
        {
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }
            model.Image = randomFileName;
            model.ProductID = Repository.Products.Count + 1; 
            Repository.Products.Add(model); // veya dbContext.Products.Add(model);
            return RedirectToAction("Index");
        }
    }
    else
    {
        ModelState.AddModelError("", "Bir dosya seçmelisiniz.");
    }

    // Hata varsa View tekrar gösteriliyor, ViewBag.Categories tekrar atanmalı!
    ViewBag.Categories = Repository.Categories;
    Repository.CreateProduct(model); 
    return RedirectToAction("Index");
}

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
} 