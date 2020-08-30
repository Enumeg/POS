using POS.Domain.Entities;
using POS.Domain.Infrastructure;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace POS.Domain.Services
{
    public interface IIncomesService : IInitializer
    {
        Task<bool> AddIncome(Income income, bool saveChanges = true);
        Task<bool?> UpdateIncome(Income income);
        Task<bool?> DeleteIncome(int incomeId, bool removeRelatedEntities = false);
        Task<Income> FindIncome(int incomeId);
        Task<List<Income>> GetAllIncomes();
    }
    public class IncomesService : ServicesBase, IIncomesService
    {
        async Task<bool> IIncomesService.AddIncome(Income income, bool saveChanges )
        {
            return await CrudService.Add(income, saveChanges: saveChanges);
        }

        async Task<bool?> IIncomesService.UpdateIncome(Income income)
        {
            return await CrudService.Update(income, income.Id);
        }

        async Task<bool?> IIncomesService.DeleteIncome(int incomeId, bool removeRelatedEntities)
        {
            //var income = Context.Incomes.Include(p => p.IncomeAccounts).FirstOrDefault(c => c.Id == incomeId);
            //if (income == null) return false;
            //if (income.IncomeAccounts.Count > 0)
            //    if (removeRelatedEntities)
            //        Context.IncomeAccounts.RemoveRange(income.IncomeAccounts);
            //    else
            //        return null;
            //Context.Incomes.Remove(income);
            //await Context.SaveChangesAsync();
            return true;
        }

        async Task<Income> IIncomesService.FindIncome(int incomeId)
        {
            return await Context.Incomes.FindAsync(incomeId);
        }       

        async Task<List<Income>> IIncomesService.GetAllIncomes()
        {
            return await Context.Incomes.ToListAsync();
        }

    }

}

