using POS.Domain.Entities;
using POS.Domain.Infrastructure;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace POS.Domain.Services
{
    public interface IExpensesService : IInitializer
    {
        Task<bool> AddExpense(Expense expense, bool saveChanges = true);
        Task<bool?> UpdateExpense(Expense expense);
        Task<bool?> DeleteExpense(int expenseId);
        Task<Expense> FindExpense(int expenseId);
        Task<List<Expense>> GetAllExpenses();
    }
    public class ExpensesService : ServicesBase, IExpensesService
    {
        async Task<bool> IExpensesService.AddExpense(Expense expense, bool saveChanges )
        {
            return await CrudService.Add(expense, saveChanges: saveChanges);
        }

        async Task<bool?> IExpensesService.UpdateExpense(Expense expense)
        {
            return await CrudService.Update(expense, expense.Id);
        }

        async Task<bool?> IExpensesService.DeleteExpense(int expenseId)
        {
            var expense = Context.Expenses.Find(expenseId);
            if (expense == null) return false;
            if (expense.AccountType == Enums.AccountType.Sales ||
                expense.AccountType == Enums.AccountType.PurchaesBack)
                return null;
            Context.Expenses.Remove(expense);
            await Context.SaveChangesAsync();
            return true;
        }

        async Task<Expense> IExpensesService.FindExpense(int expenseId)
        {
            return await Context.Expenses.FindAsync(expenseId);
        }       

        async Task<List<Expense>> IExpensesService.GetAllExpenses()
        {
            return await Context.Expenses.ToListAsync();
        }

    }

}

