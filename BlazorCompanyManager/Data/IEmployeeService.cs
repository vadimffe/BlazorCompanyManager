using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorCompanyManager.Data
{
  public interface IEmployeeService
  {
    List<Employee> GetEmployees();

    Employee GetEmployee(Guid id);

    bool UpdateEmployee(Employee employee);

    void AddEmployee(Employee employee);

    void DeleteEmployee(Guid id);
  }
}
