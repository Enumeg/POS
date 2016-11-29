using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace POS.Domain.Infrastructure
{
    public class CrudService
    {
        private readonly PosContext _context;
        public CrudService(PosContext context)
        {
            _context = context;
        }
        public async Task<bool> Add<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> filter = null, bool saveChanges = true) where TEntity : class
        {
            if (filter != null)
            {
                if (await _context.Set<TEntity>().AnyAsync(filter))
                {
                    return false;
                }
            }
            _context.Set<TEntity>().Add(entity);
            if (saveChanges)
                return await _context.SaveChangesAsync() > 0;
            return true;
        }
        public async Task<bool?> Update<TEntity>(TEntity entityToUpdate, int key, Expression<Func<TEntity, bool>> filter = null, bool saveChanges = true) where TEntity : class
        {
            var oldEntity = await _context.Set<TEntity>().FindAsync(key);
            if (oldEntity == null) return null;
            if (filter != null)
            {
                if (await _context.Set<TEntity>().AnyAsync(filter))
                {
                    return false;
                }
            }            
            _context.Entry(oldEntity).State = EntityState.Detached;
            _context.Entry(entityToUpdate).State = EntityState.Modified;
            if (saveChanges)
                 await _context.SaveChangesAsync();
            return true;
        }
        public void Remove<TEntity>(TEntity entityToDelete, bool saveChanges = true) where TEntity : class
        {
            _context.Set<TEntity>().Remove(entityToDelete);
            if (saveChanges)
                _context.SaveChanges();
        }
        public async Task<bool> Remove<TEntity>(int id, bool saveChanges = true) where TEntity : class
        {
            var entityToDelete = await _context.Set<TEntity>().FindAsync(id);
            if (entityToDelete == null)
                return false;

            _context.Set<TEntity>().Remove(entityToDelete);
            if (saveChanges)
                return await _context.SaveChangesAsync() > 0;
            return true;
        }
        public TEntity Find<TEntity>(object id) where TEntity : class
        {
            return _context.Set<TEntity>().Find(id);
        }
        public IQueryable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query;
        }
        public void AddCollection<TEntity>(IEnumerable<TEntity> collection, string keyProperty, bool updateOld = false) where TEntity : class
        {
            foreach (var entity in collection.Where(entity => (int)typeof(TEntity).GetProperty(keyProperty).GetValue(entity) > 0))
            {
                _context.Entry(entity).State = updateOld ? EntityState.Modified : EntityState.Unchanged;
            }
        }
        public List<TEntity> UpdateCollection<TEntity>(ICollection<TEntity> oldCollection, ICollection<TEntity> newCollection, string keyProperty, bool updateOld = false) where TEntity : class
        {
            var list = new List<TEntity>();

            foreach (var entity in oldCollection.Where(entity => !newCollection.Contains(entity)))
            {
                _context.Entry(entity).State = EntityState.Deleted;
            }
            foreach (var entity in newCollection)
            {
                if (oldCollection.Contains(entity))
                {
                    _context.Entry(entity).State = updateOld ? EntityState.Modified : EntityState.Unchanged;
                    list.Add(entity);
                }
                else
                {
                    if ((int)typeof(TEntity).GetProperty(keyProperty).GetValue(entity) > 0)
                        _context.Entry(entity).State = updateOld ? EntityState.Modified : EntityState.Unchanged;
                    else
                        _context.Entry(entity).State = EntityState.Added;
                    list.Add(entity);

                }
            }
            return list;
        }


    }
}
