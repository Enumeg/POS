using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Domain.Entities;
using POS.Domain.Infrastructure;

namespace POS.Domain.Services
{
    public interface ISalesService : IInitializer
    {
        Task<bool> AddSale(Sale sale);
    }
    public class SalesService : ServicesBase, ISalesService
    {
        async Task<bool> ISalesService.AddSale(Sale sale)
        {
            return await CrudService.Add(sale, p => p.Number == sale.Number && p.CustomerId == sale.CustomerId);
        }
    }
}
