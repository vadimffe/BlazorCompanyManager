using BlazorCompanyManager.Data;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorCompanyManager.Pages
{
  public class CustomersBase : ComponentBase
  {
    [Parameter]
    public string Id { get; set; }

    [Inject]
    protected IRepository Repository { get; set; }

    private readonly IJSRuntime JSRuntime;

    protected BlazorCompanyManager.Data.Customer customer { get; set; }
    protected List<BlazorCompanyManager.Data.Customer> customers;
    protected bool showPopup;
    protected double startX, startY, offsetX, offsetY = 100;
    protected string cursor = "pointer";
    protected bool isDragging = false;
    protected bool isLoaded = false;

    public CustomersBase()
    {
      this.customer = new Data.Customer();
      this.customers = new List<Customer>();
      this.offsetY = 100;
    }

    protected async override Task OnInitializedAsync()
    {
      this.customers = await this.Repository.GetCustomersAsync();
      this.isLoaded = true;
    }

    protected void OnMouseDown(MouseEventArgs args)
    {
      startX = args.ClientX;
      startY = args.ClientY;
      cursor = "move";
      isDragging = true;
    }

    protected void OnMouseMove(MouseEventArgs args)
    {
      if (isDragging)
      {
        offsetX += args.ClientX - startX;
        offsetY += args.ClientY - startY;
        startX = args.ClientX;
        startY = args.ClientY;
      }
    }
    protected void OnMouseUp(MouseEventArgs args)
    {
      isDragging = false;
      cursor = "pointer";
    }

    protected void ShowDialog()
    {
      this.showPopup = true;
      this.customer = new Data.Customer();
    }

    protected void CloseDialog()
    {
      this.showPopup = false;
    }

    protected async Task EditRecord(Guid customer)
    {
      this.showPopup = true;
      this.customer = this.Repository.GetCustomer(customer);
      this.customers = await this.Repository.GetCustomersAsync();
    }

    protected async Task OnDelete(Guid customer)
    {
      await this.Repository.DeleteCustomerAsync(customer);
      this.customers = await this.Repository.GetCustomersAsync();
    }

    protected async Task SubmitForm()
    {
      if (customer.Id != Guid.Empty)
      {
        await this.Repository.UpdateCustomerAsync(customer);
        this.showPopup = false;
      }
      else
      {
        this.customer.Id = Guid.Empty;
        await this.Repository.AddCustomersAsync(customer);
      }

      this.customers = await this.Repository.GetCustomersAsync();
    }

    public List<Customer> GetTable()
    {
      return this.customers;
    }
  }
}
