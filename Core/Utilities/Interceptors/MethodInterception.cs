using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{
    /// <summary>
    /// Method interception işlemleri için temel sınıf. Methodun çalışmasından önce, sonra, hata durumunda ve başarılı durumda çalışacak 
    /// işlemleri tanımlamayı sağlar. Bu sınıf, aspect yönelimli programlamada (AOP) methodların öncesinde ve sonrasında çeşitli işlemler 
    /// yapmayı kolaylaştırır.
    /// </summary>
    public class MethodInterception : MethodInterceptionBaseAttribute
    {
        /// <summary>
        /// Method çağrılmadan önce çalıştırılacak işlemleri temsil eder.
        /// Alt sınıflar bu methodu override ederek özelleştirebilir.
        /// </summary>
        /// <param name="invocation">Çağrılacak olan methodu temsil eden IInvocation nesnesi.</param>
        protected virtual void OnBefore(IInvocation invocation) { }

        /// <summary>
        /// Method çağrıldıktan sonra çalıştırılacak işlemleri temsil eder.
        /// Alt sınıflar bu methodu override ederek özelleştirebilir.
        /// </summary>
        /// <param name="invocation">Çağrılan methodu temsil eden IInvocation nesnesi.</param>
        protected virtual void OnAfter(IInvocation invocation) { }

        /// <summary>
        /// Method sırasında bir hata oluştuğunda çalıştırılacak işlemleri temsil eder.
        /// Alt sınıflar bu methodu override ederek özelleştirebilir.
        /// </summary>
        /// <param name="invocation">Çağrılan methodu temsil eden IInvocation nesnesi.</param>
        /// <param name="e">Oluşan istisna (Exception).</param>
        protected virtual void OnException(IInvocation invocation, System.Exception e) { }

        /// <summary>
        /// Method başarıyla çalıştırıldığında (hata olmadan) çalıştırılacak işlemleri temsil eder.
        /// Alt sınıflar bu methodu override ederek özelleştirebilir.
        /// </summary>
        /// <param name="invocation">Çağrılan methodu temsil eden IInvocation nesnesi.</param>
        protected virtual void OnSuccess(IInvocation invocation) { }

        /// <summary>
        /// Method interception işlemini gerçekleştirir. Methodun öncesinde, sonrasında, hata durumunda
        /// ve başarılı çalıştığında ilgili methodları çalıştırır.
        /// </summary>
        /// <param name="invocation">Çağrılan methodu temsil eden IInvocation nesnesi.</param>
        public override void Intercept(IInvocation invocation)
        {
            var isSuccess = true; // Methodun başarılı olup olmadığını izlemek için bir bayrak (flag).

            // Method çağrılmadan önce çalıştırılması istenen işlemleri yapar.
            OnBefore(invocation);

            try
            {
                // Methodun asıl işleyişini devam ettirir.
                invocation.Proceed();
            }
            catch (Exception e)
            {
                isSuccess = false; // Bir istisna oluşursa başarı bayrağını false yapar.

                // İstisna durumunda çalıştırılması istenen işlemleri yapar.
                OnException(invocation, e);

                // İstisnayı tekrar fırlatır, böylece dışarıya doğru yayılır.
                throw;
            }
            finally
            {
                // Eğer method başarıyla tamamlandıysa bu işlemleri yapar.
                if (isSuccess)
                {
                    OnSuccess(invocation);
                }
            }

            // Method çağrıldıktan sonra çalıştırılması istenen işlemleri yapar.
            OnAfter(invocation);
        }
    }
}
