using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Aimp.DataAccess.Interfaces;
using Entities;
using Aimp.DataAccess.Ef.Interfaces;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Aimp.DataAccess.EF.Repository
{
    public class EfRepository<TEntity> : IRepository<TEntity> 
        where TEntity : Entity
    {
        private readonly DbSet<TEntity> _dbSet;
        private readonly IEfDataContext _context;
        private IQueryable<TEntity> _DbQuery()
        {
            var query = (DbQuery<TEntity>)_dbSet;
            return query.OfType<TEntity>();
        }
        private IQueryable<TEntity> _Include(Expression<Func<TEntity, object>>[] includes)
        {
            var result = _DbQuery();

            foreach (var include in includes)
                result = result.Include(include);

            return result;
        }

        private void _AddIdForForeignKey(object entity)
        {
            foreach (var iProperty in entity.GetType().GetProperties())
            {
                if (iProperty.PropertyType.GetInterface("IEntity") != null)
                {
                    if (iProperty.GetValue(entity) != null)
                    {
                        var referencesEntity = iProperty.GetValue(entity);
                        var id = ((IEntity)referencesEntity)?.Id;
                        if (id != null && id > 0)
                        {
                            var foreignKey = entity.GetType().GetProperty($"{iProperty.Name}Id");

                            if (foreignKey == null)
                                throw new Exception(
                                    $"For property [{iProperty.Name}] not found implemented foreign key [{iProperty.Name}Id]");

                            foreignKey.SetValue(entity, id, null);
                            iProperty.SetValue(entity, null,null);
                        }
                        _AddIdForForeignKey(referencesEntity);
                    }
                }
            }
        }
        public EfRepository(IEfDataContext context, DbSet<TEntity> dbSet)
        {
            _context = context;
            _dbSet = dbSet;
        }
        public IQueryable<TEntity> All(params Expression<Func<TEntity, object>>[] includes)
        {
           return _Include(includes);
        }
        public IQueryable<TEntity> All()
        {
            return _dbSet;
        }
        public void Delete(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }
        public void Delete(int idEntity)
        {
            _dbSet.RemoveRange(_dbSet.Where(x => x.Id == idEntity));
        }
        public void DeleteRange(int[] ids)
        {
            _dbSet.RemoveRange(_dbSet.Where(x => ids.Contains(x.Id)));
        }
        public TEntity Get(int id, params Expression<Func<TEntity, object>>[] includes)
        {
            return _Include(includes).FirstOrDefault(x => x.Id == id);
        }
        public TEntity Get(int id)
        {
            return _dbSet.FirstOrDefault(x => x.Id == id);
        }
        public virtual void AddOrUpdate(TEntity entity)
        {
            _AddIdForForeignKey(entity);
            _dbSet.AddOrUpdate(entity);
        }

        public TEntity GetOrAdd(IDictionary<string, string> fieldValues,string tableName = null)
        {
            if(tableName == null)
                tableName = (_context as IObjectContextAdapter).ObjectContext.CreateObjectSet<TEntity>().EntitySet.Name;

            string select = $"SELECT * FROM {tableName} ";
            string where = "WHERE 1 = 1";
           foreach(var iParam in fieldValues)
            {
                where = $"{where} AND {iParam.Key} = '{iParam.Value}'";
            }
            var result = _dbSet.SqlQuery(select + where).FirstOrDefault();
            if(result == null)
            {
                string insert = $"INSERT INTO {tableName}(";
                string columns = string.Empty;
                string values = string.Empty;
                foreach (var iParam in fieldValues)
                {
                    columns = columns + $"{iParam.Key},";
                    values = values + $"'{iParam.Value}',";
                }
                insert = insert + columns.Substring(0, columns.Length - 1);
                insert = $"{insert}) VALUES ({values.Substring(0,values.Length - 1)});";
                _context.Query(insert);

                result = _dbSet.SqlQuery(select + where).FirstOrDefault();
            }
            return result;
        }
    }
}
