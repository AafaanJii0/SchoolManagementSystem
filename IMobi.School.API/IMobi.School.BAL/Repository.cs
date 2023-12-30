using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMobi.School.BAL
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _dbContext;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        async Task<IEnumerable<TEntity>> IRepository<TEntity>.GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            var res = await _dbContext.Set<TEntity>().FindAsync(id);
            if (res == null)
                return null;
            return res;
        }

        Task<TEntity> IRepository<TEntity>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _dbContext.Set<TEntity>().FindAsync(id);
            if (item != null)
            {
                _dbContext.Set<TEntity>().Remove(item);
                if(await _dbContext.SaveChangesAsync() > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
