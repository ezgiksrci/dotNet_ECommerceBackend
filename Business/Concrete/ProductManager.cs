using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    // Manager => Concrete business class'lar için kullanılan terim.
    public class ProductManager : IProductService
    {
        // Bir Business Class başka sınıfları new'lemez!!!

        // Business katmanı productDal'ın türünü bilmiyor. Herhangi bir "veri erişim alternatifi" olabilir. (In memory, entity framework, dapper, nhibarnete...)
        IProductDal _productDal;

        // Construction Injection...
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public List<Product> GetAll()
        {
            return _productDal.GetAll();
        }

        public List<Product> GetAllByCategoryId(int categoryId)
        {
            return _productDal.GetAll(p => p.CategoryId == categoryId);
        }

        public List<Product> GetByUnitPrice(decimal min, decimal max)
        {
            return _productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max);
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            return _productDal.GetProductDetailDtos();
        }
    }
}
