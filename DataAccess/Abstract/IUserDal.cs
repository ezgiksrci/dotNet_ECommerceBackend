using Core.DataAccess;
using Core.Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user); // DB'den operation claim'lerini çekmek için DB'de Join atılacağı için ekstra olarak bu metodu ekledik...
    }
}