﻿using GraphConsoleApp;
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
ConsoleHelper.WriteInfo("Task 1");

/*

foreach(var graph in graphs)
{
    // Do something with graph, for example:
    ConsoleHelper.WriteSeparator();
    ConsoleHelper.WriteTaskMessage(
        "Graph size (sum of vertices and UndirectedEdges) is " +
        GraphSizeAlgorithm<int, UndirectedEdge<int>>.GetSize(graph!),
        currentTask);
}
*/
currentTask = TaskEnum.Task2;

ConsoleHelper.WriteTaskMessage("The Bron-Kerbosh algorithm with pivoting is used to calculate the clique.", currentTask);

var graph_1 = graphs[1];

var cliquesList = MaximumCliqueAlgorithm.FindAll(graph_1!);

foreach (var clique in cliquesList){
  Console.WriteLine($"[ {ConsoleHelper.ListToString(clique.Vertices.ToList())}]");
}


var last2Graphs = graphs.Skip(Math.Max(0, graphs.Count - 2)).ToList();

MaximalCommonSubgraphAlgorithm.Calculate(last2Graphs!);
