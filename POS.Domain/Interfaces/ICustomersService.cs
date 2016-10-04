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
    }
}