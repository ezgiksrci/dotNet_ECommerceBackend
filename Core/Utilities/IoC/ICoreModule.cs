using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities.IoC
{
    /// <summary>
    /// Uygulamanın bağımlılıkları (dependencies) için modüler bir yapı sağlamak amacıyla kullanılan bir arayüzdür.
    /// Bu arayüz, farklı modüllerin bağımlılıklarını yüklemek için kullanılır ve bağımlılık enjeksiyonu (DI) 
    /// işlemlerini kolaylaştırır.
    /// </summary>
    public interface ICoreModule
    {
        /// <summary>
        /// Modülün bağımlılıklarını IServiceCollection'a yükler.
        /// Bu method, modülün ihtiyacı olan servisleri DI konteynerine ekler.
        /// </summary>
        /// <param name="serviceCollection">Servis koleksiyonunu temsil eden IServiceCollection nesnesi.</param>
        void Load(IServiceCollection serviceCollection);
    }
}
