using System;
using System.Collections.Generic;
using System.Linq;

namespace GoldenPond
{
    internal sealed class Pond
    {
        public int Height { get; set; }
        public int Width { get; set; }

        public readonly List<Duck> DuctDataList = new List<Duck>();

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

            foreach (var duckData in DuctDataList)
            {
                foreach (var motion in duckData.Commands)
                {
                    ExecuteCommand(duckData, motion);
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
                default:
                    throw new ArgumentException($"Command '{motion}' is invalid.", nameof(motion));
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

        private bool IsMoveValid(int x, int y) => IsPostionEmpty(x, y) && IsPositionInBounds(x, y);

        private bool IsPostionEmpty(int x, int y) => !DuctDataList.Any(d => d.Position.X == x && d.Position.X == y);

        private bool IsPositionInBounds(int x, int y) => x >= 0 && y >= 0 && x <= Width && y <= Height;
    }
}
