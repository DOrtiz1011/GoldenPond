using System;

namespace GoldenPond
{
    internal static class Extensions
    {
        public static Direction ConvertStringToDirection(this string str)
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

        public static Motion ConvertCharToMotion(this char c)
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
    }
}
