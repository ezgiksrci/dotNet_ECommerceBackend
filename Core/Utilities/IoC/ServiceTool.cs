using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities.IoC
{
    /// <summary>
    /// ServiceTool sınıfı, bağımlılıkları çözümlemek için bir ServiceProvider oluşturulmasına ve 
    /// servislerin kayıtlı olduğu IServiceCollection'ın geri döndürülmesine yardımcı olan bir araç sağlar.
    /// </summary>
    public static class ServiceTool
    {
        /// <summary>
        /// ServiceProvider, kayıtlı servislerin örneklerini çözümlemek için kullanılan hizmet sağlayıcıyı temsil eder.
        /// </summary>
        public static IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// Create yöntemi, verilen IServiceCollection kullanılarak bir ServiceProvider oluşturur
        /// ve bu hizmet sağlayıcı ile bağımlılıkları çözümlemeye olanak tanır.
        /// </summary>
        /// <param name="services">Servislerin kaydedildiği IServiceCollection.</param>
        /// <returns>Geri döndürülen IServiceCollection, kayıtlı servisleri içerir.</returns>
        public static IServiceCollection Create(IServiceCollection services)
        {
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}
