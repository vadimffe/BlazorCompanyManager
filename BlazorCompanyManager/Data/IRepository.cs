using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorCompanyManager.Data
{
  public interface IRepository
  {
    public List<Employee> GetEmployees();

    public Employee GetEmployee(Guid id);

    public bool UpdateEmployee(Employee employee);

    public void AddEmployee(Employee employee);

    public void DeleteEmployee(Guid id);
  }
}
