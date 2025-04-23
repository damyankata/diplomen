using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace OfficeShop.Models.Brand
{
    public class BrandPairVM
    {
        public int Id { get; set; }

        [Display(Name = "Brand")]
        public string Name { get; set; } = null!;
    }

}
