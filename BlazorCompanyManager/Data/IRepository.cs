using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorCompanyManager.Data
{
  public interface IRepository
  {
    public ValueTask<List<Employee>> GetEmployeesAsync();

    public Employee GetEmployee(Guid id);

    public Task<bool> UpdateEmployeeAsync(Employee employee);

    public Task AddEmployeeAsync(Employee employee);

    public Task DeleteEmployeeAsync(Guid id);
  }
}
