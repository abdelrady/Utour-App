using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace ITI.Common.Utilities.Data.Core
{
    /// <summary>
    /// In memory IObjectSet. This class is intended only
    /// for testing purposes.
    /// </summary>
    /// <typeparam name="TEntity">Type of elements in objectSet</typeparam>
    public sealed class MemorySet<TEntity> : IObjectSet<TEntity>
        where TEntity : class
    {
        #region -- Local Varaibles --

        private List<TEntity> m_InnerList;
        private List<string> m_IncludePaths;

        #endregion

        #region -- Constructor --

        /// <summary>
        /// Default constructor 
        /// </summary>
        /// <param name="innerList">A List{T} with inner values of this IObjectSet</param>
        public MemorySet(List<TEntity> innerList)
        {
            if (innerList == (List<TEntity>)null)
                throw new ArgumentNullException("innerList");

            m_InnerList = innerList;
            m_IncludePaths = new List<string>();


        }

        #endregion

        #region -- Public Methods --

        /// <summary>
        /// Include path in query objects
        /// </summary>
        /// <param name="path">Path to include</param>
        /// <returns>IObjectSet with include path</returns>
        public MemorySet<TEntity> Include(string path)
        {
            if (String.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");

            m_IncludePaths.Add(path);

            return this;
        }
        #endregion

        #region -- IObjectSet<T> Members --

        /// <summary>
        /// <see cref="System.Data.Objects.IObjectSet{T}"/>
        /// </summary>
        /// <param name="entity"><see cref="System.Data.Objects.IObjectSet{T}"/></param>
        public void AddObject(TEntity entity)
        {
            if (entity != null)
                m_InnerList.Add(entity);
        }
        /// <summary>
        /// <see cref="System.Data.Objects.IObjectSet{T}"/>
        /// </summary>
        /// <param name="entity"><see cref="System.Data.Objects.IObjectSet{T}"/></param>
        public void Attach(TEntity entity)
        {
            if (entity != null
                &&
                !m_InnerList.Contains(entity))
            {
                m_InnerList.Add(entity);
            }
        }
        /// <summary>
        /// <see cref="System.Data.Objects.IObjectSet{T}"/>
        /// </summary>
        /// <param name="entity"><see cref="System.Data.Objects.IObjectSet{T}"/></param>
        public void Detach(TEntity entity)
        {
            if (entity != null)
                m_InnerList.Remove(entity);
        }
        /// <summary>
        /// <see cref="System.Data.Objects.IObjectSet{T}"/>
        /// </summary>
        /// <param name="entity"><see cref="System.Data.Objects.IObjectSet{T}"/></param>
        public void DeleteObject(TEntity entity)
        {
            if (entity != null)
                m_InnerList.Remove(entity);
        }

        #endregion

        #region -- IEnumerable<T> Members --

        /// <summary>
        /// <see cref="System.Collections.IEnumerable.GetEnumerator"/>
        /// </summary>
        /// <returns><see cref="System.Collections.IEnumerable.GetEnumerator"/></returns>
        public IEnumerator<TEntity> GetEnumerator()
        {
            foreach (TEntity item in m_InnerList)
                yield return item;
        }

        #endregion

        #region -- IEnumerable Members --

        /// <summary>
        /// <see cref="System.Collections.IEnumerable.GetEnumerator"/>
        /// </summary>
        /// <returns><see cref="System.Collections.IEnumerable.GetEnumerator"/></returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region -- IQueryable Members --

        /// <summary>
        /// <see cref="System.Linq.IQueryable{T}"/>
        /// </summary>
        public Type ElementType
        {
            get { return typeof(TEntity); }
        }
        /// <summary>
        /// <see cref="System.Linq.IQueryable{T}"/>
        /// </summary>
        public System.Linq.Expressions.Expression Expression
        {
            get { return m_InnerList.AsQueryable().Expression; }
        }
        /// <summary>
        /// <see cref="System.Linq.IQueryable{T}"/>
        /// </summary>
        public IQueryProvider Provider
        {
            get { return m_InnerList.AsQueryable().Provider; }
        }

        #endregion
    }
}
