using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public List<Product> GetProducts()
        {
            // Logical kontrol / kural işlemleri
            // Yetkisi var mı?

            return _productDal.GetAll();
        }
    }
}
