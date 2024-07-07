using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    // Dal = data access layer (.net)
    // Dao = data access object (java)
    public interface IProductDal
    {
        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);
        List<Product> GetAll();
        List<Product> GetAllByCategory(int categoryId);
    }
}
