using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
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
        ICategoryService _categoryService;

        // Construction Injection...
        // Bir entity manager başka bir entity manager'ı inject edemez...
        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        [SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            // business codes
            // adding rules
            // Girilen product'ın yapısıyla ilgili kurallar => validation
            // Yönetimin verdiği kısıtlamalar ve iş kuralları => business

            var result = BusinessRules.Run(
                CheckCategoryIfGreaterThenMaxValue(product.CategoryId),
                CheckIfProductNameAlreadyExists(product.ProductName),
                CheckIfCategoryLimitExceeded()
            );

            if (result != null)
            {
                return result;
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

        private IResult CheckCategoryIfGreaterThenMaxValue(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result > 100)
            {
                return new ErrorResult(Messages.CategoryContainsTooManyProduct);
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductNameAlreadyExists(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Count;

            if (result > 0)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }

            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimitExceeded()
        {
            var result = _categoryService.GetAll().Data.Count;

            if (result > 100)
            {
                return new ErrorResult(Messages.CategoryLimitExceeded);
            }

            return new SuccessResult();
        }
    }
}
