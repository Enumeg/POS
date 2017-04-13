using System.Collections.Generic;
using System.Threading.Tasks;
using POS.Domain.Entities;
using POS.Domain.Infrastructure;

namespace POS.Domain.Interfaces
{
    public interface IPurchasesService : IInitializer
    {
        Task<bool> AddPurchase(Purchase purchase);
    

    }
}