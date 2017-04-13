using System.Collections.Generic;
using System.Linq;
using POS.Domain.Entities;
using POS.Domain.Infrastructure;
using System.Data.Entity;
using POS.Domain.Interfaces;
using System.Threading.Tasks;
using POS.Domain.Enums;

namespace POS.Domain.Services
{
    public class PointsService : ServicesBase, IPointsService
    {
        async Task<bool> IPointsService.AddPoint(Point point)
        {
            return await CrudService.Add(point, c => c.Name == point.Name);
        }

        async Task<bool?> IPointsService.UpdatePoint(Point point)
        {
            return await CrudService.Update(point, point.Id, c => c.Name == point.Name && c.Id != point.Id);
        }

        async Task<bool?> IPointsService.DeletePoint(int pointId, bool removeRelatedEntities)
        {
            var point = Context.Points.Include(p=>p.Damageds).FirstOrDefault(c => c.Id == pointId);
            if (point == null) return false;
            if (point.Damageds.Count > 0)
                if (removeRelatedEntities)
                    Context.Damaged.RemoveRange(point.Damageds);
                else
                    return null;
            Context.Points.Remove(point);
            await Context.SaveChangesAsync();
            return true;
        }

        async Task<Point> IPointsService.FindPoint(int pointId)
        {
            return await Context.Points.FindAsync(pointId);
        }
        async Task<List<Point>> IPointsService.GetAllPoints(PointType type)
        {
            return await Context.Points.Where(p=> p.PointType == type).ToListAsync();
        }
        async Task<List<Point>> IPointsService.GetAllPoints()
        {
            return await Context.Points.ToListAsync();
        }

    }

}
