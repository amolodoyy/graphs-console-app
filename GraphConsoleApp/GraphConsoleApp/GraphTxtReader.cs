using QuikGraph;

namespace GraphConsoleApp
{
    public static class GraphTxtReader
    {
        public static AdjacencyGraph<Vertex, Edge<Vertex>> FromTxt(string filepath)
        {
            var graph = new AdjacencyGraph<Vertex, Edge<Vertex>>();
            try
            {
                using (var reader = new StreamReader(filepath))
                {
                    string? line;
                    int lineIndex = 0;
                    while((line = reader.ReadLine()) != null)
                    {
                        if(lineIndex == 0)
                            graph.AddVertexRange(new List<Vertex>(int.Parse(line)));
                        else
                            ProcessRow((lineIndex, line), graph);

                        lineIndex++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while reading file: " + ex.Message);
                Console.WriteLine("Please try again");
            }
            return graph;
        }
        private static void ProcessRow((int, string) row, AdjacencyGraph<Vertex, Edge<Vertex>> graph)
        {
            Console.WriteLine(row.Item1);
            var values = row.Item2.Split(' ').Select(int.Parse).ToList();
            var edges = values
                .Select(v => new Edge<Vertex>(graph.Vertices.ElementAt(row.Item1 - 1), new Vertex(v)))
                .ToList();
            graph.AddEdgeRange(edges);
        }
    }
}
