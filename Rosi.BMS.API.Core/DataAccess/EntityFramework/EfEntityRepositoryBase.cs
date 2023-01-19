using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Rosi.BMS.API.Core.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;
using System.Reflection;
using Rosi.BMS.API.Core.Entities.Concrete;

namespace Rosi.BMS.API.Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
    where TEntity : class, IEntity, new()
    where TContext : DbContext, new()
    {
        private bool TrySetProperty(object obj, string property, object value)
        {
            var asd = obj.GetType().GetProperties(BindingFlags.FlattenHierarchy | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance );
            var asd2 = obj.GetType().GetMembers();
            var prop = obj.GetType().GetProperty(property);
            
            Type type = obj.GetType();
            PropertyInfo prop22 = type.GetProperty("Rosi.BMS.API.Entities.Concrete.Zone.Name");

            if (prop != null && prop.CanWrite)
            {
                prop.SetValue(obj, value, null);
                return true;
            }
            return false;
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.Property("CreatedDate").CurrentValue = DateTime.Now;                           
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
                updatedEntity.Property("UpdatedDate").CurrentValue = DateTime.Now;
                updatedEntity.State = EntityState.Modified;
                await context.SaveChangesAsync();              
            }
        }
    }
}