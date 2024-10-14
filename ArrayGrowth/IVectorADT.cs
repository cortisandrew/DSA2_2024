namespace ArrayGrowth
{
    /// <summary>
    /// Abstract data type that defines operations, behaviour and what data is stored
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IVectorADT<T>
    {
        int Count { get; }

        void Append(T item);
        void InsertAtRank(T item, int rank);
    }
}