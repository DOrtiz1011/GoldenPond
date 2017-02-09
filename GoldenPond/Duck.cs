using System.Collections.Generic;

namespace GoldenPond
{
    internal sealed class Duck
    {
        public int DuckNumber { get; set; }
        public Position Position { get; set; }
        public Direction Direction { get; set; }
        public List<Motion> Commands { get; set; }
    }
}
