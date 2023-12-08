using QuikGraph;

namespace GraphLib.Algorithms
{
    public static class GraphSizeAlgorithm<TVertex, TUndirectedEdge> where TUndirectedEdge : IUndirectedEdge<TVertex>
    {
        public static int GetSize(UndirectedGraph<TVertex, TUndirectedEdge> graph) => graph.VertexCount + graph.UndirectedEdgeCount;
    }
}
