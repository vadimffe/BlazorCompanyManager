using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorCompanyManager.Data
{
  public class Customer
  {
    [Key]
    public Guid Id { get; set; }
    public string CompanyName { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string PostCode { get; set; }
    public string PhoneNumber { get; set; }
    public string BusinessID { get; set; }
  }
}
