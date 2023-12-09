using GraphConsoleApp;
using GraphLib;
using GraphLib.Algorithms;
using GraphLib.Enums;
using GraphLib.Utils;
using QuikGraph;
using System.Collections.Generic;

var currentTask = TaskEnum.Task1;

if (!Directory.Exists(AppSettings.SourceDirectoryPath))
{
    ConsoleHelper.WriteError("Cannot find `sources` folder, please check that you've placed your folders correctly.\n");
    ConsoleHelper.WriteInfo("Exiting application...");
    return;
}

var filePaths = Directory.GetFiles(AppSettings.SourceDirectoryPath);

var graphs = filePaths.Select(f => GraphTxtReader.FromTxt(f)).Where(g => g is not null).ToList();
ConsoleHelper.WriteSeparator();
ConsoleHelper.WriteTaskMessage("Computing the size of the graphs (sum of vertices and UndirectedEdges).", currentTask);

for (int i = 0; i < graphs.Count; i++)
{
    Console.WriteLine(
        $"Size of the Graph #{i}: " +
        GraphSizeAlgorithm<int, UndirectedEdge<int>>.GetSize(graphs[i]!),
        currentTask);
}

currentTask = TaskEnum.Task2;
ConsoleHelper.WriteSeparator();
ConsoleHelper.WriteTaskMessage("Calculating the maximum cliques of the graphs", currentTask);

for (int i = 0; i < graphs.Count; i++) {
  var cliquesList = MaximumCliqueAlgorithm.FindAll(graphs[i]!);
  Console.WriteLine("Maximum clique" + (cliquesList.Count > 1 ? "s " : " ") + $"of the Graph #{i}:");
  foreach (var clique in cliquesList){
    Console.WriteLine($"[ {ConsoleHelper.ListToString(clique.Vertices.ToList())}]");
  }
  Console.WriteLine();
}

currentTask = TaskEnum.Task3;
ConsoleHelper.WriteSeparator();
ConsoleHelper.WriteTaskMessage("Calculating the maximum common subgraph of 2 graphs", currentTask);

var ran = new Random();
int graph1 = ran.Next(graphs.Count);
int graph2 = ran.Next(graphs.Count);
while(graph2 == graph1)
    graph2 = ran.Next(graphs.Count);

Console.WriteLine($"Calculating maximum common subgraph for Graph #{graph1} and Graph #{graph2}:");
var graphsForSubgraph = new List<UndirectedGraph<int, UndirectedEdge<int>>>
{
  graphs[graph1]!,
  graphs[graph2]!
};
MaximalCommonSubgraphAlgorithm.Calculate(graphsForSubgraph);
