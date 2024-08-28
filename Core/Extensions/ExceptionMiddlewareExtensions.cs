using Microsoft.AspNetCore.Builder;

namespace Core.Extensions;

// Bu sınıf, ExceptionMiddleware'i ASP.NET Core pipeline'a eklemek için uzantı metodu sağlar.
public static class ExceptionMiddlewareExtensions
{
    // ConfigureCustomExceptionMiddleware metodu, middleware'i uygulamaya ekler.
    public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
}