﻿using System.Collections.Generic;
using POS.Domain.Entities;
using POS.Domain.Infrastructure;
using System.Data.Entity;
using System.Threading.Tasks;
using POS.Domain.Enums;
using System.Linq;
using POS.Domain.Models;
using System;

namespace POS.Domain.Services
{
    public interface ISafeService
    {
        Task<bool> UpdateSafe(Safe Safe, Operation operation, bool saveChanges = false);
        Task<List<Safe>> GetSafes(SafeType? safeType = null);
    }



    public class SafeService : ServicesBase, ISafeService
    {
        public SafeService(PosContext context) : base(context)
        {

        }
        async Task<List<Safe>> ISafeService.GetSafes(SafeType? safeType) => safeType == null ? await Context.Safes.ToListAsync() : await Context.Safes.Where(s => s.Type == safeType).ToListAsync();

        async Task<bool> ISafeService.UpdateSafe(Safe safe, Operation operation, bool saveChanges)
        {
            var amount = safe.CurrentBalance * (operation == Operation.Put ? 1 : -1);
            var oldSafe = await Context.Safes.SingleOrDefaultAsync(s => s.Id == safe.Id);
            if (oldSafe != null)
                oldSafe.CurrentBalance += amount;
            else
            {
                safe.CurrentBalance = amount;
                Context.Safes.Add(safe);
            }

            return !saveChanges || await Context.SaveChangesAsync() > 0;
        }
    }

}

