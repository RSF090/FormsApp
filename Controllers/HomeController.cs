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
        products = products.Where(p => p.Name.ToLower().Contains(searchString.ToLower())).ToList();
    }
    if(!string.IsNullOrEmpty(category) && category != "0"){
        products = products.Where(p => p.CategoryId == int.Parse(category)).ToList();
    }
    
    ViewBag.Categories = new SelectList(Repository.Categories,"CategoryId","Name");
    return View(products);
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
