using Newtonsoft.Json;

namespace Core.Extensions
{
    // Bu sınıf, hata durumunda döndürülecek olan hata detaylarını tanımlar.
    public class ErrorDetails
    {
        public string Message { get; set; } // Hata mesajını tutar.
        public int StatusCode { get; set; } // HTTP durum kodunu tutar.

        // ErrorDetails nesnesini JSON formatına dönüştürmek için ToString metodunu override ediyoruz.
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
