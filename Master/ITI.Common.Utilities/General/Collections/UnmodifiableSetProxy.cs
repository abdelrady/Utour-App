using System;
using System.Collections;

namespace ITI.Common.Utilities.General.Collections
{
    public class UnmodifiableSetProxy : UnmodifiableSet
    {
        #region -- Local Variables --
        private readonly ISet s;
        #endregion

        #region -- Properties --
        public override int Count
        {
            get { return s.Count; }
        }
        public override bool IsEmpty
        {
            get { return s.IsEmpty; }
        }

        public override bool IsFixedSize
        {
            get { return s.IsFixedSize; }
        }

        public override bool IsSynchronized
        {
            get { return s.IsSynchronized; }
        }

        public override object SyncRoot
        {
            get { return s.SyncRoot; }
        }
        #endregion

        #region -- Constructor --
        public UnmodifiableSetProxy(ISet s)
        {
            this.s = s;
        }
        #endregion

        #region -- Public Methods --
        public override bool Contains(object o)
        {
            return s.Contains(o);
        }

        public override void CopyTo(Array array, int index)
        {
            s.CopyTo(array, index);
        }        

        public override IEnumerator GetEnumerator()
        {
            return s.GetEnumerator();
        }
        #endregion
    }
}
