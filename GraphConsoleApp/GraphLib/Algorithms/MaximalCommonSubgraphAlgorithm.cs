using QuikGraph;

namespace GraphLib.Algorithms;

public static class MaximalCommonSubgraphAlgorithm
{
    public static UndirectedGraph<int, UndirectedEdge<int>> Calculate(
        List<UndirectedGraph<int, UndirectedEdge<int>>> graphs)
    {
        var modularProduct = ModularProductAlgorithm.CalculateProduct(graphs);

        return new UndirectedGraph<int, UndirectedEdge<int>>();
    }
}
