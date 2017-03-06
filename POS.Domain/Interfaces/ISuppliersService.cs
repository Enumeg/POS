using System.Collections.Generic;
using System.Threading.Tasks;
using POS.Domain.Entities;
using POS.Domain.Infrastructure;

namespace POS.Domain.Interfaces
{
    public interface ISuppliersService : IInitializer
    {
        Task<bool> AddSupplier(Supplier supplier);
        Task<bool?> UpdateSupplier(Supplier supplier);
        Task<bool?> DeleteSupplier(int supplierId, bool removeRelatedEntities = false);
        Task<Supplier> FindSupplier(int supplierId);
        Task<List<Supplier>> GetAllSuppliers();
    }
}