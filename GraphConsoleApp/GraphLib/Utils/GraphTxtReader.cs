using GraphLib.Utils;
using QuikGraph;

namespace GraphLib
{
  public static class GraphTxtReader
  {
    public static (List<UndirectedGraph<int, UndirectedEdge<int>>>, List<string>)? FromTxt(string filepath)
    {
      var fileName = Path.GetFileNameWithoutExtension(filepath);
      List<UndirectedGraph<int, UndirectedEdge<int>>> graphs = new List<UndirectedGraph<int, UndirectedEdge<int>>>();
      int graphsCount = 0;
      int currentGraph = -1;
      try
      {
        using var reader = new StreamReader(filepath);
        string? line;
        int lineIndex = 0;
        while ((line = reader.ReadLine()) != null) {
          if (lineIndex == 0)
          {
            graphsCount = int.Parse(line);
          }
          else if (lineIndex == 1)
          {
            var graph = new UndirectedGraph<int, UndirectedEdge<int>>();
            graph.AddVertexRange(Enumerable.Range(0, int.Parse(line)));
            graphs.Add(graph);
            currentGraph++;
          }
          else if (line == null || line.Length == 0)
          {
            lineIndex = 0;
          }
          else
          {
            ProcessUndirectedRow((lineIndex, line), graphs[currentGraph]);
          }

          lineIndex++;
        }
      }
      catch
      {
        ConsoleHelper.WriteError($"Format of graph file {filepath} is not valid. " +
            $"Omitting this file.");
        return null;
      }

      List<string> graphNames = new List<string>();
      for (int i = 0; i < graphsCount; i++) {
        graphNames.Add(fileName + $"({i + 1})");
      }
      return (graphs, graphNames);
    }
    // use for undirected graphs
    private static void ProcessUndirectedRow((int, string) row, UndirectedGraph<int, UndirectedEdge<int>> graph)
    {
      var values = row.Item2.Split(' ').Select(int.Parse).ToArray();

      for (int i = row.Item1 - 1; i < values.Length; i++)
      {
        var UndirectedEdge = new UndirectedEdge<int>(row.Item1 - 1, i);
        graph.AddEdgeRange(Enumerable.Repeat(UndirectedEdge, values[i]));
      }
    }
  }
}
