using System.Collections;
using GraphLib.Utils;
using QuikGraph;

namespace GraphLib.Algorithms;

public static class ModularProductAlgorithm
{
    public static UndirectedGraph<string, UndirectedEdge<string>> CalculateProductString(List<UndirectedGraph<int, UndirectedEdge<int>>> graphs)
    {
        var stringGraph = GraphTypesConverter.IntRangeToStringRange(graphs).ToList();
        var currentGraph = GraphTypesConverter.IntToString(graphs.FirstOrDefault()!);

        for (int i = 1; i < stringGraph.Count; i++)
        {
            currentGraph = CalculatePairProductString(currentGraph, stringGraph[i]);
        }
        return currentGraph;
    }

    // public static UndirectedGraph<List<int>, UndirectedEdge<List<int>>> CalculateProduct(List<UndirectedGraph<int, UndirectedEdge<int>>> graphs)
    // {
    //     var currentGraph = GraphTypesConverter.IntToListInt(graphs.FirstOrDefault()!);

    //     for (int i = 1; i < graphs.Count; i++)
    //     {
    //         currentGraph = CalculatePairProduct(currentGraph!, graphs[i]);
    //     }
    //     return currentGraph;
    // }

    private static UndirectedGraph<string, UndirectedEdge<string>> CalculatePairProductString(
    UndirectedGraph<string, UndirectedEdge<string>> graph_1,
    UndirectedGraph<string, UndirectedEdge<string>> graph_2)
    {
        var modularProduct = new UndirectedGraph<string, UndirectedEdge<string>>();

        // add vertices (which are represented by pairs of vertices in a graph)
        foreach (var vertex_1 in graph_1.Vertices)
        {
            foreach (var vertex_2 in graph_2.Vertices)
            {
                modularProduct.AddVertex(vertex_1 + "_" + vertex_2);
            }
        }

        foreach (var vertex_1 in graph_1.Vertices)
        {
            foreach (var vertex_2 in graph_2.Vertices)
            {
                var vertexToAddTo = vertex_1 + "_" + vertex_2;

                // case when both are adjacent

                var leftAdjacentVertices = graph_1.AdjacentVertices(vertex_1).ToList();
                var rightAdjacentVertices = graph_2.AdjacentVertices(vertex_2).ToList();

                var adjacentVertices = leftAdjacentVertices.SelectMany(x => rightAdjacentVertices, (x, y) =>
                {
                    return x + "_" + y;
                });

                foreach (var vtx in adjacentVertices)
                {
                  var source = MinVertex(vertexToAddTo, vtx);
                  var dest = MaxVertex(vertexToAddTo, vtx);

                  if (modularProduct.TryGetEdge(source, dest, out var edge)) continue;
                  modularProduct.AddEdge(new UndirectedEdge<string>(source, dest));
                }

                // case when both are not adjacent

                var leftNonAdjacent = graph_1.Vertices.Except(leftAdjacentVertices).ToList();
                var rightNonAdjacent = graph_2.Vertices.Except(rightAdjacentVertices).ToList();

                leftNonAdjacent.Remove(vertex_1);
                rightNonAdjacent.Remove(vertex_2);

                var nonAdjacentVertices = leftNonAdjacent.SelectMany(x => rightNonAdjacent, (x, y) =>
                {
                    return x + "_" + y;
                });

                foreach (var non_vtx in nonAdjacentVertices)
                {
                    var source = MinVertex(vertexToAddTo, non_vtx);
                    var dest = MaxVertex(vertexToAddTo, non_vtx);

                    if (modularProduct.TryGetEdge(source, dest, out var edge)) continue;
                    modularProduct.AddEdge(new UndirectedEdge<string>(source, dest));
                }
                // 1_2_3, nav = { "1_1_1", "3_2_1" }
            }
        }
        return modularProduct;
    }

    // private static UndirectedGraph<List<int>, UndirectedEdge<List<int>>> CalculatePairProduct(
    //     UndirectedGraph<List<int>, UndirectedEdge<List<int>>> graph_1,
    //     UndirectedGraph<int, UndirectedEdge<int>> graph_2)
    // {
    //     var modularProduct = new UndirectedGraph<List<int>, UndirectedEdge<List<int>>>();

    //     // add vertices (which are represented by pairs of vertices in a graph)

    //     foreach (var vertex_1 in graph_1.Vertices)
    //     {
    //         foreach (var vertex_2 in graph_2.Vertices)
    //         {
    //             var vertexToAdd = new List<int>();
    //             vertexToAdd.AddRange(vertex_1);
    //             vertexToAdd.Add(vertex_2);
    //             modularProduct.AddVertex(vertexToAdd);
    //         }
    //     }

    //     // connect vertices

    //     foreach (var vertex_1 in graph_1.Vertices)
    //     {
    //         foreach (var vertex_2 in graph_2.Vertices)
    //         {
    //             var vertexToAddTo = new List<int>();
    //             vertexToAddTo.AddRange(vertex_1); vertexToAddTo.Add(vertex_2);

    //             // case when both are adjacent

    //             var leftAdjacentVertices = graph_1.AdjacentVertices(vertex_1).ToList();
    //             var rightAdjacentVertices = graph_2.AdjacentVertices(vertex_2).ToList();

    //             var adjacentVertices = leftAdjacentVertices.SelectMany(x => rightAdjacentVertices, (x, y) =>
    //             {
    //                 var res = new List<int>();
    //                 res.AddRange(x); res.Add(y);
    //                 return res;
    //             });

    //             foreach(var vtx in adjacentVertices)
    //             {
    //                 modularProduct.AddEdge(new UndirectedEdge<List<int>>(vertexToAddTo, vtx));
    //             }

    //             // case when both are not adjacent

    //             var leftNonAdjacent = graph_1.Vertices.Except(leftAdjacentVertices);
    //             var rightNonAdjacent = graph_2.Vertices.Except(rightAdjacentVertices);

    //             var nonAdjacentVertices = leftNonAdjacent.SelectMany(x => rightNonAdjacent, (x, y) =>
    //             {
    //                 var res = new List<int>();
    //                 res.AddRange(x); res.Add(y);
    //                 return res;
    //             });
    //             foreach(var non_vtx in nonAdjacentVertices)
    //             {
    //                 modularProduct.AddEdge(new UndirectedEdge<List<int>>(vertexToAddTo, non_vtx));
    //             }
    //         }
    //     }

    //     return modularProduct;

    // }
    private static string MinVertex(string vertex1, string vertex2)
    {
      return String.Compare(vertex1, vertex2) < 0 ? vertex1 : vertex2;
    }
    private static string MaxVertex(string vertex1, string vertex2)
    {
      return String.Compare(vertex1, vertex2) > 0 ? vertex1 : vertex2;
    }

}

/*
 * G1 G2 G3 , V1, V2, V3 = {1,2,3}
 * G1 * G2 * G3 = (G1 * G2) * G3
 * G1<int> * G2<int> = G_Res<int,int>
 * G_Res<int, int> * G3<int> = G_Res2 = G_Res2<int,int,int>
 *
 * 1_1_23
 * 11_2_3
 *
 */
