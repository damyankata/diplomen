using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OfficeShop.Core.Contracts;
using OfficeShop.Models;
using OfficeShop.Models.Product;
using System.Diagnostics;
using System.Linq;


namespace OfficeShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;

        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public IActionResult Index()
        {
            var products = _productService.GetProducts("", "");
            var model = products.Select(p => new ProductIndexVM
            {
                Id = p.Id,
                ProductName = p.ProductName,
                BrandName = p.Brand.BrandName,
                CategoryName = p.Category.CategoryName,
                Price = p.Price,
                Discount = p.Discount,
                Picture = p.Picture
            }).ToList();

            return View(model);
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
}
