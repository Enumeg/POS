using System.Collections.Generic;
using POS.Domain.Entities;
using POS.Domain.Infrastructure;

namespace POS.Domain.Interfaces
{
    public interface ICustomersService : IInitializer
    {
        int AddCustomer(Customer customer);
     
        Customer FindCustomerById(int customerId);

        bool? UpdateCustomer(Customer customer);


        bool DeleteCustomer(Customer customer);

        List<Customer> GetCustomers();


    }
}