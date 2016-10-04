using System.Collections.Generic;
using POS.Domain.Entities;
using POS.Domain.Infrastructure;

namespace POS.Domain.Interfaces
{
    public interface ICustomersService
    {
        int AddCustomer(Customer customer);
        void Initialize(PosContext _context);
        void SaveChanges();
        void Dispose();

        Customer FindCustomerById(int customerId);

        bool? UpdateCustomer(Customer customer);


        bool DeleteCustomer(Customer customer);

        List<Customer> GetCustomers();


    }
}