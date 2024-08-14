using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Caching
{
    /// <summary>
    /// Bu sınıf, methodların sonuçlarını cache'e alarak performans artışı sağlar.
    /// Aynı parametrelerle yapılan tekrar çağrılarda methodun sonucunu cache'den döndürür.
    /// </summary>
    public class CacheAspect : MethodInterception
    {
        private int _duration; // Cache'de tutulacak sürenin uzunluğu.
        private ICacheManager _cacheManager; // Cache işlemleri için kullanılan manager.

        /// <summary>
        /// CacheAspect constructor'ı, cache süresini ve ICacheManager'ı başlatır.
        /// Varsayılan cache süresi 60 saniyedir.
        /// </summary>
        /// <param name="duration">Cache'de tutulacak sürenin uzunluğu (saniye cinsinden).</param>
        public CacheAspect(int duration = 60)
        {
            _duration = duration;
            // ServiceTool aracılığıyla ICacheManager servisini alır.
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        /// <summary>
        /// Methodun çağrılmasını engellemeden önce cache kontrolü yapar ve daha önce cache'de varsa 
        /// cache'deki değeri döndürür. Cache'de yoksa method çalıştırılır ve sonucu cache'e eklenir.
        /// </summary>
        /// <param name="invocation">Çağrılacak olan methodu temsil eden IInvocation nesnesi.</param>
        public override void Intercept(IInvocation invocation)
        {
            // Method adı ve tam ad alanı (namespace) ile birlikte methodun adını alır.
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");

            // Methoda geçirilen argümanları bir listeye dönüştürür.
            var arguments = invocation.Arguments.ToList();

            // Method adı ve argümanları birleştirerek benzersiz bir anahtar oluşturur.
            // arguments.Select(x => x?.ToString() ?? "<Null>"):
            // - arguments listesindeki her bir öğeyi (x) ele alır.
            // - Öğenin null olup olmadığını kontrol eder (x?).
            // - Eğer öğe null değilse, ToString() methodu ile string'e çevirir.
            // - Eğer öğe null ise, "<Null>" stringini kullanır.
            // string.Join(",", ...):
            // - Yukarıdaki adımlarla elde edilen string değerleri virgülle ayırarak tek bir string yapar.
            // key:
            // - methodName ile argümanları birleştirerek methodun çağrısına özgü benzersiz bir anahtar oluşturur.
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";

            // Eğer cache'de bu anahtarla eşleşen bir değer varsa, methodu çalıştırmadan cache'deki değeri döndürür.
            if (_cacheManager.IsAdded(key))
            {
                invocation.ReturnValue = _cacheManager.Get(key);
                return;
            }
            // Eğer cache'de yoksa, methodu normal şekilde çalıştırır.
            invocation.Proceed();

            // Methodun sonucunu cache'e ekler ve belirtilen süre boyunca cache'de tutar.
            _cacheManager.Add(key, invocation.ReturnValue, _duration);
        }
    }
}
