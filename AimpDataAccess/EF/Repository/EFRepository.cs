using System;
using System.Linq;
using System.Linq.Expressions;
using AimpDataAccess.Repository;
using System.Data.Entity;
using Models.Entities;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;

namespace AimpDataAccess.EF.Repository
{
    public class EFRepository<TEntity> : IRepository<TEntity> 
        where TEntity : Entity
    {
        private readonly DbSet<TEntity> _dbSet;
        private readonly IAimpEFContext _context;
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
        public EFRepository(IAimpEFContext context, DbSet<TEntity> dbSet)
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
    }
}
