// See https://aka.ms/new-console-template for more information

using Business.Concrete;
using DataAccess.Concrete.InMemory;

ProductManager productManager = new ProductManager(new InMemoryProductDal());

foreach (var product in productManager.GetProducts())
{
    Console.WriteLine(product.ProductName);
}