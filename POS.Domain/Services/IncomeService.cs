using POS.Domain.Entities;
using POS.Domain.Infrastructure;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace POS.Domain.Services
{
    public interface IIncomesService : System.IDisposable
    {
        Task<bool> AddIncome(Income income, bool saveChanges = true);
        Task<bool?> UpdateIncome(Income income);
        Task<bool?> DeleteIncome(int incomeId);
        Task<Income> FindIncome(int incomeId);
        Task<List<Income>> GetAllIncomes();
    }
    public class IncomesService : ServicesBase, IIncomesService
    {
        public IncomesService(PosContext context) : base(context)
        {

        }
        async Task<bool> IIncomesService.AddIncome(Income income, bool saveChanges )
        {
            return await CrudService.Add(income, saveChanges: saveChanges);
        }

        async Task<bool?> IIncomesService.UpdateIncome(Income income)
        {
            return await CrudService.Update(income, income.Id);
        }

        async Task<bool?> IIncomesService.DeleteIncome(int incomeId)
        {
            var income = Context.Incomes.Find(incomeId);
            if (income == null) return false;
            if (income.AccountType == Enums.AccountType.Sales ||
                income.AccountType == Enums.AccountType.PurchaesBack)
                return null;
            Context.Incomes.Remove(income);
            await Context.SaveChangesAsync();
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

