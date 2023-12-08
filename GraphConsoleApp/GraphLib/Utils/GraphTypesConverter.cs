using QuikGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLib.Utils;

internal static class GraphTypesConverter
{
    public static IEnumerable<UndirectedGraph<string, UndirectedEdge<string>>> IntRangeToStringRange(IEnumerable<UndirectedGraph<int, UndirectedEdge<int>>> intGraphs)
    {
        var stringGraphs = new List<UndirectedGraph<string, UndirectedEdge<string>>>();
        foreach (var intGraph in intGraphs)
        {
            stringGraphs.Add(IntToString(intGraph));
        }
        return stringGraphs;
    }

    public static UndirectedGraph<List<int>, UndirectedEdge<List<int>>> IntToListInt(UndirectedGraph<int, UndirectedEdge<int>> intGraph)
    {
        var resultingGraph = new UndirectedGraph<List<int>, UndirectedEdge<List<int>>>();
        foreach (var v in intGraph.Vertices)
        {
            resultingGraph.AddVertex(new List<int>() { v });
        }

        foreach (var e in intGraph.Edges)
        {
            var source = new List<int>() { e.Source };
            var dest = new List<int>() { e.Target };

            resultingGraph.AddEdge(new UndirectedEdge<List<int>>(source, dest));
        }

        return resultingGraph;
    }
    private static UndirectedGraph<string, UndirectedEdge<string>> IntToString(UndirectedGraph<int, UndirectedEdge<int>> intGraph)
    {
        var stringGraph = new UndirectedGraph<string, UndirectedEdge<string>>();

        foreach (var vertex in intGraph.Vertices)
        {
            stringGraph.AddVertex(vertex.ToString());
        }

        foreach (var UndirectedEdge in intGraph.Edges)
        {
            string source = UndirectedEdge.Source.ToString();
            string target = UndirectedEdge.Target.ToString();

            stringGraph.AddVerticesAndEdge(new UndirectedEdge<string>(source, target));
        }

        return stringGraph;
    }
}