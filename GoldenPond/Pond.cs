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
        private bool Initialized;

        private readonly List<Duck> DuctList = new List<Duck>();

        public void Initalize(string inputFile)
        {
            var inputCommands = ReadImputFile(inputFile);

            for (var lineNumber = 0; lineNumber < inputCommands.Count; lineNumber++)
            {
                if (lineNumber == 0)
                {
                    IntitalizePondSize(inputCommands[lineNumber]);
                }
                else if (lineNumber % 2 == 1)
                {
                    var duckParameters = inputCommands[lineNumber].Split(' '); // odd numbered line is a new duck

                    DuctList.Add(new Duck
                    {
                        Position = new Position { X = int.Parse(duckParameters[0]), Y = int.Parse(duckParameters[1]) },
                        Direction = ConvertStringToDirection(duckParameters[2]),
                        MotionList = GetMotionList(inputCommands[lineNumber + 1]) // even numbered line is the duck's commands
                    });
                }
            }

            Initialized = true;
        }

        private void IntitalizePondSize(string pondDementions)
        {
            var dementions = pondDementions.Split(' '); // first line is the pond size
            int width;
            int height;

            if (int.TryParse(dementions[0], out width))
            {
                Width = width;
            }
            else
            {
                throw new Exception($"'{dementions[0]}' is not a valid integer for pond width.");
            }

            if (int.TryParse(dementions[1], out height))
            {
                Height = height;
            }
            else
            {
                throw new Exception($"'{dementions[1]}' is not a valid integer for pond height.");
            }

            if (Width < 0 || Height < 0)
            {
                throw new Exception("Both pond dimentions must be positive.");
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
                    throw new Exception($"'{str}' is not a valid direction.");
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
                    throw new Exception($"'{c}' is not a valid direction.");
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
                throw new Exception($"Input file '{inputFile}' is not properly formatted.");
            }

            return inputCommands;
        }

        private void IsInitialized()
        {
            if (!Initialized)
            {
                throw new Exception("Pond has not been initialized.");
            }
        }

        public void PrintDuckInfo()
        {
            IsInitialized();

            foreach (var duck in DuctList)
            {
                Console.WriteLine($"{duck.Position.X} {duck.Position.Y} {duck.Direction}");
            }
        }

        public void MoveDucks()
        {
            IsInitialized();

            foreach (var duck in DuctList)
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

        private bool IsPostionEmpty(int x, int y) => !DuctList.Any(duck => duck.Position.X == x && duck.Position.X == y);

        private bool IsPositionInBounds(int x, int y) => x >= 0 && y >= 0 && x <= Width && y <= Height;
    }
}
