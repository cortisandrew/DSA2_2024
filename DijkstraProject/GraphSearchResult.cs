using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraProject
{
    public class GraphSearchResult<V>
    {
        public V SourceVertex { get; private set; }

        public Dictionary<V, V> Previous = new Dictionary<V, V>(); // shows the next vertex to travel back to the SourceVertex

        public Dictionary<V, int> DistanceToSource = new Dictionary<V, int>(); // The distance to source for each vertex

        public GraphSearchResult(V sourceVertex)
        {
            SourceVertex = sourceVertex;
        }

        public List<V> FindPathToSourceFromVertex(V vertex)
        {
            List<V> path = new List<V>();
            V currentVertex = vertex;

            while (!currentVertex.Equals(SourceVertex))
            {
                path.Add(currentVertex);

                if (Previous.ContainsKey(currentVertex))
                {
                    currentVertex = Previous[currentVertex];
                }
                else
                {
                    throw new InvalidOperationException($"There is no path between {vertex} and {SourceVertex}");
                }
            }

            path.Add(SourceVertex);
            return path;
        }

        public List<V> FindPathToVertexFromSource(V vertex)
        {
            List<V> path = FindPathToSourceFromVertex(vertex);
            path.Reverse();

            return path;
        }
    }
}
