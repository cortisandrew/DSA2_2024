using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraProject
{
    public static class WeightedGraphExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <param name="graph"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        /// <remarks>
        /// Issues to know about:
        /// (1)
        /// V is representing BOTH the vertex AND its identifier. Consider adding where V : IVertex
        /// and Creating Q to be IPriorityQueue with VIdentifier as the Type Parameter
        /// ALTERNATIVE
        /// Override the Vertex.Equals operation and Vertex.HashCode
        /// 
        /// (2)
        /// Replace the ArrayBasedPriorityQueue with a more efficient PriorityQueue, e.g. Heap
        /// 
        /// (3)
        /// You will also require to implement the ReduceKey EFFICIENTLY for the Heap of find a workaround
        /// 
        /// (4)
        /// You still need to implement a WeightedGraph!
        /// </remarks>
        public static GraphSearchResult<V> Dijkstra<V>(this IWeightedGraph<V> graph, V source)
        {
            IPriorityQueue<V> Q = new ArrayBasedVectorPriorityQueue<V>();
            GraphSearchResult<V> result = new GraphSearchResult<V>(source);

            foreach (V vertex in graph.GetAllVertices())
            {
                result.DistanceToSource[vertex] = int.MaxValue; // int.MaxValue indicates that distance is unknown
                // result.Previous[vertex] = null; // If the key is NOT in the Previous, then assume to unknown previous vertex
                Q.Add(vertex);
            }

            Q.DecreaseKey(source, 0);
            result.DistanceToSource[source] = 0;

            while (!Q.IsEmpty())
            {
                V u = Q.RemoveMin();

                foreach (var getAdjacencies in graph.GetAdjacencies(u))
                {
                    V v = getAdjacencies.Vertex;

                    // w' = dist[u] + w(u,v) - Distance represents the Weight of the edge between u and v
                    int wPrime = result.DistanceToSource[u] + getAdjacencies.Distance;

                    if (wPrime < result.DistanceToSource[v])
                    {
                        // We have found a new path to source from v, by going through u
                        result.DistanceToSource[v] = wPrime;
                        Q.DecreaseKey(v, wPrime); // since the distance to source is updated, we need to inform the PriorityQueue
                        result.Previous[v] = u;
                    }
                }
            }

            return result;
        }

    }
}
