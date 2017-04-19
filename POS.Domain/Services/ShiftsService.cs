using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using POS.Domain.Entities;
using POS.Domain.Infrastructure;
using POS.Domain.Interfaces;
using POS.Domain.Models;

namespace POS.Domain.Services
{
    public class ShiftsService : ServicesBase, IShiftsService
    {
        public async Task<Result> GetUserCurrentShift(string userId, int machineId = 0)
        {
            var result = new Result() { Id = 0, Message = "" };
            if (machineId == 0)
            {
                var shift = await Context.Shifts.Where(s => s.UserId == userId && s.EndDate == null).FirstOrDefaultAsync();
                if (shift != null)
                    result.Id = shift.Id;
                return result;
            }
            else
            {
                //check for an open shift for the given user on the given machine
                var shift = await Context.Shifts.Where(s => s.UserId == userId && s.MachineId == machineId && s.EndDate == null).FirstOrDefaultAsync();
                if (shift != null)
                {
                    result.Id = shift.Id;
                    return result;
                }
                //check for an open shift for the given user on any machine
                shift = await Context.Shifts.Where(s => s.UserId == userId && s.EndDate == null).FirstOrDefaultAsync();
                if (shift != null)
                {
                    result.Message = "there is an open shift for this user on  another machine";
                    result.Id = shift.Id;
                    return result;
                }
                //check for an open shift for the any user on the given machine
                shift = await Context.Shifts.Where(s => s.MachineId == machineId && s.EndDate == null).FirstOrDefaultAsync();
                if (shift == null) return result;
                result.Message = "there is an open shift for another user on this machine";
                result.Id = shift.Id;
                return result;
            }
        }

        public decimal CloseShift(int shiftId)
        {
            var shift = Context.Shifts.Find(shiftId);
            shift.IsClosing = true;
            Context.SaveChanges();

            shift.EndDate = DateTime.Now;
            return shift.Balance;
        }
        public void CancelCloseShift(int shiftId)
        {
            var shift = Context.Shifts.Find(shiftId);
            shift.IsClosing = false;
            Context.SaveChanges();
        }

        public async Task<int> OpenShift(string userId, int? machineId)
        {
            var settings = await Context.Settings.FirstAsync();
            var lastShift = machineId != null
                ? await Context.Shifts.FirstOrDefaultAsync(s => s.IsLast && s.MachineId == machineId)
                : await Context.Shifts.FirstOrDefaultAsync(s => s.IsLast);
            var shift = new Shift()
            {
                Balance = lastShift?.Balance ?? settings.StartBalance,
                MachineId = machineId,
                UserId = userId,
                StartDate = DateTime.Now
            };
            await CrudService.Add(shift);
            return shift.Id;
        }
    }
}