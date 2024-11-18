using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphProject
{
    public static class GraphExtensions
    {
        public static GraphSearchResult DFS(this IGraph graph, string s)
        {

            GraphSearchResult output = new GraphSearchResult(s);
            output.VisitedVertices.Add(s);

            _DFS(graph, s, output);

            return output;
        }

        private static void _DFS(
            IGraph graph,
            string currentVertex,
            GraphSearchResult output)
        {
            /// Go through all the adjacent vertices of the <cref=currentVertex/>
            foreach (var adjacentVertex in graph.Adjacencies(currentVertex))
            {
                // the vertex adjacentVertex has already been visited
                // no need to do anything else
                if (output.VisitedVertices.Contains(adjacentVertex))
                    continue;

                // this is a new vertex, so add this to the output!
                output.VisitedVertices.Add(adjacentVertex);
                // keep track of the next edge to the source vertex
                output.Previous.Add(adjacentVertex, currentVertex);

                _DFS(graph, adjacentVertex, output);
            }
        }

        /// <summary>
        /// Extension method that adds a new method to instances that implement IGraph
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static GraphSearchResult BFS(this IGraph graph, string s)
        {
            GraphSearchResult result = new GraphSearchResult(s);
            Queue<string> greyVertices = new Queue<string>();

            greyVertices.Enqueue(s);
            result.VisitedVertices.Add(s);

            while (greyVertices.Count > 0)
            {
                string v = greyVertices.Dequeue();

                foreach (string u in graph.Adjacencies(v))
                {
                    if (!result.VisitedVertices.Contains(u))
                    {
                        result.VisitedVertices.Add(u);
                        result.Previous[u] = v;
                        greyVertices.Enqueue(u);
                    }
                }
            }

            return result;
        }
    }
}
