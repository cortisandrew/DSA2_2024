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
    /// <remarks>Sealed means that you cannot inherit.
    /// The hashtable assumes that there is no multi-threading!
    /// </remarks>
    public sealed class ChainingHashtable<K, V> : IHashtable<K, V> where K : IEquatable<K>
    {
        private const int DEFAULT_ARRAY_LENGTH = 4;
        private const double MAX_LOAD_FACTOR = 0.8; // Trade-off between extra space kept by the instance vs speed of finding an element
                                                    // a max-load factor of 0.8 is fine for a chaining table
                                                    // if you have other implementations, the max-load factor might be better if changed. e.g. linear-probing should be around 0.4 or 0.6

        /// <summary>
        /// The number of elementList that are currently stored 
        /// </summary>
        public int Size { get; private set; } = 0;

        public double LoadFactor { get
            {
                return (double)Size / _array.Length;
            } 
        }

        private ChainingBucket<K, V>[] _array = new ChainingBucket<K, V>[DEFAULT_ARRAY_LENGTH];

        public void Add(K key, V value)
        {
            if (key == null)
            {
                // there is an issue here!
                throw new ArgumentNullException(nameof(key), $"The value of {nameof(key)} is NULL. The key must not be NULL!");
            }

            if (LoadFactor > MAX_LOAD_FACTOR)
            {
                // perform an array growth
                // rehashing...
                ChainingBucket<K, V>[] newArray = new ChainingBucket<K, V>[_array.Length * 2];
                List<ChainingBucket<K, V>> oldElements = getAllElementsFromArray(_array);

                if (oldElements.Count != Size)
                {
                    // check! we should be copying over as many elements as the Size of the _array!
                    throw new ApplicationException("Something went wrong in the code!");
                }

                // move each old element to the new array!
                foreach (ChainingBucket<K, V> oldElement in oldElements)
                {
                    _add(oldElement.Key, oldElement.Value, newArray, false);
                }

                _array = newArray;
            }

            _add(key, value, _array);
        }

        private List<ChainingBucket<K, V>> getAllElementsFromArray(ChainingBucket<K, V>[] array)
        {

            // find all the elementList in _array
            // copy them over to newArray with their new hash location
            List<ChainingBucket<K, V>> elementList = new List<ChainingBucket<K, V>>();

            // place all elementList in the old Element list
            for (int i = 0; i < array.Length; i++)
            {
                // We might not require this line of code
                if (array[i] == null)
                    continue;

                // add all the elemnts found at location i (remember to go through the whole linked list) to the elementList
                ChainingBucket<K, V>? cursor = array[i]; // Not NULL!

                while (cursor != null)
                {
                    elementList.Add(cursor);

                    cursor = cursor.Next;
                }
            }
            // all of the elementList in the old _array are now in the list elementList
            return elementList;
        }

        /// <summary>
        /// Private method that adds the key and value to the array passed as a parameter
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="array"></param>
        /// <exception cref="DuplicateKeyException"></exception>
        private void _add(K key, V value, ChainingBucket<K, V>[] array, bool incrementSize = true)
        {
            int hashValue = (key.GetHashCode() & 0x7FFFFFFF) // The 0x7FFFFFFF is used to bit-mask and remove the negative sign from the hashcode if any
                            % array.Length; // "compression map" that changes from the possible large result of the first hash function (GetHashCode)
                                             // and maps it to a value between 0 and array.Length - 1, inclusive

            // go to the location of the hashValue and get the bucket we find, possibly no bucket, which returns null
            ChainingBucket<K, V>? bucket = array[hashValue];

            if (bucket == null)
            {
                // the location is empty! We can add the new item here!
                array[hashValue] = new ChainingBucket<K, V>(key, value);
                if (incrementSize) { Size++; }
                return;
            }

            // The bucket is NOT empty: Handle the collision (using chaining)
            // Either the key already exists and we found the current item!
            // OR there is a collision
            // (OR BOTH)
            ChainingBucket<K, V> cursor = (ChainingBucket<K, V>)bucket!;
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
                if (cursor.Next == null)
                {
                    cursor.Next = new ChainingBucket<K, V>(key, value);
                    if (incrementSize) { Size++; }
                    return;
                }

                // move one step forward
                cursor = cursor.Next;
            }

            throw new InvalidOperationException("This line of code should never be reached! Item was NOT added!");
        }

        public V Get(K key) {
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
                throw new KeyNotFoundException();
            }

            while (bucket != null)
            {
                // key found!
                if (bucket.Key.Equals(key))
                {
                    return bucket.Value;
                }

                // this is not the key that we are looking for
                bucket = bucket.Next; // move forward one step...
            }

            // we have gone through the whole list, but found nothing!
            throw new KeyNotFoundException();
        }

        public bool ContainsKey(K key) {
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

            if (bucket == null)
            {
                return false;
            }

            while (bucket != null)
            {
                // key found!
                if (bucket.Key.Equals(key))
                {
                    return true;
                }

                // this is not the key that we are looking for
                bucket = bucket.Next; // move forward one step...
            }

            // we have gone through the whole list, but found nothing!
            return false;
        }

        public void Update(K key, V value) {
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

            if (bucket == null)
            {
                throw new KeyNotFoundException();
            }

            while (bucket != null)
            {
                // key found!
                if (bucket.Key.Equals(key))
                {
                    bucket.Value = value;
                    return;
                }

                // this is not the key that we are looking for
                bucket = bucket.Next; // move forward one step...
            }

            // we have gone through the whole list, but found nothing!
            throw new KeyNotFoundException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Return true if the item was deleted successfully!
        /// False if the key was not found</returns>
        public bool Remove(K key) {
            // search for the key
            // i) obtaining the hash value
            // ii) looking up the index with the hash value
            // iii) looping through the linked list to find where the key is (if at all!)
            // iv) if the key is found, you will need to perform a remove from the linked list! Return true and Size--
            // iv) if the key is NOT found, you return False

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

            if (bucket == null)
                return false;

            ChainingBucket<K, V>? previous = null;
            ChainingBucket<K, V>? cursor = bucket;

            while (cursor != null)
            {
                if (cursor.Key.Equals(key))
                {
                    // we have found the bucket to be deleted!
                    // perform the delete here...

                    if (previous == null)
                    {
                        // the bucket to be removed is the first item in the list!
                        
                        // cursor.Value = default!;
                        _array[hashValue] = cursor.Next!; // skip the first element
                    }
                    else
                    {
                        previous.Next = cursor.Next;
                    }

                    Size--; // we have removed one element
                    return true;
                }

                // this is NOT the bucket to be deleted - keep looking
                previous = cursor;
                cursor = cursor.Next;
            }

            // the key was not found!
            return false;
        }

    }
}
