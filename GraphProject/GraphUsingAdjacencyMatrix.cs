using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GraphProject
{
    public class GraphUsingAdjacencyMatrix : IGraph
    {
        private Dictionary<string, int> mappingFromVertexToIndex = new Dictionary<string, int>();
        private Dictionary<int, string> reverseMappingFromIndex = new Dictionary<int, string>();

        private int[,] adjacencyMatrix;

        public int[,] GetMatrix()
        {
            return adjacencyMatrix;
        }

        public GraphUsingAdjacencyMatrix(List<string> vertices)
        {
            int currentIndex = 0;
            foreach (var vertex in vertices)
            {

                if (mappingFromVertexToIndex.ContainsKey(vertex))
                {
                    // you are trying to add the same vertex multiple times!
                    throw new InvalidDataException("Your vertices contain duplicates, which are disallowed!");
                }

                mappingFromVertexToIndex.Add(vertex, currentIndex);
                reverseMappingFromIndex.Add(currentIndex, vertex);
                currentIndex++;
            }

            adjacencyMatrix = new int[vertices.Count, vertices.Count]; // initially add locations are 0s!
        }

        public void AddEdge(string vertexOne, string vertexTwo)
        {
            int indexOne = mappingFromVertexToIndex[vertexOne];
            int indexTwo = mappingFromVertexToIndex[vertexTwo];

            adjacencyMatrix[indexOne, indexTwo] = 1;
            adjacencyMatrix[indexTwo, indexOne] = 1;
        }

        public int Degree(string vertex)
        {
            if (!mappingFromVertexToIndex.ContainsKey(vertex))
            {
                throw new ArgumentException($"The vertex name {vertex} does not exist in this graph instance!", nameof(vertex));
            }

            int index = mappingFromVertexToIndex[vertex];

            int degree = 0;
            for (int i = 0; i < adjacencyMatrix.Length; i++)
            {
                if (adjacencyMatrix[i, index] != 0)
                {
                    // we have found an edge!
                    degree++;
                }
            }

            return degree;
        }

        public bool IsAdjacent(string vertexOne, string vertexTwo)
        {
            if (!mappingFromVertexToIndex.ContainsKey(vertexOne))
            {
                throw new ArgumentException($"The vertex name {vertexOne} does not exist in this graph instance!", nameof(vertexOne));
            }

            if (!mappingFromVertexToIndex.ContainsKey(vertexTwo))
            {
                throw new ArgumentException($"The vertex name {vertexTwo} does not exist in this graph instance!", nameof(vertexTwo));
            }

            int i = mappingFromVertexToIndex[vertexOne];
            int j = mappingFromVertexToIndex[vertexTwo];

            return adjacencyMatrix[i, j] != 0;
        }


        public IEnumerable<string> Adjacencies(string vertex)
        {
            if (!mappingFromVertexToIndex.ContainsKey(vertex))
            {
                throw new ArgumentException($"The vertex name {vertex} does not exist in this graph instance!", nameof(vertex));
            }

            int index = mappingFromVertexToIndex[vertex];

            LinkedList<string> adjacencyList = new LinkedList<string>();
            for (int i = 0; i < adjacencyMatrix.Length; i++)
            {
                if (adjacencyMatrix[i, index] != 0)
                {
                    // we have found an adjacent vertices!
                    adjacencyList.AddLast(
                        reverseMappingFromIndex[i]  // get the vertex for index i
                        );
                }
            }

            return adjacencyList;
        }
    }
}
