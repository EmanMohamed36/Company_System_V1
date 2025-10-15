
using Demo.DAL.Models.Shared;
using System.Linq.Expressions;

namespace Demo.DAL.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        int Add(TEntity entity);
        int Delete(TEntity entity);

        int Update(TEntity entity);

        IEnumerable<TEntity> GetAll(bool withTracking = false);
        IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector);

        TEntity? GetById(int id);

        IEnumerable<TEntity> GetIEnumerable();
        IQueryable<TEntity> GetIQueryable();

    }
}
