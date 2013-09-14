using System;
using System.Collections;

namespace ITI.Common.Utilities.General.Collections
{
    public sealed class EmptyEnumerable : IEnumerable
    {
        #region -- Static Variables --
        public static readonly IEnumerable Instance = new EmptyEnumerable();
        #endregion

        #region -- Constructor --
        private EmptyEnumerable()
        {
        }
        #endregion

        #region -- Public Methods --
        public IEnumerator GetEnumerator()
        {
            return EmptyEnumerator.Instance;
        }
        #endregion
    }

    public sealed class EmptyEnumerator : IEnumerator
    {
        #region -- Static Methods --
        public static readonly IEnumerator Instance = new EmptyEnumerator();
        #endregion

        #region -- Properties --
        public object Current
        {
            get { throw new InvalidOperationException("No elements"); }
        }
        #endregion

        #region -- Constructor --
        private EmptyEnumerator()
        {
        }
        #endregion

        #region -- Public Methods --
        public bool MoveNext()
        {
            return false;
        }

        public void Reset()
        {
        }
        #endregion        
    }
}
