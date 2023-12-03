using GraphLib.Utils;
using QuikGraph;

namespace GraphLib
{
    public static class MaximumCliqueAlgorithm
    {
        // add timer
        // clique for multigraphs - most number of edges in most number of vertices
        // extend to have a few cliques
        public static List<int> FormGraphFromVertices(UndirectedGraph<int, Edge<int>> undirectedGraph)
        {
            var R = new List<int>();
            var X = new List<int>();
            var P = undirectedGraph.Vertices.ToList();
            var cliques = new List<int>();

            var totalCliques = BronKerbosh(undirectedGraph, R, P, X, cliques);
            /*var resGraph = new UndirectedGraph<int, Edge<int>>();

            resGraph.AddVertexRange(R);
            foreach (var v in R)
            {
                var RWithoutV = R.Where(r => r != v).ToList();
                foreach(var r in RWithoutV)
                {
                    resGraph.AddEdge(new Edge<int>(v, r));
                }
            }
            return resGraph;*/
            return totalCliques;
        }
        private static List<int> BronKerbosh(UndirectedGraph<int, Edge<int>> graph, List<int> R, List<int> P, List<int> X, List<int> cliques)
        {
            if(P.Count == 0 && X.Count == 0)
            {
                Console.WriteLine($"Found clique {ConsoleHelper.ListToString(R)}");
                return R;

            }

            // choose a pivot
            var pivot = P.Union(X)
                .ToList()
                .Select(graph.AdjacentDegree)
                .OrderByDescending(x => x)
                .FirstOrDefault();
            var tmp = graph.AdjacentVertices(pivot);

            var pCopy = new List<int>(P);
            pCopy.RemoveAll(v => graph.AdjacentVertices(pivot).Contains(v));
            foreach(var v in pCopy)
            {
                var vList = new List<int> { v };
                var neighbors_pList = graph.AdjacentVertices(v);
                cliques.Concat(BronKerbosh(graph,
                    R.Union(vList).ToList(),
                    P.Intersect(neighbors_pList).ToList(), 
                    X.Intersect(neighbors_pList).ToList(),
                    cliques));
                P.Remove(v);
                X.Add(v);
            }
            return cliques;
        }
    }
}