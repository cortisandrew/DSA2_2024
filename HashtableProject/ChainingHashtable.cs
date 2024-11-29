using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashtableProject
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    /// <remarks>Sealed means that you cannot inherit</remarks>
    public sealed class ChainingHashtable<K, V> : IHashtable<K, V> where K : IEquatable<K>
    {
        private const int DEFAULT_ARRAY_LENGTH = 4;

        /// <summary>
        /// The number of elements that are currently stored 
        /// </summary>
        public int Size { get; private set; } = 0;

        private ChainingBucket<K, V>[] _array = new ChainingBucket<K, V>[DEFAULT_ARRAY_LENGTH];

        public void Add(K key, V value)
        {
            if (key == null)
            {
                // there is an issue here!
                throw new ArgumentNullException(nameof(key), $"The value of {nameof(key)} is NULL. The key must not be NULL!");
            }

            int hashValue = (key.GetHashCode() & 0x7FFFFFFF) // The 0x7FFFFFFF is used to bit-mask and remove the negative sign from the hashcode if any
                            % _array.Length; // "compression map" that changes from the possible large result of the first hash function (GetHashCode)
                                             // and maps it to a value between 0 and _array.Length - 1, inclusive

            // go to the location of the hashValue and get the bucket we find, possibly no bucket, which returns null
            ChainingBucket<K, V>? bucket = _array[hashValue];

            if (bucket == null )
            {
                // the location is empty! We can add the new item here!
                _array[hashValue] = new ChainingBucket<K, V>(key, value);
                Size++;
                return;
            }

            // The bucket is NOT empty: Handle the collision (using chaining)
            // Either the key already exists and we found the current item!
            // OR there is a collision
            // (OR BOTH)
            ChainingBucket<K, V> cursor = (ChainingBucket < K, V > )bucket!;
            while (true)
            {
                // Is the bucket.Key.Equals(key)?
                if (bucket.Key.Equals(key))
                {
                    // Yes!
                    // This means that the value has already been added!
                    // You cannot add the same key twice...
                    throw new DuplicateKeyException();
                }

                // this is not the key... keep checking until, either you find an empty next or the key
                if (cursor.Next == null )
                {
                    cursor.Next = new ChainingBucket<K, V>(key, value);
                    Size++;
                    return;
                }

                // move one step forward
                cursor = cursor.Next;
            }
        }

        public V Get(K key) { throw new NotImplementedException(); }

        public bool ContainsKey(K key) { throw new NotImplementedException(); }

        public void Update(K key, V value) { throw new NotImplementedException(); }

        public bool Remove(K key) { throw new NotImplementedException(); }



    }
}
