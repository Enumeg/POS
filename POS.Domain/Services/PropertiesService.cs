using System.Collections.Generic;
using System.Linq;
using POS.Domain.Entities;
using POS.Domain.Infrastructure;
using System.Data.Entity;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using POS.Domain.Interfaces;
using POS.Domain.Models;

namespace POS.Domain.Services
{
    public class PropertiesService : ServicesBase, IPropertiesService
    {
        public PropertiesService(PosContext context) : base(context)
        {

        }
        async Task<bool> IPropertiesService.AddProperty(Property property)
        {
            return await CrudService.Add(property, c => (c.ArabicName == property.ArabicName || c.EnglishName == property.EnglishName));
        }

        async Task<bool?> IPropertiesService.UpdateProperty(Property property)
        {
            return await CrudService.Update(property, property.Id, p => (p.ArabicName == property.ArabicName || p.EnglishName == property.EnglishName) && p.Id != property.Id);
        }

        async Task<bool?> IPropertiesService.DeleteProperty(int propertyId, bool removeRelatedEntities)
        {
            var property = Context.Properties.Include(u => u.Products).FirstOrDefault(c => c.Id == propertyId);
            if (property == null) return false;
            if (property.Products.Count > 0)
                if (removeRelatedEntities)
                    Context.ProductProperties.RemoveRange(property.Products);
                else
                    return null;
            Context.Properties.Remove(property);
            await Context.SaveChangesAsync();
            return true;
        }

        async Task<Property> IPropertiesService.FindProperty(int propertyId)
        {
            return await Context.Properties.FindAsync(propertyId);
        }

        async Task<List<Property>> IPropertiesService.GetAllProperties()
        {
            return await Context.Properties.ToListAsync();
        }

        async Task<List<Property>> IPropertiesService.GetCategoryProperties(int categoryId)
        {
            var category = await Context.Categories.Include(c => c.Properties).FirstOrDefaultAsync(c => c.Id == categoryId);
            var ids = category.Properties.Select(p => p.Id);
            var properties = await Context.Properties.Include(p => p.Products).Where(p => ids.Contains(p.Id)).ToListAsync();
            properties.ForEach(p =>
            {
                p.Values = p.Products.Select(v => v.Value).Distinct().Select(v=> new PropertyValue { Value = v }).ToList();
                p.Products.Clear();
            });
            return properties;
        }

    }
}
