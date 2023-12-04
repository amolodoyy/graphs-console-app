using System.Linq;
using GraphLib.Algorithms;
using GraphLib.Utils;
using QuikGraph;

namespace GraphLib
{
    public static class MaximumCliqueAlgorithm
    {
        // add timer
        // clique for multigraphs - most number of edges in most number of vertices
        // extend to have a few cliques
        public static List<UndirectedGraph<int, Edge<int>>> FormGraphFromVertices(UndirectedGraph<int, Edge<int>> undirectedGraph)
        {
            var R = new List<int>();
            var X = new List<int>();
            var P = undirectedGraph.Vertices.ToList();
            var cliques = new List<List<int>>();

            BronKerbosh(undirectedGraph, R, P, X, cliques);
            var resGraphs = new List<UndirectedGraph<int, Edge<int>>>();

            // getting the clique as a subgraph of the original graph using obtained clique vertices
            foreach (var clique in cliques)
            {
              var graphCopy = undirectedGraph.Clone();
              graphCopy.RemoveVertexIf(v => clique.Contains(v));
              graphCopy.RemoveEdgeIf(e => clique.Contains(e.ToVertexPair().Source) || clique.Contains(e.ToVertexPair().Target));
              resGraphs.Add(graphCopy);
            }

            var maxCliqueSize = resGraphs.Select(GraphSizeAlgorithm<int, Edge<int>>.GetSize).OrderByDescending(x => x).FirstOrDefault();
            resGraphs = resGraphs.Where(g => GraphSizeAlgorithm<int, Edge<int>>.GetSize(g) == maxCliqueSize).ToList();

            return resGraphs;
        }
        private static void BronKerbosh(UndirectedGraph<int, Edge<int>> graph, List<int> R, List<int> P, List<int> X, List<List<int>> cliques)
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
    }
}
