﻿using GraphLib.Algorithms;
using QuikGraph;

namespace GraphLib;

public static class MaximumCliqueAlgorithm
{
    public static UndirectedGraph<int, UndirectedEdge<int>> FindAll(UndirectedGraph<int, UndirectedEdge<int>> undirectedGraph)
    {
        var R = new List<int>();
        var X = new List<int>();
        var P = undirectedGraph.Vertices.ToList();
        var cliques = new List<List<int>>();

        if (GraphSizeAlgorithm<int, UndirectedEdge<int>>.GetSize(undirectedGraph) > 2000) {
          Console.WriteLine("Graph is too large, applying Tabu Search algorithm approximation...");
          cliques = TabuSearch(undirectedGraph);
        } else {
          Console.WriteLine("Applying Bron-Kerbosh algorithm...");
          BronKerbosh(undirectedGraph, R, P, X, cliques);
        }
        var resGraphs = new List<UndirectedGraph<int, UndirectedEdge<int>>>();

        // getting the clique as a subgraph of the original graph using obtained clique vertices
        foreach (var clique in cliques)
        {
          var graphCopy = undirectedGraph.Clone();
          graphCopy.RemoveVertexIf(v => !clique.Contains(v));
          graphCopy.RemoveEdgeIf(e => !clique.Contains(e.ToVertexPair().Source) && !clique.Contains(e.ToVertexPair().Target));
          resGraphs.Add(graphCopy);
        }

        // finding maximum clique
        var maximumClique = resGraphs.OrderByDescending(x => x.VertexCount).FirstOrDefault();

        return maximumClique;
    }
    private static void BronKerbosh(UndirectedGraph<int, UndirectedEdge<int>> graph, List<int> R, List<int> P, List<int> X, List<List<int>> cliques)
    {
        if(P.Count == 0 && X.Count == 0)
        {
            cliques.Add(R);
        }

        // choose a pivot
        var pivot = P.Union(X)
            .ToList()
            .OrderByDescending(x => graph.AdjacentDegree(x))
            .FirstOrDefault();
        var tmp = graph.AdjacentVertices(pivot);

        var pCopy = new List<int>(P);
        pCopy.RemoveAll(v => graph.AdjacentVertices(pivot).Contains(v));
        foreach(var v in pCopy)
        {
            var vList = new List<int> { v };
            var neighbors_pList = graph.AdjacentVertices(v);
            var rUpdated = R.Union(vList).ToList();
            BronKerbosh(graph,
                rUpdated,
                P.Intersect(neighbors_pList).ToList(),
                X.Intersect(neighbors_pList).ToList(),
                cliques);
            P.Remove(v);
            X.Add(v);
        }
    }

    private static List<int> GreedyAlgorithm(UndirectedGraph<int, UndirectedEdge<int>> undirectedGraph) {
      var vertices = undirectedGraph.Vertices;

      var r = new Random();
      var randomIndex = r.Next(undirectedGraph.VertexCount);
      var clique = new List<int> { undirectedGraph.Vertices.ToList()[randomIndex] };

      foreach (var vertex in vertices) {
        if (clique.Contains(vertex)) continue;

        // Check if vertex is adjacent to all vertices of the clique
        var shouldAddToClique = undirectedGraph.AdjacentVertices(vertex).Intersect(clique).Count() == clique.Count();
        if (shouldAddToClique) clique.Add(vertex);
      }

      return clique;
    }

    public static List<List<int>> TabuSearch(UndirectedGraph<int, UndirectedEdge<int>> graph) {
      var vertices = graph.Vertices;
      const int maxIterations = 200;

      var random = new Random();
      var randomIndex = random.Next(graph.VertexCount);
      var clique = new List<int> { graph.Vertices.ToList()[randomIndex] };

      var bestClique = new List<int>();
      bestClique.AddRange(clique);
      int bestSize = bestClique.Count;

      var maximalCliques = new List<List<int>>();

      // Indexes correspond to verteces, values correspond to iteration number when vertex was added
      int[] nodeTabuList = new int[graph.VertexCount];
      for (int i = 0; i < nodeTabuList.Length; i++)
        nodeTabuList[i] =  int.MinValue;

      for (int i = 0; i < maxIterations || maximalCliques.Count == 0; i++) {
        if (clique.Count() == graph.VertexCount)
        {
          var fullClique = new List<int>();
          fullClique.AddRange(graph.Vertices);
          maximalCliques.Add(fullClique);
          break;
        }

        var cliqueChanged = false;
        var nodeOptions = GetNodeOptions(clique, graph);

        if (nodeOptions.Count > 0) {
          var allowedNodes = GetAllowedNodeOptions(nodeTabuList, nodeOptions, i);
          if (allowedNodes.Count > 0) {
            var bestNode = allowedNodes.OrderByDescending(v => graph.AdjacentDegree(v)).FirstOrDefault();
            clique.Add(bestNode);
            clique.Sort();
            nodeTabuList[bestNode] = i;
            cliqueChanged = true;
          }
        }

        if (!cliqueChanged && clique.Count() != 0) {
          maximalCliques.Add(clique.ToList());
          var nodeToRemove = clique[random.Next(0, clique.Count)];
          clique.Remove(nodeToRemove);
          clique.Sort();
          nodeTabuList[nodeToRemove] = i;
        }
      }

      return maximalCliques.Select(x => new HashSet<int>(x))
                .Distinct(HashSet<int>.CreateSetComparer()).Select(x => x.ToList()).ToList();
    }

    private static List<int> GetNodeOptions(List<int> cliqueVertices, UndirectedGraph<int, UndirectedEdge<int>> graph) {
      var nodeOptions = new List<int>();

      foreach (var vertex in graph.Vertices) {
        // Check if vertex is adjacent to all vertices of the clique
        if (graph.
            AdjacentVertices(vertex).
            Intersect(cliqueVertices).
            Count() == cliqueVertices.Count)
        nodeOptions.Add(vertex);
      }

      return nodeOptions;
    }

    private static List<int> GetAllowedNodeOptions(int[] nodeTabuList, List<int> nodeOptions, int currentIteration) {
      var allowedNodeOptions = new List<int>();

      foreach (var node in nodeOptions) {
        // Node is allowed only if it was tabooed more than 1 iteration ago
        if (nodeTabuList[node] + 1 < currentIteration)
          allowedNodeOptions.Add(node);
      }

      return allowedNodeOptions;
    }
}
