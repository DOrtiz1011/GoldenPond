using System;

namespace GoldenPond
{
    internal static class Program
    {
        private const string InputFile = "DefaultInputFile.txt";

        private static void Main()
        {
            var goldenPond = new Pond();

            goldenPond.Initalize(InputFile);
            goldenPond.MoveDucks();
            goldenPond.PrintDuckInfo();

            Console.ReadLine();
        }
    }
}
