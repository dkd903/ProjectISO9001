using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SuperSchnell.Project.Domain;

namespace SuperSchnell.Project.Mvc.Models.Customer
{
    public class EditCustomerViewModel:EntityEditViewModel
    {
        public EditCustomerViewModel()
        {
        }

        public EditCustomerViewModel(ICustomer customer)
            :base(customer.Id,customer.Version)
        {
            AccountNumber = customer.AccountNumber;
            CompanyName = customer.CompanyName;
            Street = customer.Street;
            PlaceName = customer.PlaceName;
            Zip = customer.Zip;
            City = customer.City;
            Country = customer.Country;
        }

        public string AccountNumber { get; set; }
        public string CompanyName { get; set; }
        public string Street { get; set; }
        public string PlaceName { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}