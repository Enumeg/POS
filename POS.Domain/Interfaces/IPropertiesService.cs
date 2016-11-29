using System.Collections.Generic;
using System.Threading.Tasks;
using POS.Domain.Entities;
using POS.Domain.Infrastructure;

namespace POS.Domain.Interfaces
{
    public interface IPropertiesService : IInitializer
    {
        Task<bool> AddProperty(Property property);
        Task<bool?> UpdateProperty(Property property);
        Task<bool?> DeleteProperty(int propertyId, bool removeRelatedEntities = false);
        Task<Property> FindProperty(int propertyId);
        Task<List<Property>> GetAllProperties();
    }
}