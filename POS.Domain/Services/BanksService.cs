using POS.Domain.Entities;
using POS.Domain.Infrastructure;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Domain.Services
{
    public interface IBanksService : IInitializer
    {
        Task<bool> AddBank(Bank bank);
        Task<bool?> UpdateBank(Bank bank);
        Task<bool?> DeleteBank(int bankId, bool removeRelatedEntities = false);
        Task<Bank> FindBank(int bankId);
        Task<List<Bank>> GetAllBanks();
    }
    public class BanksService : ServicesBase, IBanksService
    {
        async Task<bool> IBanksService.AddBank(Bank bank)
        {
            return await CrudService.Add(bank, c => c.Name == bank.Name);
        }

        async Task<bool?> IBanksService.UpdateBank(Bank bank)
        {
            return await CrudService.Update(bank, bank.Id, c => c.Name == bank.Name && c.Id != bank.Id);
        }

        async Task<bool?> IBanksService.DeleteBank(int bankId, bool removeRelatedEntities)
        {
            var bank = Context.Banks.Include(p => p.BankAccounts).FirstOrDefault(c => c.Id == bankId);
            if (bank == null) return false;
            if (bank.BankAccounts.Count > 0)
                if (removeRelatedEntities)
                    Context.BankAccounts.RemoveRange(bank.BankAccounts);
                else
                    return null;
            Context.Banks.Remove(bank);
            await Context.SaveChangesAsync();
            return true;
        }

        async Task<Bank> IBanksService.FindBank(int bankId)
        {
            return await Context.Banks.FindAsync(bankId);
        }       

        async Task<List<Bank>> IBanksService.GetAllBanks()
        {
            return await Context.Banks.ToListAsync();
        }

    }

}

