using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;

namespace DataAccess.Concrete.EntityFramework
{
    /// <summary>
    /// Kullanıcı verilerini yönetmek için kullanılan Entity Framework tabanlı veri erişim katmanı (DAL) sınıfıdır.
    /// Bu sınıf, temel veri erişim işlemlerini EfEntityRepositoryBase sınıfından devralır ve IUserDal arayüzünü uygular.
    /// </summary>
    public class EfUserDal : EfEntityRepositoryBase<User, NorthwindContext>, IUserDal
    {
        /// <summary>
        /// Belirli bir kullanıcıya ait olan operasyon yetkilerini (claims) getirir.
        /// Bu method, kullanıcı ile ilişkili olan tüm OperationClaim nesnelerini döner.
        /// </summary>
        /// <param name="user">Operasyon yetkileri sorgulanan kullanıcı nesnesi.</param>
        /// <returns>Kullanıcının sahip olduğu OperationClaim nesnelerinin bir listesi.</returns>
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new NorthwindContext())
            {
                // OperationClaims ile UserOperationClaims tablolarını birleştirerek,
                // belirli bir kullanıcının sahip olduğu yetkileri (claims) alır.
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim
                             {
                                 Id = operationClaim.Id,
                                 Name = operationClaim.Name
                             };

                // Sorgu sonucunu bir liste olarak döndürür.
                return result.ToList();
            }
        }
    }
}
