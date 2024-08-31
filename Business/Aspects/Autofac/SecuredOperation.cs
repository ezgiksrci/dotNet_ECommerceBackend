using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Business.Constants;

namespace Business.Aspects.Autofac
{
    // JWT için
    // Secure Aspect'ler Business Class'ı içerisine yazılır. Çünkü her proje için farklı bir security rules olabilir.
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor; // Encapsulates all HTTP-specific information about an individual HTTP request. İstekte bulunan her client için bir context oluşturulur.


        // Bu bir ASPECT olduğu için 'constructor injection' ile 'service' enjekte edemiyoruz.(Aspect'ler sadece string arg alır.) Bu yüzden 'ServiceTool' isimli bir sınıf yazdık.
        public SecuredOperation(string roles)
        {
            _roles = roles.Split(','); // Aspect'e parametre olarak gönderilen virgül ile ayrılmış değerleri ayır ve listeye at.

            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

            // Yine örneğin;
            // Autofac'te yaptığımız IProductService injection'ını almak istersek:
            // productService = ServiceTool.ServiceProvider.GetService<IProductService>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }
            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}