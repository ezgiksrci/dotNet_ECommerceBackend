// See https://aka.ms/new-console-template for more information

using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

//ProductTest();
//EmployeeTest();

static void EmployeeTest()
{
    EmployeeManager employeeManager = new EmployeeManager(new EfEmployeeDal());
    var employees = employeeManager.GetAll();

    foreach (var employee in employees)
    {
        Console.WriteLine(employee.FirstName + " " + employee.LastName);
    }
}

//static void ProductTest()
//{
//    ProductManager productManager = new ProductManager(new EfProductDal());
//    //var products = productManager.GetProductDetails();

//    //foreach (var product in products.Data)
//    //{
//    //    Console.WriteLine(product.ProductName + " " + product.CategoryName);
//    //}

//    var result = productManager.GetById(9);

//}
