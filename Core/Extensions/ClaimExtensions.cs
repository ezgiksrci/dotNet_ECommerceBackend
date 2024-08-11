using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Core.Extensions
{
    // Extension Method, mevcut bir sınıfa yeni yöntemler eklemenin bir yoludur.
    // Bu yöntemler, sınıfın tanımını değiştirmeden veya ondan türetilmiş yeni bir sınıf oluşturmadan,
    // sanki sınıfın kendi yöntemleriymiş gibi çağrılabilir.

    // Özellikler:
    // 1. Static: Extension Method, statik bir sınıf içinde statik bir yöntem olarak tanımlanır.
    // 2. `this` Keyword: İlk parametre, genişletilecek olan türü belirtmek için `this` anahtar kelimesiyle başlar.
    // 3. Kullanımı: Extension Method, sanki genişletilen türün kendi yöntemiymiş gibi çağrılır.

    // Örnek:
    // Aşağıdaki örnekte, string türüne bir Extension Method eklenmiştir.

    // public static class StringExtensions
    // {
    //     public static bool IsNullOrEmpty(this string str)
    //     {
    //         return string.IsNullOrEmpty(str);
    //     }
    // }

    // Kullanım:
    // string myString = null;
    // bool result = myString.IsNullOrEmpty(); // Sanki string'in bir methoduymuş gibi

    // Neden Kullanılır?
    // - Mevcut Sınıfları, Tipleri Genişletmek: Kütüphanelerdeki veya frameworklerdeki sınıflara yeni işlevsellik eklemek için kullanışlıdır.
    // - Kod Okunabilirliği: Kodunuzu daha okunabilir ve anlaşılır hale getirir.
    // - Bakımı Kolaylaştırır: Kendi yöntemlerinizi oluşturup kullanarak kod bakımını kolaylaştırır.

    public static class ClaimExtensions
    {
        public static void AddEmail(this ICollection<Claim> claims, string email)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, email));
        }

        public static void AddName(this ICollection<Claim> claims, string name)
        {
            claims.Add(new Claim(ClaimTypes.Name, name));
        }

        public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier)
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, nameIdentifier));
        }

        public static void AddRoles(this ICollection<Claim> claims, string[] roles)
        {
            roles.ToList().ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
        }
    }
}