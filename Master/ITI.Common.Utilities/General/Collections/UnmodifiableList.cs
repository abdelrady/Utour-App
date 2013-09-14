using System;
using System.Collections;

namespace ITI.Common.Utilities.General.Collections
{
    public abstract class UnmodifiableList : IList
    {
        #region -- Properties --
        public abstract int Count { get; }
        public abstract bool IsFixedSize { get; }
        public abstract bool IsSynchronized { get; }
        public virtual bool IsReadOnly
        {
            get { return true; }
        }
        public abstract object SyncRoot { get; }
        public virtual object this[int i]
        {
            get { return GetValue(i); }
            set { throw new NotSupportedException(); }
        }
        #endregion

        #region -- Constructor --
        protected UnmodifiableList()
        {
        }
        #endregion

        #region -- Public Methods --
        public virtual int Add(object o)
        {
            throw new NotSupportedException();
        }

        public virtual void Clear()
        {
            throw new NotSupportedException();
        }

        public abstract bool Contains(object o);

        public abstract void CopyTo(Array array, int index);        

        public abstract IEnumerator GetEnumerator();

        public abstract int IndexOf(object o);

        public virtual void Insert(int i, object o)
        {
            throw new NotSupportedException();
        }                

        public virtual void Remove(object o)
        {
            throw new NotSupportedException();
        }

        public virtual void RemoveAt(int i)
        {
            throw new NotSupportedException();
        }        

        protected abstract object GetValue(int i);
        #endregion
    }
}
