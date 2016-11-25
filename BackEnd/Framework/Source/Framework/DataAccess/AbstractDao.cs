#region

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Framework.DataAccess.Repositories;
using Framework.IoC;
using LinqKit;
using Database = Microsoft.Practices.EnterpriseLibrary.Data.Database;

#endregion

namespace Framework.DataAccess
{
    public class AbstractDao<TEntity> : IDisposable, IDao<TEntity> where TEntity : class
    {
        #region Private Fields
        public Database Entlib { get; set; }
        public DataContext.DataContext _DataContext
        {
            get { return CoreContainer.Container.Resolve<DataContext.DataContext>(); }
        }
        #endregion Private Fields



        public virtual TEntity Find(params object[] keyValues)
        {
            return _DataContext.Set<TEntity>().Find(keyValues);
        }

        public virtual IQueryable<TEntity> SelectQuery(string query, params object[] parameters)
        {
            return _DataContext.Set<TEntity>().SqlQuery(query, parameters).AsQueryable();
        }

        public virtual void Insert(TEntity entity)
        {
            _DataContext.Entry(entity).State = EntityState.Added;
            _DataContext.SaveChanges();
        }

        public virtual async void InsertAsync(TEntity entity)
        {
            _DataContext.Entry(entity).State = EntityState.Added;
            await _DataContext.SaveChangesAsync();
        }

        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Insert(entity);
            }
        }

        public virtual void InsertGraphRange(IEnumerable<TEntity> entities)
        {
            _DataContext.Set<TEntity>().AddRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            _DataContext.Entry(entity).State = EntityState.Modified;
            _DataContext.SaveChanges();
            //_DataContext.Entry(entity).State = EntityState.Detached;
        }

        public virtual async void UpdateAsync(TEntity entity)
        {
            _DataContext.Entry(entity).State = EntityState.Modified;
            await _DataContext.SaveChangesAsync();
            //_DataContext.Entry(entity).State = EntityState.Detached;
        }

        public virtual void Delete(object id)
        {
            var entity = _DataContext.Set<TEntity>().Find(id);
            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            _DataContext.Entry(entity).State = EntityState.Deleted;
            _DataContext.SaveChanges();
        }

        public IQueryFluent<TEntity> Query()
        {
            return new QueryFluent<TEntity>(this);
        }

        public virtual IQueryFluent<TEntity> Query(IQueryObject<TEntity> queryObject)
        {
            return new QueryFluent<TEntity>(this, queryObject);
        }

        public virtual IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> query)
        {
            return new QueryFluent<TEntity>(this, query);
        }

        public IQueryable<TEntity> Queryable()
        {
            return _DataContext.Set<TEntity>();
        }

        public IQueryable<TEntity> StateLessQueryable()
        {
            return _DataContext.Set<TEntity>().AsNoTracking();
        }

        //public IRepository<T> GetRepository<T>() where T : class
        //{
        //    //return _unitOfWork.Repository<T>();
        //}

        public virtual async Task<TEntity> FindAsync(params object[] keyValues)
        {
            return await _DataContext.Set<TEntity>().FindAsync(keyValues);
        }

        public virtual async Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return await _DataContext.Set<TEntity>().FindAsync(cancellationToken, keyValues);
        }

        public virtual async Task<bool> DeleteAsync(params object[] keyValues)
        {
            return await DeleteAsync(CancellationToken.None, keyValues);
        }

        public virtual async Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            var entity = await FindAsync(cancellationToken, keyValues);

            if (entity == null)
            {
                return false;
            }

            _DataContext.Entry(entity).State = EntityState.Modified;
            _DataContext.Set<TEntity>().Attach(entity);

            return true;
        }

        internal IQueryable<TEntity> Select(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            IEnumerable<Expression<Func<TEntity, object>>> includes = null,
            int? page = null,
            int? pageSize = null)
        {
            IQueryable<TEntity> query = _DataContext.Set<TEntity>();

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (filter != null)
            {
                query = query.AsExpandable().Where(filter);
            }
            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            return query;
        }

        internal async Task<IEnumerable<TEntity>> SelectAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            IEnumerable<Expression<Func<TEntity, object>>> includes = null,
            int? page = null,
            int? pageSize = null)
        {
            return await Select(filter, orderBy, includes, page, pageSize).ToListAsync();
        }

        public virtual void InsertOrUpdateGraph(TEntity entity)
        {
            SyncObjectGraph(entity);
            _entitesChecked = null;
            _DataContext.Set<TEntity>().Attach(entity);
        }

        HashSet<object> _entitesChecked; // tracking of all process entities in the object graph when calling SyncObjectGraph

        private void SyncObjectGraph(object entity) // scan object graph for all 
        {
            if (_entitesChecked == null)
                _entitesChecked = new HashSet<object>();

            if (_entitesChecked.Contains(entity))
                return;

            _entitesChecked.Add(entity);

            var objectState = _DataContext.Entry(entity).State;

            if (objectState != null && objectState == EntityState.Added)
                //_context.SyncObjectState(entity);

                // Set tracking state for child collections
                foreach (var prop in entity.GetType().GetProperties())
                {
                    // Apply changes to 1-1 and M-1 properties
                    var trackableRef = prop.GetValue(entity, null) as object;
                    if (trackableRef != null)
                    {
                        if (_DataContext.Entry(trackableRef).State == EntityState.Added)
                            //_context.SyncObjectState(entity);

                            SyncObjectGraph(prop.GetValue(entity, null));
                    }

                    // Apply changes to 1-M properties
                    var items = prop.GetValue(entity, null) as IEnumerable<object>;
                    if (items == null) continue;

                    //Debug.WriteLine("Checking collection: " + prop.Name);

                    foreach (var item in items)
                        SyncObjectGraph(item);
                }
        }

        public void Dispose()
        {
            if (_DataContext != null)
                _DataContext.Dispose();
        }
    }
}