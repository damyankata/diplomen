using OfficeShop.Infrastructure.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeShop.Core.Contracts
{
    public interface IProductService
    {
        bool Create(string name, int brandId, int categoryId, string picture, int quantity, decimal price, decimal discount);

        bool Update(int productId, string name, int brandId, int categoryId, string picture, int quantity, decimal price, decimal discount);

        List<Product> GetProducts(string searchStringCategoryName, string searchStringBrandName);

        Product GetProductById(int productId);

        bool RemoveById(int dogproductId);

    }

}
