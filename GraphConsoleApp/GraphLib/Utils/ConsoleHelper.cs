using GraphLib.Enums;
using QuikGraph;
using System.Text;

namespace GraphLib.Utils
{
    public class ConsoleHelper
    {
        public static void WriteTaskMessage(string message, TaskEnum task)
        {
            var color = Console.ForegroundColor;
            var originalColor = Console.ForegroundColor;

            switch (task)
            {
                case TaskEnum.Task1: color = ConsoleColor.DarkCyan; break;
                case TaskEnum.Task2: color = ConsoleColor.DarkBlue; break;
                case TaskEnum.Task3: color = ConsoleColor.DarkMagenta; break;
                case TaskEnum.Task4: color = ConsoleColor.DarkYellow; break;
            }

            Console.Write(DateTime.Now.ToString("HH:mm:ss") + "\n");
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ForegroundColor = originalColor;
            Console.WriteLine("\n");
        }
        public static void WriteInfo(string message)
        {
            var originalColor = Console.ForegroundColor;
            Console.Write(DateTime.Now.ToString("HH:mm:ss") + "\n");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("INFO: " + message + "\n");
            Console.ForegroundColor = originalColor;
        }
        public static void WriteSeparator()
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("==================================================" + "\n");
            Console.ForegroundColor = originalColor;
        }
        public static void WriteError(string message)
        {
            var originalColor = Console.ForegroundColor;
            Console.Write(DateTime.Now.ToString("HH:mm:ss") + "\n");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("ERROR: " + message + "\n");
            Console.ForegroundColor = originalColor;
        }
        public static string ListToString<T>(List<T> values)
        {
            string resString = string.Empty;
            foreach (var value in values)
            {
                resString += $"{value} ";
            }
            return resString;
        }
        public static string FormatGraph(UndirectedGraph<int, UndirectedEdge<int>> graph)
        {
            // format is like this: [v0, v1, numberOfUndirectedEdgesBetween]
            var stringBuilder = new StringBuilder();
            var vertices = graph.Vertices.ToArray();
            var stringRows = new List<string>();

            for (int i = 0; i < vertices.Length; i++)
            {
                // assuming that vertices always start from 0, index of vertex is the vertex itself
                var rows = graph.AdjacentVertices(vertices[i]).Where(v => v >= i);
                foreach(var row in rows)
                {
                    var numOfUndirectedEdgesBetween = graph.AdjacentEdges(vertices[i]).Intersect(graph.AdjacentEdges(row)).Count();
                    stringBuilder.Append($"[v_{i}, v_{row}, {numOfUndirectedEdgesBetween}]\n");
                }
                stringBuilder.Append('\n');
            }

            return stringBuilder.ToString();
        }
    }
}
