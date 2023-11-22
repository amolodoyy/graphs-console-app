using QuikGraph;
using QuikGraph.Algorithms.Cliques;
using QuikGraph.Algorithms.Services;

namespace GraphLib
{
    public class MaximumCliqueAlgorithm<TVertex, TEdge> : MaximumCliqueAlgorithmBase<TVertex, TEdge> where TEdge : IEdge<TVertex>
    {
        public MaximumCliqueAlgorithm(IUndirectedGraph<TVertex, TEdge> visitedGraph) : base(visitedGraph)
        {

        }

        public MaximumCliqueAlgorithm(IAlgorithmComponent host, IUndirectedGraph<TVertex, TEdge> visitedGraph) : base(host, visitedGraph)
        {

        }

        protected override void InternalCompute()
        {

        }
    }
}