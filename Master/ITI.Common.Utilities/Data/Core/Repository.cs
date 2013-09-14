using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Objects;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using ITI.Common.Utilities.Diagnostics.Trace;
using ITI.Common.Utilities.Domain.Core;
using ITI.Common.Utilities.Domain.Core.Entities;
using ITI.Common.Utilities.Domain.Core.Specification;
using ITI.Common.Utilities.Data.Core.Extensions;

namespace ITI.Common.Utilities.Data.Core
{
    /// <summary>
    /// Default base class for repostories. This generic repository 
    /// is a default implementation of <see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/>
    /// and your specific repositories can inherit from this base class so automatically will get default implementation.
    /// IMPORTANT: Using this Base Repository class IS NOT mandatory. It is just a useful base class:
    /// You could also decide that you do not want to use this base Repository class, because sometimes you don't want a 
    /// specific Repository getting all these features and it might be wrong for a specific Repository. 
    /// For instance, you could want just read-only data methods for your Repository, etc. 
    /// in that case, just simply do not use this base class on your Repository.
    /// </summary>
    /// <typeparam name="TEntity">Type of elements in repostory</typeparam>
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class,IObjectWithChangeTracker
    {

        #region -- Local Variables --
        private ITraceManager m_TraceManager = null;
        private IQueryableUnitOfWork m_CurrentUoW;
        #endregion

        #region -- Constructor --

        /// <summary>
        /// Create a new instance of Repository
        /// </summary>
        /// <param name="traceManager">Trace Manager dependency</param>
        /// <param name="unitOfWork">A unit of work for this repository</param>
        public Repository(IQueryableUnitOfWork unitOfWork, ITraceManager traceManager)
        {
            //check preconditions
            if (unitOfWork == (IQueryableUnitOfWork)null)
                throw new ArgumentNullException("unitOfWork", ITI.Common.Utilities.Data.Core.Messages.exception_ContainerCannotBeNull);

            if (traceManager == (ITraceManager)null)
                throw new ArgumentNullException("traceManager", ITI.Common.Utilities.Data.Core.Messages.exception_TraceManagerCannotBeNull);

            //set internal values
            m_CurrentUoW = unitOfWork;
            m_TraceManager = traceManager;


            m_TraceManager.TraceInfo(
                string.Format(CultureInfo.InvariantCulture,
                             ITI.Common.Utilities.Data.Core.Messages.trace_ConstructRepository,
                             typeof(TEntity).Name));
        }

        #endregion

        #region -- IRepository<TEntity> Members --

        /// <summary>
        /// Return a unit of work in this repository
        /// </summary>
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return m_CurrentUoW as IUnitOfWork;
            }
        }

        /// <summary>
        /// <see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/>
        /// </summary>
        /// <param name="item"><see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/></param>
        public virtual void Add(TEntity item)
        {
            //check item
            if (item == (TEntity)null)
                throw new ArgumentNullException("item", ITI.Common.Utilities.Data.Core.Messages.exception_ItemArgumentIsNull);

            //add object to IObjectSet for this type

            //really for STE you have two options, addobject and
            //call to ApplyChanges in this objetSet. After 
            //review discussion feedback in our codeplex project
            //ApplyChanges is the best option because solve problems in 
            //many to many associations and AddObject method

            if (item.ChangeTracker != null
                &&
                item.ChangeTracker.State == ObjectState.Added)
            {
                m_CurrentUoW.RegisterChanges<TEntity>(item);

                m_TraceManager.TraceInfo(
                    string.Format(CultureInfo.InvariantCulture,
                                  ITI.Common.Utilities.Data.Core.Messages.trace_AddedItemRepository,
                                  typeof(TEntity).Name));
            }
            else
                throw new InvalidOperationException(ITI.Common.Utilities.Data.Core.Messages.exception_ChangeTrackerIsNullOrStateIsNOK);
        }
        /// <summary>
        /// <see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/>
        /// </summary>
        /// <param name="item"><see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/></param>
        public virtual void Remove(TEntity item)
        {
            //check item
            if (item == (TEntity)null)
                throw new ArgumentNullException("item", ITI.Common.Utilities.Data.Core.Messages.exception_ItemArgumentIsNull);


            IObjectSet<TEntity> objectSet = CreateSet();

            //Attach object to unit of work and delete this
            // this is valid only if T is a type in model
            objectSet.Attach(item);

            //delete object to IObjectSet for this type
            objectSet.DeleteObject(item);

            m_TraceManager.TraceInfo(
               string.Format(CultureInfo.InvariantCulture,
                            ITI.Common.Utilities.Data.Core.Messages.trace_DeletedItemRepository,
                            typeof(TEntity).Name));
        }

        /// <summary>
        /// <see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/>
        /// </summary>
        /// <param name="item"><see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/></param>
        public virtual void RegisterItem(TEntity item)
        {
            if (item == (TEntity)null)
                throw new ArgumentNullException("item");

            (CreateSet()).Attach(item);

            m_TraceManager.TraceInfo(
               string.Format(CultureInfo.InvariantCulture,
                            ITI.Common.Utilities.Data.Core.Messages.trace_AttachedItemToRepository,
                            typeof(TEntity).Name));
        }
        /// <summary>
        /// <see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/>
        /// </summary>
        /// <param name="item"><see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/></param>
        public virtual void Modify(TEntity item)
        {
            //check arguments
            if (item == (TEntity)null)
                throw new ArgumentNullException("item", ITI.Common.Utilities.Data.Core.Messages.exception_ItemArgumentIsNull);

            //Set modifed state if change tracker is enabled and state is not deleted
            if (item.ChangeTracker != null
                &&
                ((item.ChangeTracker.State & ObjectState.Deleted) != ObjectState.Deleted)
               )
            {
                item.MarkAsModified();
            }
            //apply changes for item object
            m_CurrentUoW.RegisterChanges(item);

            m_TraceManager.TraceInfo(
                           string.Format(CultureInfo.InvariantCulture,
                                        ITI.Common.Utilities.Data.Core.Messages.trace_AppliedChangedItemRepository,
                                        typeof(TEntity).Name));
        }
        /// <summary>
        /// <see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/>
        /// </summary>
        /// <returns><see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/></returns>
        public virtual IEnumerable<TEntity> GetAll()
        {

            m_TraceManager.TraceInfo(
                           string.Format(CultureInfo.InvariantCulture,
                                        ITI.Common.Utilities.Data.Core.Messages.trace_GetAllRepository,
                                        typeof(TEntity).Name));

            //Create IObjectSet and perform query 
            return (CreateSet()).AsEnumerable<TEntity>();
        }
        /// <summary>
        /// <see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/>
        /// </summary>
        /// <param name="specification"><see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/></param>
        /// <returns><see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/></returns>
        public virtual IEnumerable<TEntity> GetBySpec(ISpecification<TEntity> specification)
        {
            if (specification == (ISpecification<TEntity>)null)
                throw new ArgumentNullException("specification");

            m_TraceManager.TraceInfo(
                           string.Format(CultureInfo.InvariantCulture,
                                        ITI.Common.Utilities.Data.Core.Messages.trace_GetBySpec,
                                        typeof(TEntity).Name));

            return (CreateSet().Where(specification.SatisfiedBy())
                                     .AsEnumerable<TEntity>());

        }

        /// <summary>
        /// <see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/>
        /// </summary>
        /// <param name="pageIndex"><see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/></param>
        /// <param name="pageCount"><see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/></param>
        /// <param name="orderByExpression"><see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/></param>
        /// <param name="ascending"><see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/></param>
        /// <returns><see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/></returns>
        public virtual IEnumerable<TEntity> GetPagedElements<S>(int pageIndex, int pageCount, System.Linq.Expressions.Expression<Func<TEntity, S>> orderByExpression, bool ascending)
        {
            //checking arguments for this query 
            if (pageIndex < 0)
                throw new ArgumentException(ITI.Common.Utilities.Data.Core.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(ITI.Common.Utilities.Data.Core.Messages.exception_InvalidPageCount, "pageCount");

            if (orderByExpression == (Expression<Func<TEntity, S>>)null)
                throw new ArgumentNullException("orderByExpression", ITI.Common.Utilities.Data.Core.Messages.exception_OrderByExpressionCannotBeNull);

            m_TraceManager.TraceInfo(
                           string.Format(CultureInfo.InvariantCulture,
                                        ITI.Common.Utilities.Data.Core.Messages.trace_GetPagedElementsRepository,
                                        typeof(TEntity).Name, pageIndex, pageCount, orderByExpression.ToString()));

            //Create associated IObjectSet and perform query

            IObjectSet<TEntity> objectSet = CreateSet();

            return objectSet.Paginate<TEntity, S>(orderByExpression, pageIndex, pageCount, ascending);
        }


        /// <summary>
        /// <see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/>
        /// </summary>
        /// <typeparam name="S"><see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/></typeparam>
        /// <param name="pageIndex"><see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/></param>
        /// <param name="pageCount"><see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/></param>
        /// <param name="orderByExpression"><see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/></param>
        /// <param name="specification"><see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/></param>
        /// <param name="ascending"><see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/></param>
        /// <returns><see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/></returns>
        public virtual IEnumerable<TEntity> GetPagedElements<S>(int pageIndex, int pageCount, Expression<Func<TEntity, S>> orderByExpression, ISpecification<TEntity> specification, bool ascending)
        {
            //checking arguments for this query 
            if (pageIndex < 0)
                throw new ArgumentException(ITI.Common.Utilities.Data.Core.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(ITI.Common.Utilities.Data.Core.Messages.exception_InvalidPageCount, "pageCount");

            if (orderByExpression == (Expression<Func<TEntity, S>>)null)
                throw new ArgumentNullException("orderByExpression", ITI.Common.Utilities.Data.Core.Messages.exception_OrderByExpressionCannotBeNull);

            if (specification == (ISpecification<TEntity>)null)
                throw new ArgumentNullException("specification", ITI.Common.Utilities.Data.Core.Messages.exception_SpecificationIsNull);

            m_TraceManager.TraceInfo(
                           string.Format(CultureInfo.InvariantCulture,
                                        ITI.Common.Utilities.Data.Core.Messages.trace_GetPagedElementsRepository,
                                        typeof(TEntity).Name, pageIndex, pageCount, orderByExpression.ToString()));

            //Create associated IObjectSet and perform query

            IObjectSet<TEntity> objectSet = CreateSet();

            //this query cannot  use Paginate IQueryable extension method because Linq queries cannot be
            //merged with Object Builder methods. See Entity Framework documentation for more information

            return (ascending)
                                ?
                                    objectSet
                                     .Where(specification.SatisfiedBy())
                                     .OrderBy(orderByExpression)
                                     .Skip(pageIndex * pageCount)
                                     .Take(pageCount)
                                     .ToList()
                                :
                                    objectSet
                                     .Where(specification.SatisfiedBy())
                                     .OrderByDescending(orderByExpression)
                                     .Skip(pageIndex * pageCount)
                                     .Take(pageCount)
                                     .ToList();
        }

        public IQueryable<TEntity> LoadWith(Expression<Func<TEntity, object>> path)
        {
            return this.CreateSet().Include(path);
        }


        /// <summary>
        /// <see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/>
        /// </summary>
        /// <param name="filter"><see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/></param>
        /// <returns><see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/></returns>
        public virtual IEnumerable<TEntity> GetFilteredElements(Expression<Func<TEntity, bool>> filter)
        {
            //checking query arguments
            if (filter == (Expression<Func<TEntity, bool>>)null)
                throw new ArgumentNullException("filter", ITI.Common.Utilities.Data.Core.Messages.exception_FilterCannotBeNull);

            m_TraceManager.TraceInfo(
                           string.Format(CultureInfo.InvariantCulture,
                                        ITI.Common.Utilities.Data.Core.Messages.trace_GetFilteredElementsRepository,
                                        typeof(TEntity).Name, filter.ToString()));

            //Create IObjectSet and perform query
            return CreateSet().Where(filter)
                                    .ToList();
        }

        public IEnumerable<TEntity> GetFilteredElementsWith(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> path)
        {
            //checking query arguments
            if (filter == (Expression<Func<TEntity, bool>>)null)
                throw new ArgumentNullException("filter", ITI.Common.Utilities.Data.Core.Messages.exception_FilterCannotBeNull);

            m_TraceManager.TraceInfo(
                           string.Format(CultureInfo.InvariantCulture,
                                        ITI.Common.Utilities.Data.Core.Messages.trace_GetFilteredElementsRepository,
                                        typeof(TEntity).Name, filter.ToString()));

            //Create IObjectSet and perform query
            return CreateSet().Include(path).Where(filter)
                                     .ToList();
        }


        /// <summary>
        /// <see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/>
        /// </summary>
        /// <param name="filter"><see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/></param>
        /// <param name="orderByExpression"><see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/></param>
        /// <param name="ascending"><see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/></param>
        /// <returns><see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/></returns>
        public virtual IEnumerable<TEntity> GetFilteredElements<S>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, S>> orderByExpression, bool ascending)
        {
            //Checking query arguments
            if (filter == (Expression<Func<TEntity, bool>>)null)
                throw new ArgumentNullException("filter", ITI.Common.Utilities.Data.Core.Messages.exception_FilterCannotBeNull);

            if (orderByExpression == (Expression<Func<TEntity, S>>)null)
                throw new ArgumentNullException("orderByExpression", ITI.Common.Utilities.Data.Core.Messages.exception_OrderByExpressionCannotBeNull);

            m_TraceManager.TraceInfo(
                           string.Format(CultureInfo.InvariantCulture,
                                        ITI.Common.Utilities.Data.Core.Messages.trace_GetFilteredElementsRepository,
                                        typeof(TEntity).Name, filter.ToString()));

            //Create IObjectSet for this type and perform query
            IObjectSet<TEntity> objectSet = CreateSet();

            return (ascending)
                                ?
                                    objectSet
                                     .Where(filter)
                                     .OrderBy(orderByExpression)
                                     .ToList()
                                :
                                    objectSet
                                     .Where(filter)
                                     .OrderByDescending(orderByExpression)
                                     .ToList();
        }

        /// <summary>
        /// <see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/>
        /// </summary>
        /// <param name="filter"><see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/></param>
        /// <param name="pageIndex"><see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/></param>
        /// <param name="pageCount"><see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/></param>
        /// <param name="orderByExpression"><see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/></param>
        /// <param name="ascending"><see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/></param>
        /// <returns><see cref="ITI.Common.Utilities.Domain.Core.IRepository{TEntity}"/></returns>
        public virtual IEnumerable<TEntity> GetFilteredElements<S>(Expression<Func<TEntity, bool>> filter, int pageIndex, int pageCount, Expression<Func<TEntity, S>> orderByExpression, bool ascending)
        {

            //checking query arguments
            if (filter == (Expression<Func<TEntity, bool>>)null)
                throw new ArgumentNullException("filter", ITI.Common.Utilities.Data.Core.Messages.exception_FilterCannotBeNull);

            if (pageIndex < 0)
                throw new ArgumentException(ITI.Common.Utilities.Data.Core.Messages.exception_InvalidPageIndex, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(ITI.Common.Utilities.Data.Core.Messages.exception_InvalidPageCount, "pageCount");

            if (orderByExpression == (Expression<Func<TEntity, S>>)null)
                throw new ArgumentNullException("orderByExpression", ITI.Common.Utilities.Data.Core.Messages.exception_OrderByExpressionCannotBeNull);

            m_TraceManager.TraceInfo(
                           string.Format(CultureInfo.InvariantCulture,
                                        ITI.Common.Utilities.Data.Core.Messages.trace_GetFilteredPagedElementsRepository,
                                        typeof(TEntity).Name, filter.ToString(), pageIndex, pageCount, orderByExpression.ToString()));

            //Create IObjectSet for this particular type and query this
            IObjectSet<TEntity> objectSet = CreateSet();

            return (ascending)
                                ?
                                    objectSet
                                     .Where(filter)
                                     .OrderBy(orderByExpression)
                                     .Skip(pageIndex * pageCount)
                                     .Take(pageCount)
                                     .ToList()
                                :
                                    objectSet
                                     .Where(filter)
                                     .OrderByDescending(orderByExpression)
                                     .Skip(pageIndex * pageCount)
                                     .Take(pageCount)
                                     .ToList();


        }

        #endregion

        #region -- Private Methods --

        IObjectSet<TEntity> CreateSet()
        {
            if (m_CurrentUoW != (IUnitOfWork)null)
            {
                IObjectSet<TEntity> objectSet = m_CurrentUoW.CreateSet<TEntity>();

                //set merge option to underlying ObjectQuery

                ObjectQuery<TEntity> query = objectSet as ObjectQuery<TEntity>;

                if (query != null) // check if this objectset is not in memory object set for testing
                    query.MergeOption = MergeOption.NoTracking;

                return objectSet;
            }
            else
                throw new InvalidOperationException(ITI.Common.Utilities.Data.Core.Messages.exception_ContainerCannotBeNull);
        }
        #endregion
    }
}
