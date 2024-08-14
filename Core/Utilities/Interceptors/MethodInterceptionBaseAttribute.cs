using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{
    /// <summary>
    /// Bu sınıf, method veya sınıf seviyesinde aspect'lerin uygulanabilmesi için bir temel sınıf sağlar.
    /// Attribute olarak kullanılabilir ve Castle DynamicProxy'nin interception mekanizmasını destekler.
    /// Methodların çalıştırılma önceliğini belirlemek için 'Priority' özelliğini içerir.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor
    {
        /// <summary>
        /// Bu özellik, aspect'lerin uygulanma sırasını belirlemek için kullanılır.
        /// Daha düşük bir değer, daha yüksek öncelik anlamına gelir.
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Method interception işlemlerini gerçekleştirmek için override edilmesi gereken method.
        /// Bu method, methodun çağrılmasını engelleyebilir veya öncesinde/sonrasında işlemler yapabilir.
        /// </summary>
        /// <param name="invocation">Çağrılan methodu temsil eden IInvocation nesnesi.</param>
        public virtual void Intercept(IInvocation invocation)
        {
            // Bu method, alt sınıflar tarafından override edilerek özelleştirilir.
            // Varsayılan olarak, herhangi bir işlem yapılmaz.
        }
    }
}
