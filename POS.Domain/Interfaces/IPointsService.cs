using System.Collections.Generic;
using System.Threading.Tasks;
using POS.Domain.Entities;
using POS.Domain.Enums;
using POS.Domain.Infrastructure;

namespace POS.Domain.Interfaces
{
    public interface IPointsService : IInitializer
    {
        Task<bool> AddPoint(Point point);
        Task<bool?> UpdatePoint(Point point);
        Task<bool?> DeletePoint(int pointId, bool removeRelatedEntities = false);
        Task<Point> FindPoint(int pointId);
        Task<List<Point>> GetAllPoints();
        Task<List<Point>> GetAllPoints(PointType type);
    }
}