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
    /// <typeparam name="V">The Vertex</typeparam>
    public interface IWeightedGraph<V>
    {
        IEnumerable<V> GetAllVertices();

        IEnumerable<VertexDistance<V>> GetAdjacencies(V vertex);

        /*
        IEnumerable<V> GetAdjacencies(V vertex);

        int GetEdgeWeight(V vertexOne, V vertexTwo);
        */
    }
}
