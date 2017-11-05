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
  public  class PurchasesService : ServicesBase, IPurchasesService
    {
      async Task<bool> IPurchasesService.AddPurchase(Purchase purchase)
      {
          return await CrudService.Add(purchase, p=> p.Number == purchase.Number && p.SupplierId == purchase.SupplierId);
      }

    
      
    }
}
