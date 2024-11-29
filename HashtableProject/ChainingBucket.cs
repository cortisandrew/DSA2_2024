using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashtableProject
{
    /// <summary>
    /// The chaining bucket is a bucket that will be used as the storage in the array of the hashtable
    /// It stores the basic information: Key and the Value
    /// It also has a nullable Next - if there is a collision, the Next is used to resolve the collision
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    internal class ChainingBucket<K, V>
        where K : IEquatable<K>
    {
        internal ChainingBucket(K key, V value)
        {
            Key = key;
            Value = value;
            Next = null;
        }

        internal K Key { get; set; }

        internal V Value { get; set; }

        internal ChainingBucket<K, V>? Next { get; set; }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(Key.ToString());
            stringBuilder.Append(":");

            // Value?.ToString() will return the ToString if the Value is NOT null
            // Value?.ToString() will return NULL if the Value is NULL
            // X ?? newValue will return X is X is NOT NULL and newValue otherwise
            stringBuilder.Append(Value?.ToString() ?? "NULL");

            return stringBuilder.ToString();
        }
    }
}
