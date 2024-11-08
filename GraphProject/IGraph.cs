namespace GraphProject
{
 
    /// <summary>
    /// Represents what operations a graph ADT should provide
    /// </summary>
    public interface IGraph
    {
        IEnumerable<string> Adjacencies(string vertex);
        int Degree(string vertex);
        bool IsAdjacent(string vertexOne, string vertexTwo);
    }
}