using QuikGraph;

namespace GraphConsoleApp
{
    public static class GraphTxtReader
    {
        public static AdjacencyGraph<int, Edge<int>> FromTxt(string filepath)
        {
            var graph = new AdjacencyGraph<int, Edge<int>>();
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
            catch (Exception ex)
            {
                Console.WriteLine("Error while reading file: " + ex.Message);
                Console.WriteLine("Please try again");
                return graph;
            }
            return graph;
        }
        private static void ProcessDirectedRow((int, string) row, AdjacencyGraph<int, Edge<int>> graph)
        {
            var values = row.Item2.Split(' ').Select(int.Parse).ToArray();

            for (int i = 0; i < values.Length; i++)
            {
                var edge = new Edge<int>(row.Item1-1, i);
                graph.AddEdgeRange(Enumerable.Repeat(edge, values[i]));
            }
        }
        private static void ProcessUndirectedRow((int, string) row, AdjacencyGraph<int, Edge<int>> graph)
        {
            var values = row.Item2.Split(' ').Select(int.Parse).ToArray();

            for(int i = row.Item1 - 1; i < values.Length; i++)
            {
                var edge = new Edge<int>(row.Item1 - 1, i);
                graph.AddEdgeRange(Enumerable.Repeat(edge, values[i]));
            }
        }
    }
}
/*
 
    v0 v1 v2 v3
 v0 0  1   0  1
 v1 1  0   
 v2
 */
