using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorCompanyManager.Data
{
  public class Repository : IRepository
  {
    protected virtual IDbContextFactory<ApplicationDbContext> DBContext { get; set; } = null;

    public Repository(IConfiguration configuration, IDbContextFactory<ApplicationDbContext> dbContext)
        => this.DBContext = dbContext;

    public async ValueTask<List<Employee>> GetEmployeesAsync()
    {
      using ApplicationDbContext dbContext = this.DBContext.CreateDbContext();
      return await dbContext.EmployeeTable.ToListAsync() ?? new List<Employee>();
    }

    public async Task AddEmployeeAsync(Employee employee)
    {
      using ApplicationDbContext dbContext = this.DBContext.CreateDbContext();
      await dbContext.EmployeeTable.AddAsync(employee);
      await dbContext.SaveChangesAsync();
    }

    public async Task DeleteEmployeeAsync(Guid id)
    {
      using ApplicationDbContext dbContext = this.DBContext.CreateDbContext();
      var employee = GetEmployee(id);
      dbContext.EmployeeTable.Remove(employee);
      await dbContext.SaveChangesAsync();
    }

    public async Task<bool> UpdateEmployeeAsync(Employee employee)
    {
      using ApplicationDbContext dbContext = this.DBContext.CreateDbContext();
      Employee tempEmployee = dbContext.EmployeeTable.FirstOrDefault(x => x.Id == employee.Id);
      if (tempEmployee != null)
      {
        tempEmployee.Name = employee.Name;
        tempEmployee.Department = employee.Department;
        tempEmployee.Salary = employee.Salary;
        await dbContext.SaveChangesAsync();
      }
      else
      {
        return false;
      }
      return true;
    }

    public Employee GetEmployee(Guid id)
    {
      using ApplicationDbContext dbContext = this.DBContext.CreateDbContext();
      return dbContext.EmployeeTable.FirstOrDefault(x => x.Id == id);
    }
  }
}
