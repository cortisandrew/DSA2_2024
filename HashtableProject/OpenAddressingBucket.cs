using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashtableProject
{
    internal struct OpenAddressingBucket<K, V>
        where K : IEquatable<K>
    {
        internal OpenAddressingBucket(K key, V value)
        {
            Key = key;
            Value = value;
        }

        internal K Key { get; set; }

        internal V Value { get; set; }

        internal bool IsDeleted { get; set; } = false;

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
