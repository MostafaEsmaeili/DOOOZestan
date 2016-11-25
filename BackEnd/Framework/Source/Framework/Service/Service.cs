using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Castle.Transactions;
using Framework.DataAccess.Repositories;

namespace Framework.Service
{
    public abstract class Service<TEntity, TDao> : IService<TEntity>
        where TEntity : class
        where TDao : IDao<TEntity>
    {
        #region Private Fields
        public TDao Dao { get; set; }
        #endregion Private Fields

        #region Constructor

        //protected Service(IDao<TEntity> repository)
        //{
        //    Dao = repository;
        //}
        #endregion Constructor

        [Transaction]
        public virtual TEntity Find(params object[] keyValues)
        {
            return Dao.Find(keyValues);
        }

        [Transaction]
        public virtual IQueryable<TEntity> SelectQuery(string query, params object[] parameters)
        {
            return Dao.SelectQuery(query, parameters).AsQueryable();
        }

        [Transaction]
        public virtual void Insert(TEntity entity)
        {
            Dao.Insert(entity);
        }

        [Transaction]
        public void InsertAsync(TEntity entity)
        {
            Dao.InsertAsync(entity);
        }

        [Transaction]
        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            Dao.InsertRange(entities);
        }

        [Transaction]
        public virtual void InsertOrUpdateGraph(TEntity entity)
        {
            Dao.InsertOrUpdateGraph(entity);
        }
        [Transaction]
        public virtual void InsertGraphRange(IEnumerable<TEntity> entities)
        {
            Dao.InsertGraphRange(entities);
        }
        [Transaction]
        public virtual void Update(TEntity entity)
        {
            Dao.Update(entity);
        }
        [Transaction]
        public void UpdateAsync(TEntity entity)
        {
            Dao.UpdateAsync(entity);
        }
        [Transaction]
        public virtual void Delete(object id) { Dao.Delete(id); }
        [Transaction]
        public virtual void Delete(TEntity entity) { Dao.Delete(entity); }
        [Transaction]
        public IQueryFluent<TEntity> Query() { return Dao.Query(); }

        public virtual IQueryFluent<TEntity> Query(IQueryObject<TEntity> queryObject) { return Dao.Query(queryObject); }

        public virtual IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> query) { return Dao.Query(query); }

        public virtual async Task<TEntity> FindAsync(params object[] keyValues) { return await Dao.FindAsync(keyValues); }

        public virtual async Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues) { return await Dao.FindAsync(cancellationToken, keyValues); }
        [Transaction]
        public virtual async Task<bool> DeleteAsync(params object[] keyValues) { return await DeleteAsync(CancellationToken.None, keyValues); }

        //IF 04/08/2014 - Before: return await DeleteAsync(cancellationToken, keyValues);
        [Transaction]
        public virtual async Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues) { return await Dao.DeleteAsync(cancellationToken, keyValues); }

        public IQueryable<TEntity> Queryable() { return Dao.Queryable(); }

        public IQueryable<TEntity> StateLessQueryable() { return Dao.StateLessQueryable(); }

    }
}