using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeShop.Core.Contracts;
using OfficeShop.Models.Order;
using System.Security.Claims;
using System.Globalization;


namespace OfficeShop.Controllers
{
    [Authorize] // трябва да си влязъл в профила, за да поръчаш
    public class OrderController : Controller
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;

        public OrderController(IProductService productService, IOrderService orderService)
        {
            _productService = productService;
            _orderService = orderService;
        }
        // GET: OrderController
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            List<OrderIndexVM> orders = _orderService.GetOrders()
                .Select(x => new OrderIndexVM
                {
                    Id = x.Id,
                    OrderDate = x.OrderDate.ToString("dd-MMM-yyyy HH:mm", CultureInfo.InvariantCulture),
                    UserId = x.UserId,
                    User = x.User.UserName,
                    ProductId = x.ProductId,
                    Product = x.Product.ProductName,
                    Picture = x.Product.Picture,
                    Quantity = x.Quantity,
                    Price = x.Price,
                    Discount = x.Discount,
                    TotalPrice = x.TotalPrice
                }).ToList();

            return View(orders);
        }


        // GET: OrderController/Create
        public ActionResult Create(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            var model = new OrderCreateVM
            {
                ProductId = product.Id,
                ProductName = product.ProductName,
                QuantityInStock = product.Quantity,
                Price = product.Price,
                Discount = product.Discount,
                Picture = product.Picture
            };

            return View(model);
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderCreateVM bindingModel)
        {
            // извличане на текущия потребител (ID от Claims)
            string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var product = _productService.GetProductById(bindingModel.ProductId);
            if (product == null || product.Quantity < bindingModel.Quantity || bindingModel.Quantity <= 0)
            {
                ModelState.AddModelError("", "Invalid quantity or product not available.");
                return RedirectToAction("Index", "Product");
            }

            if (!ModelState.IsValid)
            {
                return View(bindingModel);
            }

            _orderService.Create(bindingModel.ProductId, currentUserId, bindingModel.Quantity);
            return RedirectToAction("Index", "Product");
        }
        public IActionResult MyOrders()
        {
            string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<OrderIndexVM> orders = _orderService.GetOrdersByUser(currentUserId)
                .Select(x => new OrderIndexVM
                {
                    Id = x.Id,
                    OrderDate = x.OrderDate.ToString("dd-MMM-yyyy HH:mm", CultureInfo.InvariantCulture),
                    UserId = x.UserId,
                    User = x.User.UserName,
                    ProductId = x.ProductId,
                    Product = x.Product.ProductName,
                    Picture = x.Product.Picture,
                    Quantity = x.Quantity,
                    Price = x.Price,
                    Discount = x.Discount,
                    TotalPrice = x.TotalPrice
                }).ToList();

            return View(orders);
        }

        // GET: OrderController/Denied
        public IActionResult Denied()
        {
            return View();
        }
    }
}
