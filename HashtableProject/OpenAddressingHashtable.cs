using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashtableProject
{
    public class OpenAddressingHashtable<K, V> : IHashtable<K, V> where K : IEquatable<K>
    {
        private const int DEFAULT_INITIAL_LENGTH = 8;
        private const double MAX_LOAD_FACTOR = 0.6; // Trade-off between extra space kept by the instance vs speed of finding an element
        
        private OpenAddressingBucket<K, V>?[] _array;

        public OpenAddressingHashtable(int initialArraySize = DEFAULT_INITIAL_LENGTH)
        {
            _array = new OpenAddressingBucket<K, V>?[initialArraySize];
        }

        /// <summary>
        /// Number of elements stored, initially this is 0
        /// </summary>
        public int Size
        {
            get;
            private set;
        } = 0;

        public double LoadFactor
        {
            get
            {
                return (double)Size / _array.Length;
            }
        }

        public void Add(K key, V value)
        {

            if (key == null)
            {
                // there is an issue here!
                throw new ArgumentNullException(nameof(key), $"The value of {nameof(key)} is NULL. The key must not be NULL!");
            }

            if (LoadFactor > MAX_LOAD_FACTOR)
            {
                throw new NotImplementedException();
            }

            _add(key, value, _array);
        }

        private void _add(K key, V value, OpenAddressingBucket<K, V>?[] array)
        {

            int hashValue = (key.GetHashCode() & 0x7FFFFFFF) // The 0x7FFFFFFF is used to bit-mask and remove the negative sign from the hashcode if any
                            % array.Length; // "compression map" that changes from the possible large result of the first hash function (GetHashCode)
                                            // and maps it to a value between 0 and array.Length - 1, inclusive

            int arrayLocationCursor = hashValue;
            int writeAttempts = 0;

            while (writeAttempts < array.Length)
            {
                OpenAddressingBucket<K, V>? bucket = array[arrayLocationCursor];
                
                // try to add at this location
                if (bucket == null)
                {
                    // there is an empty space at the current arrayLocationCursor
                    array[arrayLocationCursor] = new OpenAddressingBucket<K, V>(key, value);
                    Size++;

                    // Added the item successfully!
                    return;
                }

                // bucket != null
                OpenAddressingBucket<K, V> bucketValue = bucket.Value;
                
                if (bucketValue.IsDeleted)
                {
                    // the current bucket is not in use!
                    // This means that I can replace the bucket with a useful one
                    array[arrayLocationCursor] = new OpenAddressingBucket<K, V>(key, value);
                    Size++;

                    // Added the item successfully!
                    return;
                }

                // this space is currently in-use and therefore cannot be deleted
                // find a new location
                arrayLocationCursor = (arrayLocationCursor + 1) % array.Length;
                writeAttempts++;
            }

            // writeAttempts == array.Length
            // we tried to write in every single location
            // the array is full and we cannot add
            // Is there an issue in the code? The hashtable should be keeping empty spaces because of the MAX_LOAD_FACTOR
            throw new ApplicationException("The item could not be added to the hashtable!");
        }

        public bool ContainsKey(K key)
        {
            throw new NotImplementedException();
        }

        public V Get(K key)
        {
            if (key == null)
            {
                // there is an issue here!
                throw new ArgumentNullException(nameof(key), $"The value of {nameof(key)} is NULL. The key must not be NULL!");
            }

            int hashValue = (key.GetHashCode() & 0x7FFFFFFF) // The 0x7FFFFFFF is used to bit-mask and remove the negative sign from the hashcode if any
                           % _array.Length; // "compression map" that changes from the possible large result of the first hash function (GetHashCode)
                                           // and maps it to a value between 0 and array.Length - 1, inclusive

            int arrayLocationCursor = hashValue;
            int readAttempts = 0;

            while (readAttempts < _array.Length)
            {
                OpenAddressingBucket<K, V>? bucket = _array[arrayLocationCursor];

                // try to find element at this location
                if (bucket == null)
                {
                    // we found an empty space, this means that this location does not contain what we are looking for!
                    // there is an empty space and therefore we do not need to keep searching..
                    throw new KeyNotFoundException();
                }

                // bucket != null
                OpenAddressingBucket<K, V> bucketValue = bucket.Value;
                if (bucketValue.IsDeleted)
                {
                    // empty bucket used as market, ignore
                }
                else // bucket found is NOT deleted and could contain the value
                {
                    if (bucketValue.Key.Equals(key))
                    {
                        // we have found what we are looking for
                        // successful return
                        return bucketValue.Value;
                    }
                }

                // we have tried to read, but failed to found what we want
                // the element could still be within the array!
                readAttempts++;
                arrayLocationCursor = (arrayLocationCursor + 1) % _array.Length; // try the next position
            }

            // readAttempts == _array.Length
            // we have tried every possible location, but did not find the key
            throw new KeyNotFoundException();
        }

        public bool Remove(K key)
        {
            if (key == null)
            {
                // there is an issue here!
                throw new ArgumentNullException(nameof(key), $"The value of {nameof(key)} is NULL. The key must not be NULL!");
            }

            int hashValue = (key.GetHashCode() & 0x7FFFFFFF) // The 0x7FFFFFFF is used to bit-mask and remove the negative sign from the hashcode if any
                           % _array.Length; // "compression map" that changes from the possible large result of the first hash function (GetHashCode)
                                            // and maps it to a value between 0 and array.Length - 1, inclusive

            int arrayLocationCursor = hashValue;
            int readAttempts = 0;

            while (readAttempts < _array.Length)
            {
                OpenAddressingBucket<K, V>? bucket = _array[arrayLocationCursor];

                // try to find element at this location
                if (bucket == null)
                {
                    // we found an empty space, this means that this location does not contain what we are looking for!
                    // there is an empty space and therefore we do not need to keep searching..
                    return false;
                }

                // bucket != null
                OpenAddressingBucket<K, V> bucketValue = bucket.Value;
                if (bucketValue.IsDeleted)
                {
                    // empty bucket used as market, ignore
                }
                else // bucket found is NOT deleted and could contain the value
                {
                    if (bucketValue.Key.Equals(key))
                    {
                        // we have found what we are looking for
                        // successful return
                        bucketValue.IsDeleted = true;
                        bucketValue.Key = default;
                        bucketValue.Value = default;
                        Size--;
                        return true;
                    }
                }

                // we have tried to read, but failed to found what we want
                // the element could still be within the array!
                readAttempts++;
                arrayLocationCursor = (arrayLocationCursor + 1) % _array.Length; // try the next position
            }

            // readAttempts == _array.Length
            // we have tried every possible location, but did not find the key
            return false;
        }

        public void Update(K key, V value)
        {
            throw new NotImplementedException();
        }
    }
}
