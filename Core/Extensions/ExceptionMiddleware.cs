using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Core.Extensions
{
    // Bu sınıf, ASP.NET Core uygulamasında tüm istekler için özel bir hata yönetimi sağlamak amacıyla kullanılan bir middleware'dir.
    public class ExceptionMiddleware
    {
        private RequestDelegate _next; // Bir sonraki middleware'e geçişi temsil eder.

        // Constructor, bir sonraki middleware'i alır.
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // HTTP isteği işlenirken ortaya çıkan hataları yakalamak ve yönetmek için InvokeAsync metodu kullanılır.
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                // Hata yoksa bir sonraki middleware'e geçilir.
                await _next(httpContext);
            }
            catch (Exception e)
            {
                // Bir hata meydana gelirse, özel hata işleme metodu çağrılır.
                await HandleExceptionAsync(httpContext, e);
            }
        }

        // Bu metod, yakalanan hatayı ele alır ve uygun bir yanıt döner.
        private Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json"; // Yanıt türünü JSON olarak ayarlar.
            httpContext.Response.StatusCode =
                (int)HttpStatusCode
                    .InternalServerError; // Varsayılan olarak 500 - Internal Server Error durum kodunu ayarlar.

            string message = "Internal Server Error"; // Genel hata mesajı.
            if (e.GetType() ==
                typeof(ValidationException)) // Eğer hata bir FluentValidation hatasıysa, özel mesajı alır.
            {
                message = e.Message;
            }

            // Hata detaylarını içeren bir JSON yanıt oluşturur ve geri döner.
            return httpContext.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = message
            }.ToString());
        }
    }
}