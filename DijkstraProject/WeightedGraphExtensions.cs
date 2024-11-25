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
        /// 
        /// (5) You can also pass an instance which impleemnts the IPriorityQueue as a parameter - this will allow you to use heaps with this implementation
        /// 
        /// (6) However, heaps need to either:
        ///  (i) Avoid the decrease key operation (e.g. by adding multiple times the vertex to the heap) OR
        ///  (ii) Maintain a hashtable with the location of each vertex, so that the ReduceKey can be implemented efficiently (without a linear search)
        /// </remarks>
        public static GraphSearchResult<V> Dijkstra<V>(this IWeightedGraph<V> graph, V source)
        {
            // The time complexity can change if we replace the PriorityQueue with a different type
            IPriorityQueue<V> Q = new ArrayBasedVectorPriorityQueue<V>();
            GraphSearchResult<V> result = new GraphSearchResult<V>(source);

            // There are |V| number of vertices in the graph
            foreach (V vertex in graph.GetAllVertices())
            {
                // Do not add the source immediately...
                if (vertex.Equals(source))
                {
                    continue;
                }

                //each of these lines of code is repeated x|V| times
                result.DistanceToSource[vertex] = int.MaxValue; // int.MaxValue indicates that distance is unknown // updating a value in a correctly implemented and typical hashtable is O(1) amortised/average
                // result.Previous[vertex] = null; // If the key is NOT in the Previous, then assume to unknown previous vertex
                Q.Add(vertex); // O(1) amortised since we are using ArrayBasedVectors.
            }
            // Total time for the above loop is |V|xconstant. 

            // |V|xconstant
            //Q.DecreaseKey(source, 0);

            // |V|xconstant
            Q.Add(source, 0);

            // constant time
            result.DistanceToSource[source] = 0;

            // There are initially |V| elements in Q. Each loop is going to remove exactly one element. The code inside the main loop is repeated x|V| times
            while (!Q.IsEmpty())
            {
                // |V| x constant time
                V u = Q.RemoveMin();

                // The number of iterations of the inner loop is deg(u)
                // The outer loop is repeated once for every vertex and the inner loop is repeated deg(u) times
                // The elements in the inner loop are repeated 2x|E| times
                foreach (var getAdjacencies in graph.GetAdjacencies(u))
                {
                    // primitive operation (constant time)
                    V v = getAdjacencies.Vertex;

                    // primite operation
                    // w' = dist[u] + w(u,v) - Distance represents the Weight of the edge between u and v
                    int wPrime = result.DistanceToSource[u] + getAdjacencies.Distance;

                    // primitive operation
                    if (wPrime < result.DistanceToSource[v])
                    {
                        // primitive operation
                        // We have found a new path to source from v, by going through u
                        result.DistanceToSource[v] = wPrime;

                        // |V|xconstant
                        Q.DecreaseKey(v, wPrime); // since the distance to source is updated, we need to inform the PriorityQueue
                        
                        // primitive operaion
                        result.Previous[v] = u;
                    }
                }
                // The total work for the inner loop is:
                // 2x|E|x(contant + |V|xconstant)
                // |E|x|V|xconstant +...
            }
            // Total time for these loops is:
            // |V|*|V| for the outer loop + |E|x|V| for the inner loop (times constants + smaller terms)

            return result;
        }

    }
}
