using System;
using System.ComponentModel.DataAnnotations;
namespace OfficeShop.Models.Order
{
    

    public class OrderCreateVM
    {
        public int Id { get; set; }

        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        public int ProductId { get; set; }

        [Display(Name = "Product")]
        public string ProductName { get; set; } = null!;

        [Display(Name = "In Stock")]
        public int QuantityInStock { get; set; }

        [Display(Name = "Picture")]
        public string? Picture { get; set; }

        [Range(1, 100)]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Discount")]
        public decimal Discount { get; set; }

        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }
    }

}
