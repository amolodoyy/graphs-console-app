using QuikGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLib.Algorithms
{
    public static class GraphSizeAlgorithm<TVertex, TEdge> where TEdge : IEdge<TVertex>
    {
        public static int GetSize(AdjacencyGraph<TVertex, TEdge> graph) => graph.VertexCount + graph.EdgeCount;
    }
}
