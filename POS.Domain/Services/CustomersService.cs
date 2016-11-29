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
        #region Add Customer
       
        int ICustomersService.AddCustomer(Customer customer)
        {
            var insert = CrudService.Add(customer,c=>c.Name==customer.Name).Result;
            if (insert) return customer.Id;
            return 0;

        }

        #endregion


        #region Find Customer By ID
       
        Customer ICustomersService.FindCustomerById(int customerId)
        {
            return CrudService.Find<Customer>(customerId);   
        }

        #endregion


        #region Update Customer

        bool? ICustomersService.UpdateCustomer(Customer customer)
        {
            return CrudService.Update(customer, customer.Id, c => c.Name == customer.Name).Result;
            
        }


        #endregion

        #region Delete Customer

        bool ICustomersService.DeleteCustomer(Customer customer)
        {
            return CrudService.Remove<Customer>(customer.Id).Result;

        }


        #endregion


        #region Get All Customer

        List<Customer> ICustomersService.GetCustomers()
        {
            return Context.Customers.ToList();
        }


        #endregion

    }
}
