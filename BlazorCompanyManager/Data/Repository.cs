using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorCompanyManager.Data
{
  public class Repository : IRepository
  {
    protected readonly ApplicationDbContext dbContext;

    public Repository(ApplicationDbContext db)
    {
      this.dbContext = db;
    }

    public List<Employee> GetEmployees()
    {
      return this.dbContext.EmployeeTable.ToList();
    }

    public void AddEmployee(Employee employee)
    {
      this.dbContext.EmployeeTable.Add(employee);
      this.dbContext.SaveChanges();
    }

    public void DeleteEmployee(Guid id)
    {
      var employee = GetEmployee(id);
      this.dbContext.EmployeeTable.Remove(employee);
      this.dbContext.SaveChanges();
    }

    public bool UpdateEmployee(Employee employee)
    {
      Employee tempEmployee = this.dbContext.EmployeeTable.FirstOrDefault(x => x.Id == employee.Id);
      if (tempEmployee != null)
      {
        tempEmployee.Name = employee.Name;
        tempEmployee.Department = employee.Department;
        tempEmployee.Salary = employee.Salary;
        this.dbContext.SaveChanges();
      }
      else
      {
        return false;
      }
      return true;
    }

    public Employee GetEmployee(Guid id)
    {
      return this.dbContext.EmployeeTable.FirstOrDefault(x => x.Id == id);
    }
  }
}
