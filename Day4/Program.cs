internal class Program
{
    private static void Main(string[] args)
    {
        int part1Answer = 0;
        int part2Answer = 0;

        if (File.Exists("input.txt"))
        {
            // parse input
            var file = File.ReadAllLines("input.txt");

            int[] GamesList = new int[file.Length];

            for(int lineNumber = 0; lineNumber < file.Length;lineNumber++)
            {
                GamesList[lineNumber]++;
                var line = file[lineNumber];

                (var winners, var numbers) = (line.Split(':')[1].Split('|')[0].Split(' ', StringSplitOptions.RemoveEmptyEntries), line.Split(':')[1].Split('|')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries));

                var numWinners = numbers.Select(num => winners.Any(winner => winner.Equals(num))).Where(x => x == true).Count();

                part1Answer += (int) Math.Pow(2, numWinners - 1);

                for (int i = 1; i <= numWinners; i++)
                {
                    GamesList[lineNumber + i] += GamesList[lineNumber];
                }
            }

            part2Answer = GamesList.Sum();
        }


        Console.WriteLine("Part 1 answer: " + part1Answer);

        Console.WriteLine("Part 2 answer: " + part2Answer);
    }
}