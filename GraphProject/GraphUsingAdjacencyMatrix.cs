using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphProject
{
    public class GraphUsingAdjacencyMatrix
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
    }
}
