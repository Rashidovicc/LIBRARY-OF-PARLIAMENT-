using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EFLibrary.Data.Contexts;
using EFLibrary.Data.IRepository;
using EFLibrary.Domain.Commons;
using Microsoft.EntityFrameworkCore;

namespace EFLibrary.Data.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IAuditable
    {
        private readonly LibraryDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository()
        {
            this._dbContext = new LibraryDbContext();
            this._dbSet = this._dbContext.Set<TEntity>();
        }

        public void Dispose() => GC.SuppressFinalize(this);
        
        public Task SaveAsync() => this._dbContext.SaveChangesAsync();
        
        public virtual Task<TEntity> CreateAsync(TEntity entity) =>
            Task.FromResult(this._dbSet.Add(entity).Entity);
        
        public TEntity Update(TEntity entity) => this._dbSet.Update(entity).Entity;

        public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entity = await GetAsync(predicate);

            if (entity is null)
                return false;

            _dbSet.Remove(entity);

            return true;
        }
        
        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate) =>
            this._dbSet.FirstOrDefaultAsync(predicate);
        

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null) =>
            predicate is null ? _dbSet : _dbSet.Where(predicate);

       
    }
}