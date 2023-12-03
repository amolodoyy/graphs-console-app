namespace GraphLib.Models
{
    public sealed class Vertex 
    {
        public int Id;
        private int x;
        private int y;
        private string name;
        private Vertex vertex;

        public int X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }
        public int CentreX
        {
            get
            {
                return x + 25;
            }
        }
        public int CentreY
        {
            get
            {
                return y + 25;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public Vertex(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Vertex(Vertex vertex)
        {
            this.x = vertex.x;
            this.y = vertex.y;
            this.Id = vertex.Id;
            this.Name = vertex.Name;
        }
    }
}
