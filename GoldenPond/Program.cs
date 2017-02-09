using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GoldenPond
{
    internal static class Program
    {
        private const string InputFile = "DefaultInputFile.txt";

        private static void Main()
        {
            ReadImputFile();
            var goldenPond = Initalize();
            goldenPond.MoveDucks();
            goldenPond.PrintDuckInfo();
            Console.ReadLine();
        }

        private static Pond Initalize()
        {
            var goldenPond = new Pond();
            var inputCommands = ReadImputFile();

            for (var lineNumber = 0; lineNumber < inputCommands.Count; lineNumber++)
            {
                if (lineNumber == 0)
                {
                    var pondDementions = inputCommands[lineNumber].Split(' '); // first line is the pond size

                    goldenPond.Width = int.Parse(pondDementions[0]);
                    goldenPond.Height = int.Parse(pondDementions[1]);
                }
                else if (lineNumber % 2 == 1)
                {
                    var duckParameters = inputCommands[lineNumber].Split(' '); // odd numbered line is a new duck

                    goldenPond.DuctDataList.Add(new Duck
                    {
                        DuckNumber = lineNumber + 1,
                        Position = new Position { X = int.Parse(duckParameters[0]), Y = int.Parse(duckParameters[1]) },
                        Direction = duckParameters[2].ConvertCharToDirection(),
                        Commands = GetCommandList(inputCommands[lineNumber + 1]) // even numbered line is the duck's commands
                    });
                }
            }

            return goldenPond;
        }

        private static List<Motion> GetCommandList(string commands) => commands.Select(c => c.ConvertCharToMotion()).ToList();

        private static Direction ConvertCharToDirection(this string c)
        {
            Direction direction;

            switch (c)
            {
                case "N":
                    direction = Direction.North;
                    break;
                case "S":
                    direction = Direction.South;
                    break;
                case "E":
                    direction = Direction.East;
                    break;
                case "W":
                    direction = Direction.West;
                    break;
                default:
                    throw new ArgumentException($"'{c}' is not a valid direction.");
            }

            return direction;
        }

        private static Motion ConvertCharToMotion(this char c)
        {
            Motion direction;

            switch (c)
            {
                case 'P':
                    direction = Motion.Port;
                    break;
                case 'S':
                    direction = Motion.Starboard;
                    break;
                case 'F':
                    direction = Motion.Forward;
                    break;
                default:
                    throw new ArgumentException($"'{c}' is not a valid direction.");
            }

            return direction;
        }

        private static List<string> ReadImputFile()
        {
            var inputCommands = new List<string>();

            try
            {
                using (var streamReader = new StreamReader(InputFile))
                {
                    while (streamReader.Peek() >= 0)
                    {
                        inputCommands.Add(streamReader.ReadLine());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            if (inputCommands.Count < 3 || inputCommands.Count % 2 != 1)
            {
                throw new ArgumentException($"Input file '{InputFile}' is not properly formatted.");
            }

            return inputCommands;
        }
    }
}
