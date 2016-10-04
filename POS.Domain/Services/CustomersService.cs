using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Domain.Entities;
using POS.Domain.Infrastructure;
using POS.Domain.Interfaces;

namespace POS.Domain.Services
{
    public class CustomersService : ServicesBase, ICustomersService
    {
        //ADD
        //Update
        //Delete
        //Find by id
        //Get all

        int ICustomersService.AddCustomer(Customer customer)
        {
            var insert = crudService.Add(customer,c=>c.Name==customer.Name);
            if (insert) return customer.Id;
            return 0;

        }

        Customer ICustomersService.FindCustomerById(int customerId)
        {
            return crudService.Find<Customer>(customerId);   
        }
    }
}
