namespace GoldenPond
{
    class Duck
    {
        private int X { get; set; }
        private int Y { get; set; }
        private Direction DuckDirection { get; set; }
        private const char Port = 'P';
        private const char Starboad = 'S';
        private const char Forward = 'F';

        public Duck (int x, int y, Direction direction)
        {
            X = x;
            Y = y;
            DuckDirection = direction;
        }

        // verify input string

        public void ReadInput(string input)
        {
            foreach (var command in input)
            {
                ExecuteCommand(command);
            }
        }

        private void ExecuteCommand(char command)
        {
            switch (command)
            {
                case Port:
                case Starboad:
                    Rotate(command);
                    break;
                case Forward:
                    Move(command);
                    break;
            }
        }

        private void Rotate(char command)
        {
            switch (command)
            {
                case Port:
                    switch (DuckDirection)
                    {
                        case Direction.North:
                            DuckDirection = Direction.West;
                            break;
                        case Direction.South:
                            DuckDirection = Direction.East;
                            break;
                        case Direction.East:
                            DuckDirection = Direction.North;
                            break;
                        case Direction.West:
                            DuckDirection = Direction.South;
                            break;
                    }

                    break;

                case Starboad:
                    switch (DuckDirection)
                    {
                        case Direction.North:
                            DuckDirection = Direction.East;
                            break;
                        case Direction.South:
                            DuckDirection = Direction.West;
                            break;
                        case Direction.East:
                            DuckDirection = Direction.South;
                            break;
                        case Direction.West:
                            DuckDirection = Direction.North;
                            break;
                    }

                    break;
            }
        }

        private void Move(char command)
        {

        }
    }
}
