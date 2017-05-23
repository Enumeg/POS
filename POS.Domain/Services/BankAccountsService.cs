using POS.Domain.Entities;
using POS.Domain.Infrastructure;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Domain.Services
{
    public interface IBankAccountsService : IInitializer
    {
        Task<bool> AddBankAccount(BankAccount bankAccount);
        Task<bool?> UpdateBankAccount(BankAccount bankAccount);
        Task<bool?> DeleteBankAccount(int bankAccountId, bool removeRelatedEntities = false);
        Task<BankAccount> FindBankAccount(int bankAccountId);
        Task<List<BankAccount>> GetAllBankAccounts();
    }
    public class BankAccountsService : ServicesBase, IBankAccountsService
    {
        async Task<bool> IBankAccountsService.AddBankAccount(BankAccount bankAccount)
        {
            return await CrudService.Add(bankAccount, c => c.Name == bankAccount.Name);
        }

        async Task<bool?> IBankAccountsService.UpdateBankAccount(BankAccount bankAccount)
        {
            return await CrudService.Update(bankAccount, bankAccount.Id, c => c.Name == bankAccount.Name && c.Id != bankAccount.Id);
        }

        async Task<bool?> IBankAccountsService.DeleteBankAccount(int bankAccountId, bool removeRelatedEntities)
        {
            var bankAccount = Context.BankAccounts.Include(p => p.SupplierCheques).FirstOrDefault(c => c.Id == bankAccountId);
            if (bankAccount == null) return false;
            if (bankAccount.SupplierCheques.Count > 0)
                if (removeRelatedEntities)
                    Context.SupplierCheques.RemoveRange(bankAccount.SupplierCheques);
                else
                    return null;
            Context.BankAccounts.Remove(bankAccount);
            await Context.SaveChangesAsync();
            return true;
        }

        async Task<BankAccount> IBankAccountsService.FindBankAccount(int bankAccountId)
        {
            return await Context.BankAccounts.FindAsync(bankAccountId);
        }       

        async Task<List<BankAccount>> IBankAccountsService.GetAllBankAccounts()
        {
            return await Context.BankAccounts.ToListAsync();
        }

    }

}

