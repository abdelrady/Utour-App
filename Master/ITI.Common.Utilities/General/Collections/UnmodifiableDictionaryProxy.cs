﻿using System.Collections;
using System;

namespace ITI.Common.Utilities.General.Collections
{
    public class UnmodifiableDictionaryProxy : UnmodifiableDictionary
    {
        #region -- Local Variables --
        private readonly IDictionary d;
        #endregion

        #region -- Properties --
        public override bool IsFixedSize
        {
            get { return d.IsFixedSize; }
        }

        public override bool IsSynchronized
        {
            get { return d.IsSynchronized; }
        }

        public override object SyncRoot
        {
            get { return d.SyncRoot; }
        }
        
        public override int Count
        {
            get { return d.Count; }
        }

        public override ICollection Keys
        {
            get { return d.Keys; }
        }

        public override ICollection Values
        {
            get { return d.Values; }
        }
        #endregion

        #region -- Constructor --
        public UnmodifiableDictionaryProxy(IDictionary d)
        {
            this.d = d;
        }
        #endregion

        #region -- Public Methods --
        public override bool Contains(object k)
        {
            return d.Contains(k);
        }

        public override void CopyTo(Array array, int index)
        {
            d.CopyTo(array, index);
        }        

        public override IDictionaryEnumerator GetEnumerator()
        {
            return d.GetEnumerator();
        }        

        protected override object GetValue(object k)
        {
            return d[k];
        }
        #endregion
    }
}