using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashtableProject
{
    public class ThreadSafeChainingHashTable<K, V> : IHashtable<K, V> where K : IEquatable<K>
    {
        private ReaderWriterLockSlim lockSlim = new ReaderWriterLockSlim();

        private ChainingHashtable<K, V> chainingHashtable = new ChainingHashtable<K, V>();

        public int Size
        {
            get
            {
                try
                {
                    lockSlim.EnterReadLock();
                    return chainingHashtable.Size;
                }
                finally { lockSlim.ExitReadLock();}
            }
        }

        public void Add(K key, V value)
        {
            try
            {
                lockSlim.EnterWriteLock();
                chainingHashtable.Add(key, value);
            }
            finally
            {
                lockSlim.ExitWriteLock();
            }
        }

        public bool ContainsKey(K key)
        {
            try
            {
                lockSlim.EnterReadLock();
                return chainingHashtable.ContainsKey(key);
            }
            finally { lockSlim.ExitReadLock(); }
        }

        public V Get(K key)
        {
            try
            {
                lockSlim.EnterReadLock();
                return chainingHashtable.Get(key);
            }
            finally { lockSlim.ExitReadLock(); }
        }

        public bool Remove(K key)
        {
            try
            {
                lockSlim.EnterWriteLock();
                return chainingHashtable.Remove(key);
            }
            finally
            {
                lockSlim.ExitWriteLock();
            }
        }

        public void Update(K key, V value)
        {
            try
            {
                lockSlim.EnterWriteLock();
                chainingHashtable.Update(key, value);
            }
            finally
            {
                lockSlim.ExitWriteLock();
            }
        }
    }
}
