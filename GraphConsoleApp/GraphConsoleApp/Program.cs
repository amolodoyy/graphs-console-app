using GraphConsoleApp;

const string dirPath = ".\\Graphs\\";

Console.WriteLine("Starting application...\n");
Thread.Sleep(1000);

Console.WriteLine("Taking graphs from `Graphs` folder...\n");
Thread.Sleep(1000);

if(!Directory.Exists(dirPath))
{
    Console.WriteLine("Cannot find `Graphs` folder, exiting.\n");
    return;
}

var filePaths = Directory.GetFiles(dirPath);

var graphs = filePaths.Select(f => GraphTxtReader.FromTxt(f)).ToList();

foreach(var graph in graphs)
{
    // Do something with graph, for example:
    Console.WriteLine("====================================================");
    Console.WriteLine("Graph size (sum of vertices and edges) is " + (graph.VertexCount + graph.EdgeCount) + "\n");
}