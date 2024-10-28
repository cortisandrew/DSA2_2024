using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphProject
{
    /// <summary>
    /// A Graph where each Vertex is uniquely identified using a string
    /// (The name of the Vertex is a string, and it is a "PrimaryKey")
    /// </summary>
    public class GraphUsingAdjacencyLists : IGraph // <T> where T:IVertex (possible changes that you may consider)
    {
        /// <summary>
        /// Hashtable that maps the vector (represented as a string) to its adjacency list
        /// </summary>
        /// <example>
        /// adjacencyLists["A"] returns the vertices that are adjacent to A
        /// An example of the return value is { "B", "C" }
        /// </example>
        /// <remarks>
        /// If you feel more comfortable, consider replacing the LinkedList with an AdjacenyList class
        /// </remarks>
        Dictionary<string, LinkedList<string>> adjacencyLists = new Dictionary<string, LinkedList<string>>();

        public void AddVertex(string vertex)
        {
            if (adjacencyLists.ContainsKey(vertex))
            {
                // TODO
                // create a custom KeyExistsException here!
                throw new Exception($"A vertex with name {vertex} already exists");
            }

            // add a new vertex with an empty linkedlist
            adjacencyLists.Add(vertex, new LinkedList<string>());
        }

        public void AddEdge(string vertexOne, string vertexTwo)
        {
            // check that both vertices exist
            // throw an exception if they do not exist

            if (!adjacencyLists.ContainsKey(vertexOne) || !adjacencyLists.ContainsKey(vertexTwo))
            {
                throw new Exception("One of the vertices of the edge does not exist");
            }
            // an alternative would be to add the vertices here instead of throwing an exception

            // now we know that both vertices exist and each vertex has its own adjacency list

            if (adjacencyLists[vertexOne].Contains(vertexTwo) || adjacencyLists[vertexTwo].Contains(vertexOne))
            {
                // the edge already exists
                // because (usually both) the adjacencyList of vertexOne AND the adjacencyList of vertexTwo contain the other vertex
                // therefore the vertices are already adjacent and we are not adding anything new!
                throw new Exception("Edge already exists");
            }

            // now we know that both vertices exist and each vertex has its own adjacency list
            // we also know that the edge does NOT exist
            adjacencyLists[vertexOne].AddLast(vertexTwo);
            adjacencyLists[vertexTwo].AddLast(vertexOne);
        }

        public int Degree(string vertex)
        {
            if (!adjacencyLists.ContainsKey(vertex))
            {
                throw new ArgumentException($"The vertex name {vertex} does not exist in this graph instance!", nameof(vertex));
            }

            // Average O(1) time, therefore for this analysis, we are going to assume that this lookup is constant time
            var adjacentyList = adjacencyLists[vertex];

            return adjacentyList.Count;

            // return adjacentyList?.Count ?? 0;
            // return adjacentyList == null ? 0 : adjacentyList.Count;
        }

        public bool IsAdjacent(string vertexOne, string vertexTwo)
        {
            if (!adjacencyLists.ContainsKey(vertexOne))
            {
                throw new ArgumentException($"The vertex name {vertexOne} does not exist in this graph instance!", nameof(vertexOne));
            }

            if (!adjacencyLists.ContainsKey(vertexTwo))
            {
                throw new ArgumentException($"The vertex name {vertexTwo} does not exist in this graph instance!", nameof(vertexTwo));
            }

            // Average O(1) time, therefore for this analysis, we are going to assume that this lookup is constant time
            var adjacencyList = adjacencyLists[vertexOne];

            foreach (var adjacentVertex in adjacencyList)
            {
                // vertexTwo is found in the adjacency list
                if (adjacentVertex == vertexTwo)
                {
                    return true;
                }
            }

            // vertexTwo is NOT in the adjacency list
            return false;
        }


        public IEnumerable<string> Adjacencies(string vertex)
        {
            if (!adjacencyLists.ContainsKey(vertex))
            {
                throw new ArgumentException($"The vertex name {vertex} does not exist in this graph instance!", nameof(vertex));
            }

            return adjacencyLists[vertex];

            // Alternative, make a copy of the linked list and return the copy

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("graph G {");

            // Step 1, add all the vertices (to avoid having vertices that are not connected to anything else and being missed)
            foreach (string vertex in adjacencyLists.Keys)
            {
                sb.AppendLine($"{vertex};");
            }

            HashSet<string> addedEdges = new HashSet<string>();

            // Step 2, add all the edges (make sure to add each edge EXACTLY once)
            foreach (string vertex in adjacencyLists.Keys)
            {
                LinkedList<string> adjacencyListForVertex = adjacencyLists[vertex];

                foreach (string adjacentVertex in adjacencyListForVertex)
                {
                    string newEdge = $"{vertex}--{adjacentVertex};";
                    string alternateEdge = $"{adjacentVertex}--{vertex};";
                    
                    if (addedEdges.Contains(newEdge)) //  || addedEdges.Contains(alternateEdge))
                    {
                        // this edge was already added
                        // do nothing
                        continue;
                    }
                    
                    // the edge is new, we can draw it:
                    sb.AppendLine(newEdge);

                    // we also want to remember that we have added the edge!
                    addedEdges.Add(newEdge);
                    addedEdges.Add(alternateEdge);

                }
            }

            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
