using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Rosi.BMS.API.Core.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;

namespace Rosi.BMS.API.Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
    where TEntity : class, IEntity, new()
    where TContext : DbContext, new()
    {
        public async Task<TEntity> Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;                
                await context.SaveChangesAsync();
                return entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (var context = new TContext())
            {
                var table = context.Set<TEntity>();
                var deletedEntity = table.Find(id);
                table.Remove(deletedEntity);
                await context.SaveChangesAsync();
                return true;
            }

        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                return await context.Set<TEntity>().SingleOrDefaultAsync(filter);
            }
        }

        public async Task<IList<TEntity>> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                return filter == null
                    ? await context.Set<TEntity>().ToListAsync()
                    : await context.Set<TEntity>().Where(filter).ToListAsync();
            }
        }

        public async Task Update(TEntity entity)
        {
            using (var context = new TContext())
            {               
                var updatedEntity = context.Entry(entity);                
                updatedEntity.State = EntityState.Modified;
                await context.SaveChangesAsync();              
            }
        }
    }
}