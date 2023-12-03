using GraphLib.Models;
using GraphLib.Utils;
using QuikGraph;
using System.Collections.ObjectModel;

namespace GraphLib
{
    public static class MaximumCliqueAlgorithm
    {
        //Условие в основном цикле: not должно содержать вершины, НЕ СОЕДИНЕННЫХ НИ С ОДНОЙ из вершин во множестве candidates
        private static bool CheckIfConnected(List<Vertex> candidates, List<Vertex> not, AdjancencyMatrix adjMatrix)
        {
            for (int i = 0; i < not.Count; i++)
            {
                int l = 0;
                for (int j = 0; j < candidates.Count; j++)
                    if (adjMatrix[candidates[j].Id, not[i].Id] || adjMatrix[not[i].Id, candidates[j].Id])
                        l++;

                if (l == 0)
                    return true;
            }


            return false;
        }
        private static List<List<Vertex>> result = new List<List<Vertex>>();
        private static void Extend(List<Vertex> compsub, List<Vertex> candidates, List<Vertex> not, AdjancencyMatrix adjMatrix)
        {
            while (candidates.Count != 0 && !CheckIfConnected(candidates, not, adjMatrix))
            {
                //Для формирования new_candidates и new_not, необходимо удалять из candidates и not вершины, СОЕДИНЕННЫЕ с выбранной вершиной.

                Vertex v = candidates[0];
                if (adjMatrix[v.Id, v.Id])
                {
                    candidates.Remove(v);
                    continue;
                }
                compsub.Add(v);
                List<Vertex> new_candidates = new List<Vertex>(candidates);
                new_candidates.RemoveAt(0);
                new_candidates.RemoveAll(p => adjMatrix[v.Id, p.Id] || adjMatrix[p.Id, v.Id] || adjMatrix[p.Id, p.Id]);

                List<Vertex> new_not = new List<Vertex>(not);
                new_not.RemoveAll(p => adjMatrix[v.Id, p.Id] || adjMatrix[p.Id, v.Id] || adjMatrix[p.Id, p.Id]);
                if (new_not.Count == 0 && new_candidates.Count == 0)
                    result.Add(compsub.ToList());
                else
                    Extend(compsub, new_candidates, new_not, adjMatrix);
                compsub.Remove(v);
                candidates.Remove(v);
                not.Add(v);
            }
        }

        public static List<List<Vertex>> Alghorithm(AdjancencyMatrix adjMatrix, ObservableCollection<Vertex> vertices)
        {
            result = new List<List<Vertex>>();

            Extend(new List<Vertex>(), new List<Vertex>(vertices), new List<Vertex>(), adjMatrix);
            return result;
        }
    }
}
