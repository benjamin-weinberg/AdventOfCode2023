using System.Drawing;

namespace Day10
{
    internal class Program
    {
        static void Main(string[] args)
        {

            if (File.Exists("input.txt"))
            {
                var part1Answer = 0;
                var part2Answer = 0;

                // parse input
                var file = File.ReadAllLines("input.txt").Select(x => new List<char>(x.ToArray())).ToList();

                (int xs, int ys) = FindStart(file);

                Direction? initialDirection = FindEntry(xs, ys, file);

                switch (initialDirection)
                {
                    case Direction.North:
                        ys--;
                        break;

                    case Direction.South:
                        ys++;
                        break;

                    case Direction.East:
                        xs--;
                        break;

                    case Direction.West:
                        xs++;
                        break;
                }

                Direction? currentDirection = FindExit(file[ys][xs], initialDirection);
                var steps = 1;

                while (currentDirection != null)
                {
                    steps++;

                    switch (currentDirection)
                    {
                        case Direction.North:
                            ys--;
                            currentDirection = Direction.South;
                            break;

                        case Direction.South:
                            ys++;
                            currentDirection = Direction.North;
                            break;

                        case Direction.East:
                            xs++;
                            currentDirection = Direction.West;
                            break;

                        case Direction.West:
                            xs--;
                            currentDirection = Direction.East;
                            break;
                    }

                    currentDirection = FindExit(file[ys][xs], currentDirection);
                }


                part1Answer = steps / 2;



                Console.WriteLine("Part 1 answer: " + part1Answer);
                Console.WriteLine("Part 2 answer: " + part2Answer);

            }
        }

        public static Direction? FindExit(char currentPipe, Direction? entryDirection)
        {
            switch (currentPipe, entryDirection)
            {
                case ('|', Direction.North):
                    return Direction.South;
                case ('|', Direction.South):
                    return Direction.North;
                case ('-', Direction.East):
                    return Direction.West;
                case ('-', Direction.West):
                    return Direction.East;
                case ('L', Direction.North):
                    return Direction.East;
                case ('L', Direction.East):
                    return Direction.North;
                case ('J', Direction.West):
                    return Direction.North;
                case ('J', Direction.North):
                    return Direction.West;
                case ('7', Direction.West):
                    return Direction.South;
                case ('7', Direction.South):
                    return Direction.West;
                case ('F', Direction.East):
                    return Direction.South;
                case ('F', Direction.South):
                    return Direction.East;
                case ('S', _):
                    return null;
                default: // SHOULD NEVER HIT THIS 
                    return null;
            }
        }

        public static (int, int) FindStart(List<List<char>> file)
        {
            for (int i = 0; i < file.Count; i++)
            {
                for (int j = 0; j < file[i].Count; j++)
                {
                    if (file[i][j] == 'S')
                        return (j, i);
                }
            }
            return (0, 0);
        }

        public static Direction FindEntry(int x, int y, List<List<char>> file)
        {
            Direction? output = FindExit(file[y - 1][x], Direction.South);
            if (output != null) return Direction.South;

            output = FindExit(file[y][x + 1], Direction.West);
            if (output != null) return Direction.West;

            output = FindExit(file[y + 1][x], Direction.North);
            if (output != null) return Direction.North;

            // IN THEORY, THIS SHOULDNT HAPPEN BECAUSE THERE SHOULD BE ANOTHER PIPE ADJACENT (1 IN and 1 OUT)
            return Direction.East;


        }

        public enum Direction
        {
            North, South, East, West
        }
    }
}
