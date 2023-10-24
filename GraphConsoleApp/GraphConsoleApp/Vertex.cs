namespace GraphConsoleApp
{
    public class Vertex
    {
        private int _vertex = 0;
        public int Value { get { return _vertex; } set { _vertex = value; } }
        public Vertex() { }
        public Vertex(int v) { Value = v; }
    }
}