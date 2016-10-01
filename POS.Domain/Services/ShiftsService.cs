using System;
using POS.Domain.Infrastructure;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Collections.Generic;
using POS.Domain.Entities;
using POS.Domain.Models;

namespace POS.Domain.Services
{
    public class ShiftsService : ServicesBase
    {
        public async Task<Result> GetUserCurrentShift(string userId, int machineId = 0)
        {        
            var result = new Result() { Id = 0, Message = "" };
            if (machineId == 0)
            {
                var  shift = await context.Shifts.Where(s => s.UserId == userId && !s.IsClosed).FirstOrDefaultAsync();
                if (shift != null)               
                    result.Id = shift.Id;
                return result;
            }
            else
            {
                //check for an open shift for the given user on the given machine
                var shift = await context.Shifts.Where(s => s.UserId == userId && s.MachineId == machineId && !s.IsClosed).FirstOrDefaultAsync();
                if (shift != null)
                {
                    result.Id = shift.Id;
                    return result;
                }
                //check for an open shift for the given user on any machine
                shift = await context.Shifts.Where(s => s.UserId == userId && !s.IsClosed).FirstOrDefaultAsync();
                if (shift != null)
                {
                    result.Message = "there is an open shift for this user on  another machine";
                    result.Id = shift.Id;
                    return result;
                }
                //check for an open shift for the any user on the given machine
                shift = await context.Shifts.Where(s => s.MachineId == machineId && !s.IsClosed).FirstOrDefaultAsync();
                if (shift != null)
                {
                    result.Message = "there is an open shift for another user on this machine";
                    result.Id = shift.Id;
                    return result;
                }

                return result;
            }
        }

        public decimal CloseShift(int shiftId)
        {
            var shift = context.Shifts.Find(shiftId);
            shift.IsClosed = true;
            shift.EndDate = DateTime.Now;
            return shift.Balance;                  
        }

        public int OpenShift(string userId, int? machineId = null)
        {
            var settings = context.Settings.First();
            var shift = new Shift()
            {
                Balance = settings.StartBalance,
                IsClosed = false,
                MachineId = machineId,
                UserId = userId,
                StartDate = DateTime.Now
            };
            crudService.Add(shift);
            return shift.Id;
        }
    }
}