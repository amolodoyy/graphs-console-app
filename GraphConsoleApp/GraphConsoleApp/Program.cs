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

var graphsInfo = filePaths.Select(f => GraphTxtReader.FromTxt(f)).Where(pair => pair.Item1 is not null && pair.Item2 is not null).ToList();
ConsoleHelper.WriteSeparator();
ConsoleHelper.WriteTaskMessage("Computing the size of the graphs (sum of vertices and UndirectedEdges).", currentTask);

for (int i = 0; i < graphsInfo.Count; i++)
{
  ConsoleHelper.StartTimer();
  Console.WriteLine(
      $"Size of {graphsInfo[i].Item2}: " +
      GraphSizeAlgorithm<int, UndirectedEdge<int>>.GetSize(graphsInfo[i].Item1!),
      currentTask);
  ConsoleHelper.StopTimer();
}

currentTask = TaskEnum.Task2;
ConsoleHelper.WriteSeparator();
ConsoleHelper.WriteTaskMessage("Calculating the maximum cliques of the graphs", currentTask);

for (int i = 0; i < graphsInfo.Count; i++)
{
  ConsoleHelper.StartTimer();
  var maxClique = MaximumCliqueAlgorithm.FindAll(graphsInfo[i].Item1!);
  Console.WriteLine($"Maximum clique of {graphsInfo[i].Item2}:");

  if (maxClique.EdgeCount > 100) {
    Console.WriteLine("(Clique has too many edges to print, printing only vertices)");
    Console.WriteLine($"[ {ConsoleHelper.ListToString(maxClique.Vertices.ToList())}]");
  } else {
    Console.WriteLine("(Printing edges)");
    Console.WriteLine(ConsoleHelper.FormatGraph(maxClique));
  }

  ConsoleHelper.StopTimer();
  Console.WriteLine();
}

currentTask = TaskEnum.Task3;
ConsoleHelper.WriteSeparator();
ConsoleHelper.WriteTaskMessage("Calculating the maximum common subgraph of 2 graphs", currentTask);

var ran = new Random();
int graph1 = ran.Next(graphsInfo.Count);
int graph2 = ran.Next(graphsInfo.Count);
while (graph2 == graph1)
  graph2 = ran.Next(graphsInfo.Count);

Console.WriteLine($"Calculating maximum common subgraph for {graphsInfo[graph1].Item1} and {graphsInfo[graph2].Item2}:");
var graphsForSubgraph = new List<UndirectedGraph<int, UndirectedEdge<int>>>
{
  graphsInfo[graph1].Item1!,
  graphsInfo[graph2].Item1!
};
ConsoleHelper.StartTimer();
MaximalCommonSubgraphAlgorithm.Calculate(graphsForSubgraph);
ConsoleHelper.StopTimer();
