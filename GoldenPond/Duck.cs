using System.Collections.Generic;

namespace GoldenPond
{
    internal sealed class Duck
    {
        public Position Position { get; set; }
        public Direction Direction { get; set; }
        public List<Motion> MotionList { get; set; }
    }
}
