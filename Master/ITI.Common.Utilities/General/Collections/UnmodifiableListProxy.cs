using System;
using System.Collections;

namespace ITI.Common.Utilities.General.Collections
{
    public class UnmodifiableListProxy : UnmodifiableList
    {
        #region -- Local Variables --
        private readonly IList l;
        #endregion

        #region -- Properties --
        public override int Count
        {
            get { return l.Count; }
        }
        public override bool IsFixedSize
        {
            get { return l.IsFixedSize; }
        }

        public override bool IsSynchronized
        {
            get { return l.IsSynchronized; }
        }

        public override object SyncRoot
        {
            get { return l.SyncRoot; }
        }
        #endregion

        #region -- Constructor --
        public UnmodifiableListProxy(IList l)
        {
            this.l = l;
        }
        #endregion

        #region -- Public Methods --
        public override bool Contains(object o)
        {
            return l.Contains(o);
        }

        public override void CopyTo(Array array, int index)
        {
            l.CopyTo(array, index);
        }
        
        public override IEnumerator GetEnumerator()
        {
            return l.GetEnumerator();
        }

        public override int IndexOf(object o)
        {
            return l.IndexOf(o);
        }        

        protected override object GetValue(int i)
        {
            return l[i];
        }
        #endregion
    }
}
