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
    }
}
