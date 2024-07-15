using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
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

        public IResult Add(Product product)
        {
            // business codes
            // adding rules

            if (product.ProductName.Length < 2)
            {
                return new ErrorResult(Messages.ProductNameInvalid);
            }

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);
        }

        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }

        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 23)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }

            var products = _productDal.GetAll();
            return new SuccessDataResult<List<Product>>(products, Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int categoryId)
        {
            var allProductsByCategoryId = _productDal.GetAll(p => p.CategoryId == categoryId);
            return new SuccessDataResult<List<Product>>(allProductsByCategoryId);
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            var productsByUnitPrice = _productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max);
            return new SuccessDataResult<List<Product>>(productsByUnitPrice);
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            var productDetails = _productDal.GetProductDetailDtos();
            return new SuccessDataResult<List<ProductDetailDto>>(productDetails);
        }

        public IDataResult<Product> GetById(int productId)
        {
            var product = _productDal.Get(p => p.ProductId == productId);
            return new SuccessDataResult<Product>(product, Messages.ProductGetted);
        }
    }
}
