using GraphConsoleApp;
using GraphLib;
using GraphLib.Algorithms;
using GraphLib.Enums;
using GraphLib.Utils;
using QuikGraph;

var currentTask = TaskEnum.Task1;

if (!Directory.Exists(AppSettings.SourceDirectoryPath))
{
    ConsoleHelper.WriteError("Cannot find `sources` folder, please check that you've placed your folders correctly.\n");
    ConsoleHelper.WriteInfo("Exiting application...");
    return;
}

var filePaths = Directory.GetFiles(AppSettings.SourceDirectoryPath);

var graphs = filePaths.Select(f => GraphTxtReader.FromTxt(f)).Where(g => g is not null).ToList();

ConsoleHelper.WriteInfo("Task 1");
currentTask = TaskEnum.Task1;

foreach(var graph in graphs)
{
    // Do something with graph, for example:
    ConsoleHelper.WriteSeparator();
    ConsoleHelper.WriteTaskMessage(
        "Graph size (sum of vertices and edges) is " +
        GraphSizeAlgorithm<int, Edge<int>>.GetSize(graph!),
        currentTask);
}

currentTask = TaskEnum.Task2;

ConsoleHelper.WriteTaskMessage("The Bron-Kerbosh algorithm with pivoting is used to calculate the clique.", currentTask);

var graph_1 = graphs.FirstOrDefault();

var cliquesList = MaximumCliqueAlgorithm.FormGraphFromVertices(graph_1!);

Console.WriteLine(ConsoleHelper.ListToString(cliquesList));
