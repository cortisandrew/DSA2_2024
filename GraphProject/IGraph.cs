namespace GraphProject
{
    public interface IGraph
    {
        IEnumerable<string> Adjacencies(string vertex);
        int Degree(string vertex);
        bool IsAdjacent(string vertexOne, string vertexTwo);
    }
}