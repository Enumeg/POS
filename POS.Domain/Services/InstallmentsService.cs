using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using POS.Domain.Entities;
using POS.Domain.Infrastructure;

namespace POS.Domain.Services
{
    public interface IInstallmentsService : IInitializer
    {
        Task<bool> AddInstallment(Installment installment);
        Task<bool?> UpdateInstallment(Installment installment);
        Task<bool?> DeleteInstallment(int installmentId);
        Task<Installment> FindInstallment(int installmentId);
        Task<List<Installment>> GetAllInstallments();
    }
    public class InstallmentsService : ServicesBase, IInstallmentsService
    {
        async Task<bool> IInstallmentsService.AddInstallment(Installment installment)
        {
            return await CrudService.Add(installment, c => c.TransactionId == installment.TransactionId && c.PaymentDate == installment.PaymentDate);
        }

        async Task<bool?> IInstallmentsService.UpdateInstallment(Installment installment)
        {
            return await CrudService.Update(installment, installment.Id, c => c.TransactionId == installment.TransactionId && c.PaymentDate == installment.PaymentDate);
        }

        async Task<bool?> IInstallmentsService.DeleteInstallment(int installmentId)
        {
            var installment = Context.Installments.FirstOrDefault(c => c.Id == installmentId);
            if (installment == null) return false;
            if (installment.PaymentDate != null)
                return null;
            Context.Installments.Remove(installment);
            await Context.SaveChangesAsync();
            return true;
        }

        async Task<Installment> IInstallmentsService.FindInstallment(int installmentId)
        {
            return await Context.Installments.FindAsync(installmentId);
        }

        async Task<List<Installment>> IInstallmentsService.GetAllInstallments()
        {
            return await Context.Installments.ToListAsync();
        }

    }
}
