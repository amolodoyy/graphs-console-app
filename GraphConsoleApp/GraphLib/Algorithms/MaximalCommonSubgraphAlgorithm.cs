using GraphLib.Utils;
using QuikGraph;

namespace GraphLib.Algorithms;

public static class MaximalCommonSubgraphAlgorithm
{
  public static void Calculate(
      List<UndirectedGraph<int, UndirectedEdge<int>>> graphs)
  {
    // Creating modular graph with vertices as strings
    var modularProductString = ModularProductAlgorithm.CalculateProductString(graphs);

    Dictionary<string, int> indexedVertices = new Dictionary<string, int>();

    // Convert modular graph into an int graph with vertices mapping saved in indexedVertices
    var intGraph = ConvertStringGraphToInt(modularProductString, indexedVertices);

    var maximumCliques = MaximumCliqueAlgorithm.FindAll(intGraph);

    var subgraphs = GetOriginalSubgraphs(indexedVertices, maximumCliques, graphs.Count);

    PrintSubGraphs(subgraphs);
  }

  private static UndirectedGraph<int, UndirectedEdge<int>> ConvertStringGraphToInt(UndirectedGraph<string, UndirectedEdge<string>> stringGraph, Dictionary<string, int> indexesDict)
  {
    var intGraph = new UndirectedGraph<int, UndirectedEdge<int>>();
    int i = 0;

    foreach (var vertex in stringGraph.Vertices)
    {
      indexesDict.Add(vertex, i);
      intGraph.AddVertex(i);
      i++;
    }

    foreach (var edge in stringGraph.Edges)
    {
      var v1 = indexesDict[edge.Source];
      var v2 = indexesDict[edge.Target];

      intGraph.AddEdge(new UndirectedEdge<int>(Math.Min(v1, v2), Math.Max(v1, v2)));
    }

    return intGraph;
  }

  private static List<List<int>> GetOriginalSubgraphs(Dictionary<string, int> indexedVertices, List<UndirectedGraph<int, UndirectedEdge<int>>> maximumCliques, int numOfGraphs)
  {
    Console.WriteLine($"\nThe common subgraphs for {numOfGraphs} graphs:");

    var clique = maximumCliques.FirstOrDefault();
    var originalVertices = new List<List<int>>();
    var subgraphs = new List<List<int>>();

    foreach (var vertex in clique.Vertices) {
      var splitVertex = indexedVertices.FirstOrDefault(x => x.Value == vertex).Key.Split('_');
      var list = new List<int>();

      foreach (var stringVertex in splitVertex) {
        list.Add(int.Parse(stringVertex));
      }

      originalVertices.Add(list);
    }

    for (int i = 0; i < numOfGraphs; i++) {
      var subgraph = new List<int>();
      foreach (var entry in originalVertices) {
        subgraph.Add(entry[i]);
      }
      subgraphs.Add(subgraph);
    }

    return subgraphs;
  }

  private static void PrintSubGraphs(List<List<int>> subgraphs) {
    int i = 0;
    foreach (var subgraph in subgraphs){
      i++;
      Console.WriteLine($"Common subgraph in Graph #{i}:");
      Console.WriteLine($"[ {ConsoleHelper.ListToString(subgraph)}]\n");
    }
  }
}
