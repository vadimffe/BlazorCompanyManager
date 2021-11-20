using BlazorCompanyManager.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
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
    protected double startX, startY, offsetX, offsetY;

    public EmployeesBase()
    {
      this.employee = new Data.Employee();
      this.employees = new List<Employee>();
      this.offsetY = 100;
    }

    protected void OnDragStart(DragEventArgs args)
    {
      startX = args.ClientX;
      startY = args.ClientY;
    }

    protected void OnDragEnd(DragEventArgs args)
    {
      offsetX += args.ClientX - startX;
      offsetY += args.ClientY - startY;
    }

    protected async override Task OnInitializedAsync()
    {
      this.employees = await this.Repository.GetEmployeesAsync();
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

    protected async Task EditRecord(Guid employee)
    {
      this.showPopup = true;
      this.employee = this.Repository.GetEmployee(employee);
      this.employees = await this.Repository.GetEmployeesAsync();
    }

    protected async Task OnDelete(Guid employee)
    {
      await this.Repository.DeleteEmployeeAsync(employee);
      this.employees = await this.Repository.GetEmployeesAsync();
    }

    protected async Task SubmitForm()
    {
      if (employee.Id != Guid.Empty)
      {
        await this.Repository.UpdateEmployeeAsync(employee);
        this.showPopup = false;
      }
      else
      {
        this.employee.Id = Guid.Empty;
        await this.Repository.AddEmployeeAsync(employee);
      }

      this.employees = await this.Repository.GetEmployeesAsync();
    }
  }
}
