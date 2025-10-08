
using Demo.DAL.Models.Shared;

namespace Demo.DAL.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        int Add(TEntity entity);
        int Delete(TEntity entity);

        int Update(TEntity entity);

        IEnumerable<TEntity> GetAll(bool withTracking = false);

        TEntity? GetById(int id);
    }
}
