using BlazorCompanyManager.Data;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorCompanyManager.Pages
{
  public class EmployeesBase : ComponentBase
  {
    [Parameter]
    public string Id { get; set; }
    
    [Inject]
    protected IRepository Repository { get; set; }

    protected BlazorCompanyManager.Data.Employee employee { get; set; }
    protected List<BlazorCompanyManager.Data.Employee> employees;
    protected bool showPopup;

    public EmployeesBase()
    {
      this.employee = new Data.Employee();
    }

    protected override void OnInitialized()
    {
      this.employees = this.Repository.GetEmployees();
    }

    protected void ShowPopup()
    {
      this.showPopup = true;
      this.employee = new Data.Employee();
    }

    protected void ClosePopup()
    {
      this.showPopup = false;
    }

    protected void EditRecord(Guid employee)
    {
      this.showPopup = true;
      this.employee = this.Repository.GetEmployee(employee);
      this.employees = this.Repository.GetEmployees();
    }

    protected void OnDelete(Guid employee)
    {
      this.Repository.DeleteEmployee(employee);
      this.employees = this.Repository.GetEmployees();
    }

    protected void SubmitForm()
    {
      if (employee.Id != Guid.Empty)
      {
        this.Repository.UpdateEmployee(employee);
        this.showPopup = false;
      }
      else
      {
        this.employee.Id = Guid.Empty;
        this.Repository.AddEmployee(employee);
      }

      this.employees = this.Repository.GetEmployees();
    }
  }
}
