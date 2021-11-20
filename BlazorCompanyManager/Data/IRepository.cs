using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorCompanyManager.Data
{
  public interface IRepository
  {
    public ValueTask<List<Customer>> GetCustomersAsync();

    public Customer GetCustomer(Guid id);

    public Task<bool> UpdateCustomerAsync(Customer employee);

    public Task AddCustomersAsync(Customer employee);

    public Task DeleteCustomerAsync(Guid id);
  }
}
