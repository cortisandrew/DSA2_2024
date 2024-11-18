using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraProject
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="V">The unique identifier for a vertex</typeparam>
    public interface IPriorityQueue<V>
    {
        /// <summary>
        /// Add the vertex with vertexIdentifier to the Queue
        /// </summary>
        /// <param name="vertexIdentifier"></param>
        /// <param name="distanceToSource">The distanceToSource of the vertex from the source. Default to int.MaxValue</param>
        void Add(V vertexIdentifier, int distanceToSource = int.MaxValue);  // double distanceToSource = double.PositiveInfinity // might make your implementation tricker

        /// <summary>
        /// Remove the vertex with the minimum distanceToSource
        /// </summary>
        /// <returns>Return the vertexIdentifier of the vertex removed</returns>
        V RemoveMin();

        /// <summary>
        /// Can be used when a NEW SHORTER path to the source is found.
        /// Find the vertexIdentifier in the PriorityQueue and REDUCE it's key
        /// </summary>
        /// <param name="vertexIdentifier"></param>
        /// <param name="newDistanceToSource"></param>
        /// <remarks>If the newDistanceToSource is LARGER than before, this method should throw an InvalidOperationException</remarks>
        void DecreaseKey(V vertexIdentifier, int newDistanceToSource);

        /// <summary>
        /// Returns true when the PriorityQueue is empty
        /// </summary>
        /// <returns></returns>
        bool IsEmpty();
    }
}
