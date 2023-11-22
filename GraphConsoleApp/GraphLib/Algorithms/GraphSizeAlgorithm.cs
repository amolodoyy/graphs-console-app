using QuikGraph;

namespace GraphLib.Algorithms
{
    public static class GraphSizeAlgorithm<TVertex, TEdge> where TEdge : IEdge<TVertex>
    {
        public static int GetSize(AdjacencyGraph<TVertex, TEdge> graph) => graph.VertexCount + graph.EdgeCount;
    }
}
