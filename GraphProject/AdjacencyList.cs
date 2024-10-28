using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphProject
{
    /// <summary>
    /// Example code describint how a adjacency list class can be used to replace the LinkedList in the Hashtable of the Graph
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AdjacencyList<T> // where T : IVertex
    {
        LinkedList<T> adjacencies = new LinkedList<T>();
    }
}
