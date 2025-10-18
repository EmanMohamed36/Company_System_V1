using Demo.DAL.Data;
using Demo.DAL.Models.EmployeeModel;
using Demo.DAL.Models.Shared;
using Demo.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Repositories.Classes
{
    public class GenericRepository<TEntity>(ApplicationDbContext _context):IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
          
        }
        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            
        }
        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            
        }

        public IEnumerable<TEntity> GetAll(bool withTrack = false)
        {
            if (withTrack) return _context
                    .Set<TEntity>()
                    .Where(entity => entity.IsDeleted == false)
                    .ToList();
            else return _context
                    .Set<TEntity>()
                    .Where(entity => entity.IsDeleted == false)
                    .AsNoTracking()
                    .ToList();
        }

        IEnumerable<TResult> IGenericRepository<TEntity>.GetAll<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            return _context.Set<TEntity>().Where(entity => entity.IsDeleted == false).Select(selector).ToList();
        }

        //public TEntity? GetById(int id) => _context.Set<TEntity>().Find(id);
        public TEntity? GetById(int id) => _context.Set<TEntity>().FirstOrDefault(e => e.Id == id && e.IsDeleted == false);

        IEnumerable<TEntity> IGenericRepository<TEntity>.GetIEnumerable()
        {
            return _context.Set<TEntity>();

        }

        IQueryable<TEntity> IGenericRepository<TEntity>.GetIQueryable()
        {
            return _context.Set<TEntity>();
        }

        IEnumerable<TEntity> IGenericRepository<TEntity>.GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(entity => entity.IsDeleted == false ).Where(predicate).ToList();

        }
    }
}
