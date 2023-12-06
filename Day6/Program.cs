using System;

internal class Program
{
    private static void Main(string[] args)
    {
        uint part1Answer = 1;
        uint part2Answer = 0;


        if (File.Exists("input.txt"))
        {
            // parse input
            var file = File.ReadAllLines("input.txt");

            // Part 1
            var timesPart1 = Array.ConvertAll<string, uint>(file[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).Skip(1).ToArray(), uint.Parse);
            var distancesPart1 = Array.ConvertAll<string, uint>(file[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Skip(1).ToArray(), uint.Parse);

            for (int i = 0; i < timesPart1.Count(); i++)
            {
                uint waysToBeat = 0;
                var raceTime = timesPart1[i];
                var distanceToBeat = distancesPart1[i];

                for (uint speed = 0; speed < raceTime; speed++)
                {
                    if (speed * (raceTime - speed) > distanceToBeat) waysToBeat++;
                }

                part1Answer *= waysToBeat;
            }

            // Part 2
            var timeInRace = double.Parse(file[0].Split(":", StringSplitOptions.RemoveEmptyEntries).Last().Replace(" ", ""));
            var distanceInRace = double.Parse(file[1].Split(":", StringSplitOptions.RemoveEmptyEntries).Last().Replace(" ", ""));

            for (double speed = 0; speed < timeInRace; speed++)
            {
                if (speed * (timeInRace - speed) > distanceInRace) part2Answer++;
            }
        }


        Console.WriteLine("Part 1 answer: " + part1Answer);

        Console.WriteLine("Part 2 answer: " + part2Answer);
    }
}