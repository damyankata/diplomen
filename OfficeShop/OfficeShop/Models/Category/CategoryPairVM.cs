using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace OfficeShop.Models.Category
{
    public class CategoryPairVM
    {
        public int Id { get; set; }

        [Display(Name = "Category")]
        public string Name { get; set; } = null!;
    }

}
