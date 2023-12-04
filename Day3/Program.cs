internal class Program
{
    private static void Main(string[] args)
    {
        int part1Answer = 0;
        int part2Answer = 0;

        var numbers = new List<Number>();
        var symbols = new List<Symbol>();
        const char Void = '.';

        if (File.Exists("input.txt"))
        {
            // parse input
            var file = File.ReadAllLines("input.txt");

            for (var row = 0; row < file.Length; row++)
            {
                for (var col = 0; col < file[row].Length; col++)
                {
                    if (file[row][col] == Void) continue;

                    if (char.IsDigit(file[row][col]))
                    {
                        var currentNumber = new Number();
                        numbers.Add(currentNumber);
                        var numberValue = "";
                        numberValue += file[row][col];

                        if (numberValue.Length == 1)
                        {
                            currentNumber.Start = (row, col);
                        }

                        while (col < file[row].Length - 1 && char.IsDigit(file[row][col + 1]))
                        {
                            numberValue += file[row][col + 1];
                            col++;
                        }

                        currentNumber.End = (row, col);
                        currentNumber.Value = int.Parse(numberValue);
                    }
                    else
                    {
                        symbols.Add(new Symbol
                        {
                            Value = file[row][col],
                            Position = (row, col)
                        });
                    }
                }
            }
        }

        // use linq to get answers
        part1Answer = numbers
            .Where(number => symbols.Any(symbol => AreAdjacent(number, symbol)))
            .Sum(number => number.Value);

        part2Answer = symbols
            .Where(symbol => symbol.Value == '*')
            .Select(symbol => numbers.Where(number => AreAdjacent(number, symbol)).ToArray())
            .Where(gears => gears.Length == 2)
            .Sum(gears => gears[0].Value * gears[1].Value);

        Console.WriteLine("Part 1 answer: " + part1Answer);

        Console.WriteLine("Part 2 answer: " + part2Answer);
    }

    static bool AreAdjacent(Number number, Symbol symbol)
    {
        // a symbol must be within the start and end rows (+-1) to be 'adjacent'
        return (symbol.Position.Row >= number.Start.Row - 1 && symbol.Position.Row <= number.Start.Row + 1 )
               && 
               (symbol.Position.Column >= number.Start.Column - 1 && symbol.Position.Column <= number.End.Column + 1);
    }

    class Number
    {
        public int Value { get; set; }
        public (int Row, int Column) Start { get; set; }
        public (int Row, int Column) End { get; set; }
    }

    class Symbol
    {
        public char Value { get; set; }
        public (int Row, int Column) Position { get; set; }
    }
}