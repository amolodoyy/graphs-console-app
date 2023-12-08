using GraphLib.Utils;
using QuikGraph;

namespace GraphLib
{
    public static class GraphTxtReader
    {
        public static UndirectedGraph<int, UndirectedEdge<int>>? FromTxt(string filepath)
        {
            var graph = new UndirectedGraph<int, UndirectedEdge<int>>();
            try
            {
                using var reader = new StreamReader(filepath);
                string? line;
                int lineIndex = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    if (lineIndex == 0)
                    {
                        graph.AddVertexRange(Enumerable.Range(0, int.Parse(line)));
                    }
                    else
                    {
                        ProcessUndirectedRow((lineIndex, line), graph);
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
            return graph;
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
