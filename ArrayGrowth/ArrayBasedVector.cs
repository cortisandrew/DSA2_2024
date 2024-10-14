using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayGrowth
{
    /// <summary>
    /// A vector is an ordered (not sorted!) group of items
    /// The items can be accessed by the "Rank": The Rank is the number of items before the current item.
    /// In the case of the ArrayBasedVector, the items are stored in an array
    /// The "Rank" is equal to the position in the array
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ArrayBasedVector<T> : IVectorADT<T>
    {
        // Start off with an array of length 1 (mostly to be the same as the analysis on the slides)
        // Ideally, you start off with a larger array
        private const int DEFAULT_LENGTH = 1;

        // The array of items that are stored with the array based vector
        T[] array = new T[DEFAULT_LENGTH];

        private volatile int workCarriedOut = 0;

        public int WorkCarriedOut
        {
            get { return workCarriedOut; }
        }

        readonly IGrowthStrategy _growthStrategy;

        public ArrayBasedVector() : this(new DoublingGrowthStrategy())
        {
        }
        public ArrayBasedVector(IGrowthStrategy growthStrategy) { 
            _growthStrategy = growthStrategy;
        }

        /// <summary>
        /// The number of elements currently stored in the ArrayBasedVector
        /// </summary>
        // private int count = 0;

        /// <summary>
        /// The number of elements currently stored in the ArrayBasedVector
        /// </summary>
        /// <remarks>
        /// Notice: This is NOT equal to the length of the array! We can have an array with empty spaces
        /// that can be filled later
        /// </remarks>
        public int Count
        {
            get;
            private set;
        } = 0;

        /// <summary>
        /// The get allows you to read at rank (GetElementAtRank)
        /// The set is a ReplaceAtRank
        /// </summary>
        /// <param name="rank"></param>
        /// <returns></returns>
        public T this[int rank]
        {
            get
            {
                if (rank < 0 || rank >= Count)
                {
                    // rank cannot be < 0
                    // rank < count, because we want to refer to an existing element!
                    throw new ArgumentOutOfRangeException(nameof(rank), $"Rank greater than or equal to 0 and smaller than or equal to {Count - 1}, inclusive!");
                }

                return array[rank];
            }
            set
            {
                if (rank < 0 || rank >= Count)
                {
                    // rank cannot be < 0
                    // rank < count, because we want to refer to an existing element!
                    throw new ArgumentOutOfRangeException(nameof(rank), $"Rank greater than or equal to 0 and smaller than or equal to {Count - 1}, inclusive!");
                }

                array[rank] = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <remarks>
        /// Equivalent to InsertAtRank(item, count)
        /// </remarks>
        public void Append(T item)
        {
            EnsureCapacity();

            // assuming that there is enough space
            // place the item at the end
            array[Count] = item;
            workCarriedOut++;
            // increment the counter
            Count++;
        }

        public void InsertAtRank(T item, int rank)
        {
            EnsureCapacity();

            if (rank < 0 || rank > Count)
            {
                // rank cannot be < 0
                // rank <= count, because we do not want any gaps
                throw new ArgumentOutOfRangeException(nameof(rank), $"Rank must be between 0 and {Count}, inclusive!");

                // throw new ArgumentOutOfRangeException("rank", );
            }

            // copy all elements, up to rank, one step forward
            // starting from the last element, down to the rank parameter
            // the last element is at index count - 1.
            for (int i = Count - 1; i >= rank; i--)
            {
                // copy the element one step forward
                array[i + 1] = array[i];
                workCarriedOut++;
            }
            // You can also use the array COPY method for the above, however, it still takes a lot of time
            // Here we are making the work requirement for the copy explicit

            // array at rank is now free
            array[rank] = item;
            workCarriedOut++;
            Count++;
        }

        private void EnsureCapacity()
        {
            if (Count == array.Length)
            {
                // The number of elements stored is equal to the number of available spaces (length)
                // Your array is full!
                // You need to do something about this!
                int newArrayLength = _growthStrategy.GetNewLength(array.Length);

                T[] newArray = new T[newArrayLength];

                // equivalent to the loop below...
                // array.CopyTo( newArray, 0 );

                for (int i = 0; i < array.Length; i++)
                {
                    newArray[i] = array[i];
                    workCarriedOut++;
                }

                // replace the old array with the new Array
                array = newArray;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");

            for (int i = 0; i < Count; i++)
            {
                // Append the value found or NULL
                if (array[i] == null)
                {
                    sb.Append("NULL");
                }
                else
                {
                    sb.Append(array[i]!.ToString());
                }

                sb.Append(", "); // you might want to avoid adding a comma at the end of the list, i.e. when i == count - 1
            }

            sb.Append("]");

            return sb.ToString();
        }
    }
}
