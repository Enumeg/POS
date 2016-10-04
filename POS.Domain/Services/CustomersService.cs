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
            var insert = crudService.Add(customer,c=>c.Name==customer.Name);
            if (insert) return customer.Id;
            return 0;

        }

        #endregion


        #region Find Customer By ID
       
        Customer ICustomersService.FindCustomerById(int customerId)
        {
            return crudService.Find<Customer>(customerId);   
        }

        #endregion


        #region Update Customer

        bool? ICustomersService.UpdateCustomer(Customer customer)
        {
            return crudService.Update(customer, customer.Id, c => c.Name == customer.Name);
            
        }


        #endregion

        #region Delete Customer

        bool ICustomersService.DeleteCustomer(Customer customer)
        {
            return crudService.Remove<Customer>(customer.Id);

        }


        #endregion


        #region Get All Customer

        List<Customer> ICustomersService.GetCustomers()
        {
            return context.Customers.ToList();
        }


        #endregion

    }
}
