using System.Collections.Generic;

namespace GoldenPond
{
    class Pond
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public List<Duck> DuckList = new List<Duck>();

        public Pond(int height, int width)
        {
            Height = height;
            Width = width;
        }
    }
}
