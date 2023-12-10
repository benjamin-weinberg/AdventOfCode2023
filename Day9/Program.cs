namespace Day9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int part1Answer = 0;
            int part2Answer = 0;


            if (File.Exists("input.txt"))
            {
                // parse input
                var file = File.ReadAllLines("input.txt");


                foreach (var line in file)
                {
                    // Parse File
                    List<List<int>> extrapolations = new List<List<int>>();

                    var lastList = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
                    extrapolations.Add(lastList);

                    while (!extrapolations.Last().All(x => x == 0))
                    {
                        lastList = extrapolations.Last();
                        var nextList = new List<int>();

                        for (int i = 0; i < extrapolations.Last().Count - 1; i++)
                        {
                            nextList.Add((lastList[i+1] - lastList[i]));
                        }

                        extrapolations.Add(nextList);
                    }

                    // Part 1
                    part1Answer += extrapolations.Sum(x => x.LastOrDefault());

                    // Part 2
                    var newValue = 0;

                    for (int i = extrapolations.Count - 1; i >= 0; i--)
                        newValue = extrapolations[i].FirstOrDefault() - newValue;

                    part2Answer += newValue;
                }
            }

            Console.WriteLine("Part 1 answer: " + part1Answer);

            Console.WriteLine("Part 2 answer: " + part2Answer);
        }
    }
}
