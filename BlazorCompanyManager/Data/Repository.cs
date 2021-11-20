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

    public async ValueTask<List<Customer>> GetCustomersAsync()
    {
      using ApplicationDbContext dbContext = this.DBContext.CreateDbContext();
      return await dbContext.CustomerTable.ToListAsync() ?? new List<Customer>();
    }

    public async Task AddCustomersAsync(Customer employee)
    {
      using ApplicationDbContext dbContext = this.DBContext.CreateDbContext();
      await dbContext.CustomerTable.AddAsync(employee);
      await dbContext.SaveChangesAsync();
    }

    public async Task DeleteCustomerAsync(Guid id)
    {
      using ApplicationDbContext dbContext = this.DBContext.CreateDbContext();
      var employee = GetCustomer(id);
      dbContext.CustomerTable.Remove(employee);
      await dbContext.SaveChangesAsync();
    }

    public async Task<bool> UpdateCustomerAsync(Customer customer)
    {
      using ApplicationDbContext dbContext = this.DBContext.CreateDbContext();
      Customer tempCustomer = dbContext.CustomerTable.FirstOrDefault(x => x.Id == customer.Id);
      if (tempCustomer != null)
      {

        tempCustomer.CompanyName = customer.CompanyName;
        tempCustomer.City = customer.City;
        tempCustomer.Address = customer.Address;
        tempCustomer.PostCode = customer.PostCode;
        tempCustomer.PhoneNumber = customer.PhoneNumber;
        tempCustomer.BusinessID = customer.BusinessID;
        await dbContext.SaveChangesAsync();
      }
      else
      {
        return false;
      }
      return true;
    }

    public Customer GetCustomer(Guid id)
    {
      using ApplicationDbContext dbContext = this.DBContext.CreateDbContext();
      return dbContext.CustomerTable.FirstOrDefault(x => x.Id == id);
    }
  }
}
