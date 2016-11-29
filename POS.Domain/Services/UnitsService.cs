using System.Collections.Generic;
using System.Linq;
using POS.Domain.Entities;
using POS.Domain.Infrastructure;
using System.Data.Entity;
using POS.Domain.Interfaces;
using System.Threading.Tasks;

namespace POS.Domain.Services
{
    public class UnitsService : ServicesBase, IUnitsService
    {
        async Task<bool> IUnitsService.AddUnit(Unit unit)
        {
            return await CrudService.Add(unit, c => c.Name == unit.Name);
        }

        async Task<bool?> IUnitsService.UpdateUnit(Unit unit)
        {
            return await CrudService.Update(unit, unit.Id, c => c.Name == unit.Name && c.Id != unit.Id);
        }

        async Task<bool?> IUnitsService.DeleteUnit(int unitId, bool removeRelatedEntities)
        {
            var unit = Context.Units.Include(u => u.Products).FirstOrDefault(c => c.Id == unitId);
            if (unit == null) return false;
            if (unit.Products.Count > 0)
                if (removeRelatedEntities)
                    Context.Products.RemoveRange(unit.Products);
                else
                    return null;
            Context.Units.Remove(unit);
            await Context.SaveChangesAsync();
            return true;
        }

        async Task<Unit> IUnitsService.FindUnit(int unitId)
        {
            return await Context.Units.FindAsync(unitId);
        }

        async Task<List<Unit>> IUnitsService.GetAllUnits()
        {
            return await Context.Units.ToListAsync();
        }

    }

}
