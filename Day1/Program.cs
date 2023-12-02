internal class Program
{
    private static void Main(string[] args)
    {
        int? sumPart1 = 0;
        int? sumPart2 = 0;

        if (File.Exists("input.txt"))
        {
            TextReader file = File.OpenText("input.txt");

            string? line;

            while ((line = file.ReadLine()) != null)
            {
                // Part 1
                int? firstNumberPart1 = null;
                int? lastNumberPart1 = null;

                foreach (char c in line)
                {
                    if (int.TryParse(c.ToString(), out int number))
                    {
                        firstNumberPart1 ??= number;
                        lastNumberPart1 = number;
                    }
                }
                sumPart1 += firstNumberPart1 * 10 + lastNumberPart1;

                // Part 2
                int? firstNumberIndexPart2 = null;
                int? lastNumberIndexPart2 = null;
                int? firstNumberPart2 = null;
                int? lastNumberPart2 = null;

                foreach (var item in line.Select((value, index) => new { index, value }))
                {
                    if (int.TryParse(item.value.ToString(), out int number))
                    {
                        firstNumberPart2 ??= number;
                        firstNumberIndexPart2 ??= item.index;
                        lastNumberPart2 = number;
                        lastNumberIndexPart2 = item.index;
                    }
                }

                var wordsFound = searchWords.FindAll(word => line.Contains(word, StringComparison.OrdinalIgnoreCase));
                foreach(string  word in wordsFound)
                {
                    int firstIndex = line.IndexOf(word);
                    int lastIndex = line.LastIndexOf(word);

                    if (firstIndex != -1 && firstIndex < firstNumberIndexPart2)
                    {
                        firstNumberIndexPart2 = firstIndex;
                        firstNumberPart2 = (int)Enum.Parse(typeof(numbersAsWords), word, true);
                    }
                    if(lastIndex != -1 && lastIndex > lastNumberIndexPart2)
                    {
                        lastNumberIndexPart2 = lastIndex;
                        lastNumberPart2 = (int)Enum.Parse(typeof(numbersAsWords), word, true);
                    }
                }
                //Console.WriteLine(line + " - " + firstNumberPart2 + " - " + lastNumberPart2);

                sumPart2 += firstNumberPart2 * 10 + lastNumberPart2;
            }
        }

        Console.WriteLine("Part 1 answer: " + sumPart1);

        Console.WriteLine("Part 2 answer: " + sumPart2);
    }

    enum numbersAsWords
    {
        zero, one, two, three, four, five, six, seven, eight, nine
    };

    public static List<String> searchWords = new List<String>()
    {
        "zero", 
        "one", 
        "two", 
        "three", 
        "four", 
        "five", 
        "six", 
        "seven", 
        "eight",
        "nine"
    };
}