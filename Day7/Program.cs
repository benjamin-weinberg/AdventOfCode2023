using System;
using System.Runtime.CompilerServices;

internal class Program
{
    private static void Main(string[] args)
    {
        if (File.Exists("input.txt"))
        {
            // parse input
            var file = File.ReadAllLines("input.txt");

            var part1Answer = ParseGames(file).Order().Select((game, index) => (index + 1) * game.bet).Sum();

            var part2Answer = ParseGames(file, true).Order().Select((game, index) => (index + 1) * game.bet).Sum();

            Console.WriteLine("Part 1 answer: " + part1Answer);

            Console.WriteLine("Part 2 answer: " + part2Answer);

        }
    }

    public static List<Game> ParseGames(string[] input, bool jokers = false)
    {
        var games = new List<Game>();

        foreach(var line in input){
            var bet = int.Parse(line.Split(" ")[1]);
            var hand = line.Split(" ")[0];

            if (jokers)
            {
                var occuring = hand.GroupBy(c => c)
                                .ToDictionary(group => group.Key, group => group.Count());
                int score = hand.Aggregate(0, (acc, c) => (acc << 4) + (c == 'J' ? 0 : CardRankings.IndexOf(c) + 1));
                int jokerCount = 0;
                if (occuring.ContainsKey('J'))
                {
                    jokerCount = occuring['J'];
                    occuring.Remove('J');
                }

                if (occuring.Count == 0)
                {
                    // JJJJJ
                    games.Add(new Game(hand, score, bet, 5, 0));
                    continue;
                }

                var maxCount = occuring.Values.Max();
                var maxCountKey = occuring.FirstOrDefault(x => x.Value == maxCount).Key;

                if (jokerCount > 0)
                {
                    occuring[maxCountKey] += jokerCount;
                    maxCount = occuring[maxCountKey];
                }

                var occuring1 = (occuring.Count > 1 ? occuring.OrderByDescending(x => x.Value).Skip(1).First().Value : 0);

                games.Add(new Game(hand, score, bet, maxCount, occuring1));
                continue;
            }
            else
            {
                int score = hand.Aggregate(0, (acc, c) => (acc << 4) + CardRankings.IndexOf(c));
                var occuring = hand.GroupBy(c => c)
                                    .Select(group => group.Count())
                                    .OrderByDescending(count => count).ToList();
                var occuring1 = (occuring.Count > 1 ? occuring[1] : 0);

                games.Add(new Game(hand, score, bet, occuring[0], occuring1));
                continue;
            }
        }

        return games;
    }

    public const string CardRankings = "23456789TJQKA";

    public enum HandType
    {
        HighCard,
        Pair,
        TwoPair,
        ThreeOfAKind,
        FullHouse,
        FourOfAKind,    
        FiveOfAKind
    }   

    public record Game(string hand, int score, int bet, int occurring0, int occurring1) : IComparable<Game>
    {
        public HandType type =>
            occurring0 switch
            {
                5 => HandType.FiveOfAKind,
                4 => HandType.FourOfAKind,
                3 => occurring1 == 2 ? HandType.FullHouse : HandType.ThreeOfAKind,
                2 => occurring1 == 2 ? HandType.TwoPair : HandType.Pair,
                _ => HandType.HighCard,
            };
        public int CompareTo(Game other) => type == other.type ? score.CompareTo(other.score) : type.CompareTo(other.type);
    }
}