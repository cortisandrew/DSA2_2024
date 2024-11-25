using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraProject
{
    /// <summary>
    /// Represents a PriorityQueue that uses the List (which is an ArrayBasedVector) to store all items
    /// </summary>
    /// <typeparam name="V"></typeparam>
    public class ArrayBasedVectorPriorityQueue<V> : IPriorityQueue<V>
    {
        private List<VertexDistance<V>> _priorityQueue = new List<VertexDistance<V>>();
        // private List<(V, int)> _priorityQueue = new List<(V, int)>();
        
        public ArrayBasedVectorPriorityQueue() {
        }

        /// <summary>
        /// Add a vertex to the List, with distance to source
        /// Add is amortised O(1) time.
        /// </summary>
        /// <param name="vertexIdentifier"></param>
        /// <param name="distanceToSource"></param>
        /// <exception cref="ArgumentNullException">If the VertexIdentifier is null, you get an ArgumentNullException</exception>
        public void Add(V vertexIdentifier, int distanceToSource = int.MaxValue)
        {
            // disallow nulls as vertexIdentifier
            if (vertexIdentifier == null)
            {
                throw new ArgumentNullException(nameof(vertexIdentifier));
            }

            /* You could add this - but in some cases, I will allow multiple of the same identifier to be added...
                // Takes O(n) time if you include it
                _priorityQueue.Any(x=>x.Vertex!.Equals(vertexIdentifier)) {
                    throw new ArgumentException("The vertexIdentifier already exists in the PriorityQueue", nameof(vertexIdentifier));
                }
            */

            _priorityQueue.Add(
                new VertexDistance<V>(vertexIdentifier, distanceToSource));
        }

        /// <summary>
        /// Find the vertex with the VertexIdentifier
        /// There should be exactly one vertex with this identifier!
        /// Reduce the distance to source
        /// O(n) time to execute (where n is the number of elements on the Priority Queue)
        /// </summary>
        /// <param name="vertexIdentifier"></param>
        /// <param name="newDistanceToSource"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public void DecreaseKey(V vertexIdentifier, int newDistanceToSource)
        {
            // O(n) time
            VertexDistance<V>? getItem = _priorityQueue.SingleOrDefault(x => x.Vertex!.Equals(vertexIdentifier));

            if (getItem == null)
            {
                throw new ArgumentException("The vertexIdentifier in the parameter does not exist in the priority queue", nameof(getItem));
            }

            if (newDistanceToSource <= getItem.Distance)
            {
                getItem.Distance = newDistanceToSource;
            }
            else
            {
                throw new InvalidOperationException("You cannot increase the distance to source!");
            }
        }

        /// <summary>
        /// O(1) time
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return _priorityQueue.Count == 0;
        }

        /// <summary>
        /// O(n) time
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public V RemoveMin()
        {
            // O(n)
            VertexDistance<V>? getItem = _priorityQueue.MinBy(x => x.Distance);

            if (getItem == null)
            {
                throw new InvalidOperationException("The PriorityQueue is empty!");
            }

            // O(n)
            _priorityQueue.Remove(getItem);
            return getItem.Vertex;
        }
    }
}
