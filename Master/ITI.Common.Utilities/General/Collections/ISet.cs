﻿using System.Collections;

namespace ITI.Common.Utilities.General.Collections
{
    public interface ISet : ICollection
    {
        void Add(object o);
        void AddAll(IEnumerable e);
        void Clear();
        bool Contains(object o);
        bool IsEmpty { get; }
        bool IsFixedSize { get; }
        bool IsReadOnly { get; }
        void Remove(object o);
        void RemoveAll(IEnumerable e);
    }
}