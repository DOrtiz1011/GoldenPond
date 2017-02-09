using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenPond
{
    class Program
    {
        static void Main(string[] args)
        {
            var goldenPond = new Pond(5, 5);

            goldenPond.DuckList.Add(new Duck(1, 2, Direction.North));
            goldenPond.DuckList.Add(new Duck(3, 3, Direction.East));
        }
    }
}
