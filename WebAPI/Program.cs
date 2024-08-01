using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));


// Add services to the container.

builder.Services.AddControllers();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
