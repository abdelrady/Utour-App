using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ITI.Common.Utilities.General
{
    /// <summary>
    /// Modified Binding Source to avoid conflicts of multiple thread access
    /// Developed by Ramy El-Zawahry
    /// </summary>
    public class SafeBindingSounrce : BindingSource
    {
        public override int Add(object value)
        {
            lock (SyncRoot)
            {
                return base.Add(value);
            }
        }

        public override void Clear()
        {
            lock (SyncRoot)
            {
                base.Clear();
            }
        }

        public override int IndexOf(object value)
        {
            lock (SyncRoot)
            {
                return base.IndexOf(value);
            }
        }

        public override void Insert(int index, object value)
        {
            lock (SyncRoot)
            {
                base.Insert(index, value);
            }
        }

        public override void Remove(object value)
        {
            lock (SyncRoot)
            {
                base.Remove(value);
            }
        }

        public override void RemoveAt(int index)
        {
            lock (SyncRoot)
            {
                base.RemoveAt(index);
            }
        }

        public override int Count
        {
            get
            {
                lock (SyncRoot)
                {
                    return base.Count;
                }
            }
        }

        public override object this[int index]
        {
            get
            {
                lock (SyncRoot)
                {
                    return base[index];
                }
            }
            set
            {
                lock (SyncRoot)
                {
                    base[index] = value;
                }
            }
        }

        public override System.Collections.IEnumerator GetEnumerator()
        {
            lock (SyncRoot)
            {
                return base.GetEnumerator();
            }
        }
    }

}
