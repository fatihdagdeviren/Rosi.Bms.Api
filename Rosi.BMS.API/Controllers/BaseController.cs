using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Rosi.BMS.API.DataAccess.Concrete.EntityFramework.Context;
using System.Linq;
using Rosi.BMS.API.Core.Entities;

namespace Rosi.BMS.API.Controllers
{
    [ApiController]
    public class BaseController<TEntity> : ControllerBase where TEntity : BaseEntity
    {
        protected readonly RosiBMSApiDbContext _context;
        protected DbSet<TEntity> _dbSet { get; set; }
        public BaseController(RosiBMSApiDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        [HttpGet]
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        [HttpGet("{id}")]
        public virtual async Task<TEntity> GetAsync(int id)
        {
            return await _dbSet.Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        [HttpDelete("{id}")]
        public virtual async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.Where(e => e.Id == id).FirstOrDefaultAsync();
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        [HttpPost]
        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
