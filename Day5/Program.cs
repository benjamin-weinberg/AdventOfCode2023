using System;

internal class Program
{
    private static void Main(string[] args)
    {
        uint part1Answer = 0;
        uint part2Answer = 0;

        List<AlmanacEntry> EntriesForSeeds1 = new List<AlmanacEntry>();
        uint? minLocation = null;

        if (File.Exists("input.txt"))
        {
            // parse input
            var file = File.ReadAllText("input.txt");

            var alamanacMappings = file.Split("\n\n");

            var seeds = alamanacMappings[0].Split(" ");
            var seedsToSoil = alamanacMappings[1].Split("\n");
            var soilToFertilizer = alamanacMappings[2].Split("\n");
            var fertilizerToWater = alamanacMappings[3].Split("\n");
            var waterToLight = alamanacMappings[4].Split("\n");
            var lightToTemp = alamanacMappings[5].Split("\n");
            var tempToHumidity = alamanacMappings[6].Split("\n");
            var humidityToLocation = alamanacMappings[7].Split("\n");

            // Part 1
            foreach (var seed in seeds)
            {
                if (uint.TryParse(seed, out uint seedNum))
                {
                    var seedEntry = new AlmanacEntry();
                    seedEntry.Seed = seedNum;
                    seedEntry.Soil = findEntry(seedNum, seedsToSoil);
                    seedEntry.Fertilizer = findEntry(seedEntry.Soil, soilToFertilizer);
                    seedEntry.Water = findEntry(seedEntry.Fertilizer, fertilizerToWater);
                    seedEntry.Light = findEntry(seedEntry.Water, waterToLight);
                    seedEntry.Tempature = findEntry(seedEntry.Light, lightToTemp);
                    seedEntry.Humidity = findEntry(seedEntry.Tempature, tempToHumidity);
                    seedEntry.Location = findEntry(seedEntry.Humidity, humidityToLocation);

                    EntriesForSeeds1.Add(seedEntry);
                }

            }


            // Part 2 **VERY INEFFICIENT**
            var seedRanges = new List<List<uint>>();
            for (int i = 1; i < seeds.Length; i += 2)
            {
                uint.TryParse(seeds[i], out uint startSeedNum);
                uint.TryParse(seeds[i + 1], out uint rangeSeedNum);
                seedRanges.Add(new List<uint>() { startSeedNum, startSeedNum + rangeSeedNum });
            }

            object lockMinLocation = new();
            Parallel.ForEach(seedRanges, x =>
            {
                for (uint currentSeedNum = x[0]; currentSeedNum <= x[0] + x[1]; currentSeedNum++)
                {
                    var currentLocation = findEntry(findEntry(findEntry(findEntry(findEntry(findEntry(findEntry(currentSeedNum, seedsToSoil), soilToFertilizer), fertilizerToWater), waterToLight), lightToTemp), tempToHumidity), humidityToLocation);

                    lock (lockMinLocation)
                    {
                        if (minLocation == null || currentLocation < minLocation)
                        {
                            minLocation = currentLocation;
                            part2Answer = currentSeedNum;
                        }
                    }
                }

                Console.WriteLine($"Range Complete! Range start {x[0]} range end {x[1]}");
            });

        }

        part1Answer = EntriesForSeeds1.Min(e => e.Location);


        Console.WriteLine("Part 1 answer: " + part1Answer);

        Console.WriteLine("Part 2 answer: " + part2Answer);
    }

    public static uint findEntry(uint item, string[] entriesToSearch)
    {
        //convert entry list to list of entries as a 2d array of entry[int]
        var entries = entriesToSearch.Skip(1).Select(x => Array.ConvertAll<string, uint>(x.Split(" "), uint.Parse)).Where(x => x[1] <= item && item <= x[1] + x[2]).FirstOrDefault();

        if (entries is not null && entries.Any())
        {
            return (item - entries[1]) + entries[0];
        }
        else return item;
    }

    class AlmanacEntry
    {
        public uint Seed { get; set; }
        public uint Soil { get; set; }
        public uint Fertilizer { get; set; }
        public uint Water { get; set; }
        public uint Light { get; set; }
        public uint Tempature { get; set; }
        public uint Humidity { get; set; }
        public uint Location { get; set; }
    }
}