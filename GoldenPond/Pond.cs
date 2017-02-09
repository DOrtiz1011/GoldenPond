using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GoldenPond
{
    internal sealed class Pond
    {
        private int Height { get; set; }
        private int Width { get; set; }

        private readonly List<Duck> DuctDataList = new List<Duck>();

        public void Initalize(string inputFile)
        {
            var inputCommands = ReadImputFile(inputFile);

            for (var lineNumber = 0; lineNumber < inputCommands.Count; lineNumber++)
            {
                if (lineNumber == 0)
                {
                    var pondDementions = inputCommands[lineNumber].Split(' '); // first line is the pond size

                    Width = int.Parse(pondDementions[0]);
                    Height = int.Parse(pondDementions[1]);
                }
                else if (lineNumber % 2 == 1)
                {
                    var duckParameters = inputCommands[lineNumber].Split(' '); // odd numbered line is a new duck

                    DuctDataList.Add(new Duck
                    {
                        Position = new Position { X = int.Parse(duckParameters[0]), Y = int.Parse(duckParameters[1]) },
                        Direction = ConvertStringToDirection(duckParameters[2]),
                        MotionList = GetMotionList(inputCommands[lineNumber + 1]) // even numbered line is the duck's commands
                    });
                }
            }
        }

        private static Direction ConvertStringToDirection(string str)
        {
            Direction direction;

            switch (str)
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
                    throw new ArgumentException($"'{str}' is not a valid direction.");
            }

            return direction;
        }

        private static List<Motion> GetMotionList(string commands) => commands.Select(ConvertCharToMotion).ToList();

        private static Motion ConvertCharToMotion(char c)
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

        private static List<string> ReadImputFile(string inputFile)
        {
            var inputCommands = new List<string>();

            using (var streamReader = new StreamReader(inputFile))
            {
                while (streamReader.Peek() >= 0)
                {
                    inputCommands.Add(streamReader.ReadLine());
                }
            }

            if (inputCommands.Count < 3 || inputCommands.Count % 2 != 1)
            {
                throw new ArgumentException($"Input file '{inputFile}' is not properly formatted.");
            }

            return inputCommands;
        }

        public void PrintDuckInfo()
        {
            foreach (var duckData in DuctDataList)
            {
                Console.WriteLine($"{duckData.Position.X} {duckData.Position.Y} {duckData.Direction}");
            }
        }

        public void MoveDucks()
        {
            if (Height < 1 || Width < 1)
            {
                throw new ArgumentException("Pond dimentions have not been set.");
            }

            foreach (var duck in DuctDataList)
            {
                foreach (var motion in duck.MotionList)
                {
                    ExecuteCommand(duck, motion);
                }
            }
        }

        private void ExecuteCommand(Duck duck, Motion motion)
        {
            switch (motion)
            {
                case Motion.Port:
                case Motion.Starboard:
                    Rotate(duck, motion);
                    break;
                case Motion.Forward:
                    Move(duck);
                    break;
            }
        }

        private static void Rotate(Duck duck, Motion motion)
        {
            if (motion == Motion.Port)
            {
                switch (duck.Direction)
                {
                    case Direction.North:
                        duck.Direction = Direction.West;
                        break;
                    case Direction.South:
                        duck.Direction = Direction.East;
                        break;
                    case Direction.East:
                        duck.Direction = Direction.North;
                        break;
                    case Direction.West:
                        duck.Direction = Direction.South;
                        break;
                }
            }
            else if (motion == Motion.Starboard)
            {
                switch (duck.Direction)
                {
                    case Direction.North:
                        duck.Direction = Direction.East;
                        break;
                    case Direction.South:
                        duck.Direction = Direction.West;
                        break;
                    case Direction.East:
                        duck.Direction = Direction.South;
                        break;
                    case Direction.West:
                        duck.Direction = Direction.North;
                        break;
                }
            }
        }

        private void Move(Duck duck)
        {
            switch (duck.Direction)
            {
                case Direction.North:
                    if (IsMoveValid(duck.Position.X, duck.Position.Y + 1))
                    {
                        duck.Position.Y++;
                    }
                    break;
                case Direction.South:
                    if (IsMoveValid(duck.Position.X, duck.Position.Y - 1))
                    {
                        duck.Position.Y--;
                    }
                    break;
                case Direction.East:
                    if (IsMoveValid(duck.Position.X + 1, duck.Position.Y))
                    {
                        duck.Position.X++;
                    }
                    break;
                case Direction.West:
                    if (IsMoveValid(duck.Position.X - 1, duck.Position.Y))
                    {
                        duck.Position.X--;
                    }
                    break;
            }
        }

        private bool IsMoveValid(int x, int y) => IsPositionInBounds(x, y) && IsPostionEmpty(x, y);

        private bool IsPostionEmpty(int x, int y) => !DuctDataList.Any(d => d.Position.X == x && d.Position.X == y);

        private bool IsPositionInBounds(int x, int y) => x >= 0 && y >= 0 && x <= Width && y <= Height;
    }
}
