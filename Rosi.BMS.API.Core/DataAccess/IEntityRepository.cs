using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Rosi.BMS.API.Core.Entities;

namespace Rosi.BMS.API.Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        Task<T> Get(Expression<Func<T, bool>> filter);

        IList<T> GetList(Expression<Func<T, bool>> filter = null);

        Task<T> Add(T entity);

        Task Update(T entity);

        Task<bool> Delete(int id);
    }
}