namespace Core.Utilities.Security.JWT
{
    // Bu tip class'lar helper classtır. Bu yüzden isimlendirme çoğul yapılır.
    // Ama Entity Class'lar çoğul isimlendirme yapılmaz. Çünkü tekil varlıklardır.

    // Örneğin; TokenOptions'daki her bir property bir Option'dır.
    public class TokenOptions
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int AccessTokenExpiration { get; set; }
        public string SecurityKey { get; set; }
    }
}
