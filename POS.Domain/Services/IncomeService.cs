using POS.Domain.Entities;
using POS.Domain.Infrastructure;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Domain.Services
{
    public interface IIncomesService : IInitializer
    {
        Task<bool> AddIncome(Income income);
        Task<bool?> UpdateIncome(Income income);
        Task<bool?> DeleteIncome(int incomeId, bool removeRelatedEntities = false);
        Task<Income> FindIncome(int incomeId);
        Task<List<Income>> GetAllIncomes();
    }
    public class IncomesService : ServicesBase, IIncomesService
    {
        async Task<bool> IIncomesService.AddIncome(Income income)
        {
            return await CrudService.Add(income);
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

