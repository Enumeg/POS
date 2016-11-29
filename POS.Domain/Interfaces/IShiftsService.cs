using System.Threading.Tasks;
using POS.Domain.Infrastructure;
using POS.Domain.Models;

namespace POS.Domain.Interfaces
{
    public interface IShiftsService: IInitializer
    {
        void CancelCloseShift(int shiftId);
        decimal CloseShift(int shiftId);
        Task<Result> GetUserCurrentShift(string userId, int machineId = 0);
        int OpenShift(string userId, int? machineId = default(int?));
    }
}