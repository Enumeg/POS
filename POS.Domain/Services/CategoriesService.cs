using System.Collections.Generic;
using System.Linq;
using POS.Domain.Entities;
using POS.Domain.Infrastructure;
using System.Data.Entity;
using System.Threading.Tasks;
using POS.Domain.Interfaces;

namespace POS.Domain.Services
{
    public class CategoriesService : ServicesBase, ICategoriesService
    {
        public CategoriesService(PosContext context) : base(context)
        {

        }
        async Task<bool> ICategoriesService.AddCategory(Category category)
        {
            if (await Context.Categories.AnyAsync(c => c.ArabicName == category.ArabicName || c.EnglishName == category.EnglishName))
                return false;

            Context.Categories.Add(category);
            foreach (var unit in category.Units) { Context.Entry(unit).State = EntityState.Unchanged; }
            foreach (var proprty in category.Properties) { Context.Entry(proprty).State = EntityState.Unchanged; }
            return await Context.SaveChangesAsync() > 0;

        }
        async Task<bool?> ICategoriesService.UpdateCategory(Category category)
        {
            var oldCategory = await Context.Categories.Include(c => c.Properties).Include(c => c.Units).FirstOrDefaultAsync(c => c.Id == category.Id);
            if (oldCategory == null) return null;
            if (await Context.Categories.AnyAsync(c => (c.ArabicName == category.ArabicName || c.EnglishName == category.EnglishName) && c.Id != category.Id))
                return false;
            oldCategory.ArabicName = category.ArabicName;
            oldCategory.EnglishName = category.EnglishName;
            var products = await Context.Products.Where(p => p.CategoryId == category.Id).Select(p => p.Id).ToListAsync();

            oldCategory.Properties.Where(e => category.Properties.All(p => p.Id != e.Id)).ToList().ForEach(p =>
                        {
                            Context.ProductProperties.RemoveRange(Context.ProductProperties.Where(pr => pr.PropertyId == p.Id && products.Contains(pr.ProductId)));
                            oldCategory.Properties.Remove(p);
                        });
            category.Properties.Where(e => oldCategory.Properties.All(p => p.Id != e.Id)).ToList().ForEach(p =>
            {
                Context.ProductProperties.AddRange(products.Select(id => new ProductProperty
                {
                    ProductId = id,
                    PropertyId = p.Id,
                    Value = ""
                }));
                oldCategory.Properties.Add(p);
                Context.Entry(p).State = EntityState.Unchanged;
            });
            oldCategory.Units.Where(e => category.Units.All(p => p.Id != e.Id)).ToList().ForEach(p => oldCategory.Units.Remove(p));
            category.Units.Where(e => oldCategory.Units.All(p => p.Id != e.Id)).ToList().ForEach(p =>
            {
                oldCategory.Units.Add(p);
                Context.Entry(p).State = EntityState.Unchanged;
            });

            await Context.SaveChangesAsync();
            return true;
        }
        async Task<bool?> ICategoriesService.DeleteCategory(int categoryId, bool removeRelatedEntities)
        {
            var category = Context.Categories.Include(u => u.Products).FirstOrDefault(c => c.Id == categoryId);
            if (category == null) return false;
            if (category.Products.Count > 0)
                if (removeRelatedEntities)
                    Context.Products.RemoveRange(category.Products);
                else
                    return null;
            Context.Categories.Remove(category);
            await Context.SaveChangesAsync();
            return true;
        }
        async Task<Category> ICategoriesService.FindCategory(int categoryId)
        {
            return await Context.Categories.FindAsync(categoryId);
        }
        async Task<List<Category>> ICategoriesService.GetAllCategories()
        {
            return await Context.Categories.Include(c => c.Properties).Include(c => c.Units).ToListAsync();
        }
        async Task ICategoriesService.BindPropertiesToCategory(Category category, List<Property> properties)
        {
            properties.ForEach(property => category.Properties.Add(property));
            await Context.SaveChangesAsync();
        }
        async Task ICategoriesService.BindUnitsToCategory(Category category, List<Unit> units)
        {
            units.ForEach(unit => category.Units.Add(unit));
            await Context.SaveChangesAsync();
        }

    }
}
