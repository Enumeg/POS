using System.Collections.Generic;
using System.Threading.Tasks;
using POS.Domain.Entities;
using POS.Domain.Infrastructure;

namespace POS.Domain.Interfaces
{
    public interface ICustomersService : IInitializer
    {
        Task<bool> AddCustomer(Customer customer);
        Task<bool?> UpdateCustomer(Customer customer);
        Task<bool?> DeleteCustomer(int customerId, bool removeRelatedEntities = false);
        Task<Customer> FindCustomer(int customerId);
        Task<List<Customer>> GetAllCustomers();


    }
}