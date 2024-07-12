// See https://aka.ms/new-console-template for more information

using Business.Concrete;
using DataAccess.Concrete.EntityFramework;

ProductManager productManager = new ProductManager(new EfProductDal());
var products = productManager.GetByUnitPrice(5, 300);

foreach (var product in products)
{
    Console.WriteLine(product.ProductName);
}
