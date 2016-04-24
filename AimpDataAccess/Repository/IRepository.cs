using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AimpDataAccess.Repository
{
    public interface IRepository<TEntity> 
        where TEntity : IEntity
    {
        TEntity Get(int id, params Expression<Func<TEntity, object>>[] includes);
        void AddOrUpdate(TEntity entity);
        void Delete(TEntity entity);
        void Delete(int idEntity);
        void DeleteRange(int[] ids);
        IQueryable<TEntity> All(params Expression<Func<TEntity, object>>[] includes);
        TEntity GetOrAdd(IDictionary<string, string> fieldValues);
    }
}
