using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    // Service => Interface'ler için kullanılan terim.
    public interface IProductService
    {
        List<Product> GetAll();
        List<Product> GetAllByCategoryId(int categoryId);
        List<Product> GetByUnitPrice(decimal min, decimal max);
        List<ProductDetailDto> GetProductDetails();
    }
}
