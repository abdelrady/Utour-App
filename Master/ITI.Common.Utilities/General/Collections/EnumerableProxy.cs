using System.Collections;
using System;

namespace ITI.Common.Utilities.General.Collections
{
    public sealed class EnumerableProxy : IEnumerable
    {
        #region -- Local Variables --
        private readonly IEnumerable inner;
        #endregion

        #region -- Constructor --
        public EnumerableProxy(IEnumerable inner)
        {
            if (inner == null)
                throw new ArgumentNullException("inner");

            this.inner = inner;
        }
        #endregion

        #region -- Public Methods --
        public IEnumerator GetEnumerator()
        {
            return inner.GetEnumerator();
        }
        #endregion
    }
}
