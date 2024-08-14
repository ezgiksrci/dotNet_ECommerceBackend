using Castle.DynamicProxy;
using Core.Aspects.Autofac.Performance;
using System.Reflection;

namespace Core.Utilities.Interceptors
{
    /// <summary>
    /// Bu sınıf, belirli bir sınıf ve method için uygulanacak aspect'leri seçmek ve sıralamak amacıyla kullanılır.
    /// Aspect'lerin önceliğini belirler ve onları sıralar.
    /// </summary>
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        /// <summary>
        /// Belirli bir sınıf ve method için geçerli olan aspect'leri seçer.
        /// </summary>
        /// <param name="type">Aspect'lerin uygulanacağı sınıfın tipi.</param>
        /// <param name="method">Aspect'lerin uygulanacağı methodun bilgisi.</param>
        /// <param name="interceptors">Mevcut interceptors dizisi.</param>
        /// <returns>Seçilen ve önceliklerine göre sıralanan aspect'lerin bir dizisini döndürür.</returns>
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            // Sınıf seviyesindeki aspect'leri (attributes) alır.
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();

            // Method seviyesindeki aspect'leri (attributes) alır.
            var methodAttributes = type.GetMethod(method.Name).GetCustomAttributes<MethodInterceptionBaseAttribute>(true);

            // Method seviyesindeki aspect'leri sınıf seviyesindekilere ekler.
            classAttributes.AddRange(methodAttributes);

            // Her methoda varsayılan olarak bir PerformanceAspect ekler. 
            classAttributes.Add(new PerformanceAspect(5)); // 5 saniyelik bir performans limiti ayarlanmış.

            // Aspect'leri öncelik sırasına göre (Priority özelliğine göre) sıralar ve dizi olarak döndürür.
            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
