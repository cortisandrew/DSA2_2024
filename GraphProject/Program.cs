using GraphProject;

GraphUsingAdjacencyLists graph = new GraphUsingAdjacencyLists();

graph.AddVertex("A");
graph.AddVertex("B");
graph.AddVertex("C");
graph.AddVertex("D");
graph.AddVertex("E");
graph.AddVertex("F");
graph.AddVertex("G");
graph.AddVertex("H");

graph.AddEdge("A", "B");
graph.AddEdge("A", "C");
// graph.AddEdge("B", "A"); // - not needed, we already have this Edge!
graph.AddEdge("B", "D");
graph.AddEdge("B", "F");
graph.AddEdge("C", "D");
graph.AddEdge("D", "E");
graph.AddEdge("F", "G");
graph.AddEdge("F", "H");
graph.AddEdge("G", "H");

//Console.WriteLine(graph);

// Next lesson to output the graph search result as a directed graph
GraphSearchResult searchResult = graph.DFS("A");

Console.WriteLine(
    String.Join(", ",
                searchResult.FindPathToSourceFromVertex("H")));

Console.WriteLine(
    String.Join(", ",
                searchResult.FindPathToVertexFromSource("H")));


searchResult = graph.BFS("A");

Console.WriteLine(
    String.Join(", ",
                searchResult.FindPathToVertexFromSource("H")));


GraphUsingAdjacencyMatrix graphTwo = new GraphUsingAdjacencyMatrix(new List<string> { "A", "B", "C", "D", "E", "F", "G", "H" });


graphTwo.AddEdge("A", "B");
graphTwo.AddEdge("A", "C");
graphTwo.AddEdge("B", "D");
graphTwo.AddEdge("B", "F");
graphTwo.AddEdge("C", "D");
graphTwo.AddEdge("D", "E");
graphTwo.AddEdge("F", "G");
graphTwo.AddEdge("F", "H");
graphTwo.AddEdge("G", "H");

DisplayMatrix(graphTwo.GetMatrix());

static void DisplayMatrix(int[,] mat)
{
    int V = mat.GetLength(0);
    for (int i = 0; i < V; i++)
    {
        for (int j = 0; j < V; j++)
        {
            Console.Write(mat[i, j] + " ");
        }
        Console.WriteLine();
    }
}

Console.WriteLine();