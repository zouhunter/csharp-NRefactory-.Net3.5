﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.NRefactory.TypeSystem;
namespace ICSharpCode.NRefactory
{
    public class Lazy<T>
    {
        public T Value { get { return getFunc(); } }
        private Func<T> getFunc;
        public Lazy(Func<T> action)
        {
            getFunc = action;
        }
    }
}
namespace System.Collections.Concurrent
{
    public class ConcurrentDictionary<T, S> : Dictionary<T, S>
    {
        ICSharpCode.NRefactory.Utils.ReferenceComparer Instance;
        public ConcurrentDictionary()
        {
        }
        public ConcurrentDictionary(ICSharpCode.NRefactory.Utils.ReferenceComparer instence)
        {
            this.Instance = instence;
        }
        public S GetOrAdd(T key, S value)
        {
            lock (Instance)
            {
                if (!ContainsKey(key))
                {
                    this[key] = value;
                }
            }
          
            return value;
        }

        public bool TryAdd(T key, S value)
        {
            lock (Instance)
            {
                if (!ContainsKey(key))
                {
                    this[key] = value;
                    return true;
                }
            }
            return false;
        }
    }

}
namespace System.Threading
{
    public class CancellationToken
    {
        public static CancellationToken None { get; set; }
        public bool IsCancellationRequested { get; set; }
        public void ThrowIfCancellationRequested()
        {
        }
    }
}

namespace ICSharpCode.NRefactory.TypeSystem
{
    public static class LazyInitializer
    {
        internal static IList<T> EnsureInitialized<T>(ref IList<T> implementedInterfaceMembers, Func<IList<T>> findImplementedInterfaceMembers)
        {
            if (implementedInterfaceMembers == null)
            {
                implementedInterfaceMembers = findImplementedInterfaceMembers();
            }
            return implementedInterfaceMembers;
        }
    }
  
}
