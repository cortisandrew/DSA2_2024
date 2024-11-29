namespace HashtableProject
{
    public interface IHashtable<K, V> where K : IEquatable<K>
    {
        int Size { get; }

        void Add(K key, V value);
        bool ContainsKey(K key);
        V Get(K key);
        bool Remove(K key);
        void Update(K key, V value);
    }
}