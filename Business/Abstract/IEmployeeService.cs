using Entities.Concrete;

namespace Business.Abstract
{
    public interface IEmployeeService
    {
        public List<Employee> GetAll();
    }
}
