using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EFLibrary.Domain.Commons;

namespace EFLibrary.Data.IRepository
{
    public interface IGenericRepository<TEntity> : IDisposable
        where TEntity : IAuditable
    {
        Task SaveAsync();
        
        Task<TEntity> CreateAsync(TEntity entity);
        TEntity Update(TEntity entity);
        Task<bool> DeleteAsync(Expression<Func<TEntity,bool>> predicate);
        Task<TEntity> GetAsync(Expression<Func<TEntity,bool>> predicate);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null);

    }
}