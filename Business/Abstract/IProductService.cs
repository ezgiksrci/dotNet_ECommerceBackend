using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    // Service => Interface'ler için kullanılan terim.
    public interface IProductService
    {
        List<Product> GetProducts();
    }
}
