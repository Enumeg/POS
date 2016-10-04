using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;

namespace POS.Domain.Infrastructure
{
    public class CRUDService
    {
        PosContext context;
        public CRUDService(PosContext dbContext)
        {
            this.context = dbContext;
        }
        public bool Add<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> filter = null, bool saveChanges = true) where TEntity : class
        {
            if (filter != null)
            {
                if (context.Set<TEntity>().Any(filter))
                {
                    return false;
                }
            }
            context.Set<TEntity>().Add(entity);
            if (saveChanges)
                context.SaveChanges();
            return true;
        }
        public bool? Update<TEntity>(TEntity entityToUpdate, int key,Expression<Func<TEntity, bool>> filter = null, bool saveChanges = true) where TEntity : class
        {
            if (context.Set<TEntity>().Find(key) != null)
            {
                if (filter != null)
                {
                    if (context.Set<TEntity>().Any(filter))
                    {
                        return false;
                    }
                }
                context.Set<TEntity>().AddOrUpdate(entityToUpdate);
                if (saveChanges)
                    context.SaveChanges();
                return true;
            }
            else
            {
                return null;
            }

            

            
            
        }
        public void Remove<TEntity>(TEntity entityToDelete, bool saveChanges = true) where TEntity : class
        {         
            context.Set<TEntity>().Remove(entityToDelete);
            if (saveChanges)
                context.SaveChanges();
        }
        public bool Remove<TEntity>(int Id, bool saveChanges = true) where TEntity : class
        {
            var entityToDelete = context.Set<TEntity>().Find(Id);
            if (entityToDelete == null)
                return false;

            context.Set<TEntity>().Remove(entityToDelete);
            if (saveChanges)
                context.SaveChanges();
            return true;
        }
        public TEntity Find<TEntity>(object id) where TEntity : class
        {
            return context.Set<TEntity>().Find(id);
        }
        public IQueryable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class
        {
            IQueryable<TEntity> query = context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query;
        }

      
    }
}
