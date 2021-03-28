using POS.Domain.Entities;
using POS.Domain.Enums;
using POS.Domain.Infrastructure;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Domain.Services
{
    public interface ISafeService : System.IDisposable
    {
        Task<bool> UpdateSafeAmount(Safe safe, Operation operation, bool saveChanges = false);
        Task<List<Safe>> GetSafes(SafeType? safeType = null);
        Task<bool> AddSafe(Safe safe);
        Task<bool?> UpdateSafe(Safe safe);
        Task<bool?> DeleteSafe(int safeId, bool removeRelatedEntities);
    }



    public class SafeService : ServicesBase, ISafeService
    {
        public SafeService(PosContext context) : base(context)
        {

        }
        async Task<List<Safe>> ISafeService.GetSafes(SafeType? safeType) => safeType == null ? await Context.Safes.ToListAsync() : await Context.Safes.Where(s => s.Type == safeType).ToListAsync();

        async Task<bool> ISafeService.UpdateSafeAmount(Safe safe, Operation operation, bool saveChanges)
        {
            var amount = safe.CurrentBalance * (operation == Operation.Put ? 1 : -1);
            var oldSafe = await Context.Safes.SingleOrDefaultAsync(s => s.Id == safe.Id);
            if (oldSafe != null)
                oldSafe.CurrentBalance += amount;
            else
            {
                safe.CurrentBalance = amount;
                Context.Safes.Add(safe);
            }

            return !saveChanges || await Context.SaveChangesAsync() > 0;
        }
        async Task<bool> ISafeService.AddSafe(Safe safe)
        {
            return await CrudService.Add(safe, c => c.ArabicName == safe.ArabicName || c.EnglishName == safe.EnglishName);
        }

        async Task<bool?> ISafeService.UpdateSafe(Safe safe)
        {
            return await CrudService.Update(safe, safe.Id, c => (c.ArabicName == safe.ArabicName || c.EnglishName == safe.EnglishName) && c.Id != safe.Id);
        }

        async Task<bool?> ISafeService.DeleteSafe(int safeId, bool removeRelatedEntities)
        {
            var safe = Context.Safes.Find(safeId);
            if (safe == null) return false;
            var transactions = Context.Transactions.Where(s => s.SafeId == safeId);
            if (transactions.Any())
            {
                if (removeRelatedEntities)
                    Context.Transactions.RemoveRange(transactions);
                else
                    return null;
            }
            var incomes = Context.Incomes.Where(s => s.SafeId == safeId);
            if (incomes.Any())
            {
                if (removeRelatedEntities)
                    Context.Incomes.RemoveRange(incomes);
                else
                    return null;
            }
            var expenses = Context.Expenses.Where(s => s.SafeId == safeId);
            if (expenses.Any())
            {
                if (removeRelatedEntities)
                    Context.Expenses.RemoveRange(expenses);
                else
                    return null;
            }
         
            Context.Safes.Remove(safe);
            await Context.SaveChangesAsync();
            return true;
        }
    }

}

