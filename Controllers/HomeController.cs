using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FormsApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;

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
        var extension = "";
    if (imageFile != null)
    {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png"};
            extension = Path.GetExtension(imageFile.FileName);
        if (!allowedExtensions.Contains(extension))
        {
            ModelState.AddModelError("", "Sadece .jpg, .jpeg ve .png uzantılı dosyalar yüklenebilir."); 
        }

        if (ModelState.IsValid)
        {
            if (imageFile != null){
                var randomFileName = $"{Guid.NewGuid()}{extension}";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName);
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




[HttpGet]
public IActionResult Edit(int? id)
{
    if (id == null)
    {
        return NotFound();
    }
    var entity = Repository.Products.FirstOrDefault(p => p.ProductID == id);
    if (entity == null)
    {
        return NotFound();
    }
    ViewBag.Categories = Repository.Categories;
    return View(entity);
}



[HttpPost]
public async Task<IActionResult> Edit(int id, Product model, IFormFile? imageFile)
{
    if (id != model.ProductID)
    {
        return NotFound();
    }

    if (ModelState.IsValid)
    {
        if (imageFile != null)
        {
            var extension = Path.GetExtension(imageFile.FileName);
            var randomFileName = $"{Guid.NewGuid()}{extension}";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            model.Image = randomFileName;
        }

        Repository.EditProduct(model);
        return RedirectToAction("Index");
    }
    ViewBag.Categories = Repository.Categories;
    return View(model);
}

public IActionResult Delete(int? id)
{
    if (id == null)
    {
        return NotFound();
    }
    var entity = Repository.Products.FirstOrDefault(p => p.ProductID == id);
    if (entity == null)
    {
        return NotFound();
    }
    
return View("DeleteConfirm", entity);
}

[HttpPost]
public IActionResult Delete (int? id , int ProductID)
{
    if (id != ProductID)
    {
        return NotFound();
    }
    var entity = Repository.Products.FirstOrDefault(p => p.ProductID == ProductID);
    if (entity == null)
    {
        return NotFound();
    }
    Repository.DeleteProduct(entity);
    return RedirectToAction("Index");
}
}

