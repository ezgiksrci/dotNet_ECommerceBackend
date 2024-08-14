using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));


// Add services to the container.
builder.Services.AddControllers();

var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
        };
    });

builder.Services.AddDependencyResolvers(new ICoreModule[] {
    new CoreModule()
}
);

///-----------------------------------------------------------------------------
///
// .Net'deki her proje tipinde built-in IoC Container mimarisi yok. Bunun için: 
// Autofac, Ninject, CastleWindsor, StructureMap, LightInject, DryInject
// .Net'de IoC Container imkanı sunan sistemler...
// Eger AOP (Aspect Oriented Programming) yapilacaksa da bu programlara ihtiyac duyuyoruz.

// .Net'in Built-In IoC Sistemi
// IProductService isteginde bulunuldugunda IProductService olarak ProductManager new'le ve ver. Bunu da bellekte tut ve her isteyene bunu ver. 
//builder.Services.AddSingleton<IProductService, ProductManager>();
//builder.Services.AddSingleton<IProductDal, EfProductDal>();
///
///-----------------------------------------------------------------------------

// AddSingleton gibi diğer yapılar...
// - builder.Services.AddScoped
// - builder.Services.AddTransient

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Middleware -- İhtiyaç halinde projeye eklenen araya giren operasyonlar.
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
