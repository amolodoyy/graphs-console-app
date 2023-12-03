namespace GraphLib.Models
{
    public sealed class AdjancencyMatrix
    {
        private bool[,] adjancencyMatrix;
        public AdjancencyMatrix()
        {
            adjancencyMatrix = new bool[2, 2];
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                    adjancencyMatrix[i, j] = false;
        }
        public AdjancencyMatrix(int countOfVertices)
        {
            adjancencyMatrix = new bool[countOfVertices, countOfVertices];
            for (int i = 0; i < countOfVertices; i++)
                for (int j = 0; j < countOfVertices; j++)
                    adjancencyMatrix[i, j] = false;
        }

        public bool this[int i, int j]
        {
            get
            {
                return adjancencyMatrix[i, j];
            }
        }

        public int Size
        {
            get
            {
                return adjancencyMatrix.GetLength(0);
            }
        }

        private void Resize(int newSize)
        {
            var newArray = new bool[newSize, newSize];
            int minRows = Math.Min(newSize, adjancencyMatrix.GetLength(0));
            int minCols = Math.Min(newSize, adjancencyMatrix.GetLength(1));
            for (int i = 0; i < minRows; i++)
                for (int j = 0; j < minCols; j++)
                    newArray[i, j] = adjancencyMatrix[i, j];
            adjancencyMatrix = newArray;

        }

        public void AddTwoWay(Vertex first, Vertex second)
        {
            int max = Math.Max(first.Id, second.Id);

            if (max >= Size)
                Resize(2 * max);
            adjancencyMatrix[first.Id, second.Id] = true;
            adjancencyMatrix[second.Id, first.Id] = true;
        }

        public void AddOneWay(Vertex first, Vertex second)
        {
            int max = Math.Max(first.Id, second.Id);

            if (max >= Size)
                Resize(2 * max);
            adjancencyMatrix[first.Id, second.Id] = true;
        }

        public void RemoveVertex(Vertex v)
        {
            for (int i = 0; i < Size; i++)
            {
                adjancencyMatrix[v.Id, i] = false;
                adjancencyMatrix[i, v.Id] = false;
            }
        }
        public void RemoveTwoWay(Vertex first, Vertex second)
        {
            adjancencyMatrix[first.Id, second.Id] = false;
            adjancencyMatrix[second.Id, first.Id] = false;
        }
        public void RemoveOneWay(Vertex first, Vertex second)
        {
            adjancencyMatrix[first.Id, second.Id] = false;
        }
        public void RemoveAll()
        {
            for (int i = 0; i < Size; i++)
                for (int j = 0; j < Size; j++)
                    adjancencyMatrix[i, j] = false;
        }
    }
}

