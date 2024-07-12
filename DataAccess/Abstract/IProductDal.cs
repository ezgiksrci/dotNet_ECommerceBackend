using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    // Dal = data access layer (.net)
    // Dao = data access object (java)
    public interface IProductDal : IEntityRepository<Product>
    {
        List<ProductDetailDto> GetProductDetailDtos();
        //deneme
    }
}
