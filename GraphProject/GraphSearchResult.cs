using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphProject
{
    public class GraphSearchResult
    {
        public string SourceVertex { get; private set; }

        public HashSet<string> VisitedVertices { get; } = new HashSet<string>();

        public Dictionary<string, string> Previous = new Dictionary<string, string>(); // shows the next vertex to travel back to the SourceVertex

        public GraphSearchResult(string sourceVertex) {
            SourceVertex = sourceVertex;
        }

        public List<string> FindPathToSourceFromVertex(string vertex)
        {
            List<string> path = new List<string>();
            string currentVertex = vertex;

            while (currentVertex != SourceVertex) { 
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

        public List<string> FindPathToVertexFromSource(string vertex)
        {
            List<string> path = FindPathToSourceFromVertex(vertex);
            path.Reverse();

            return path;
        }
    }
}
