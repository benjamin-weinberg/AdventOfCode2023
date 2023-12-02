internal class Program
{
    private static void Main(string[] args)
    {
        var sumOfGameNumbers = 0;
        var sumOfPowerSets = 0;

        if (File.Exists("input.txt"))
        {
            TextReader file = File.OpenText("input.txt");

            string? game;

            while ((game = file.ReadLine()) != null)
            {
                var gameDraws = game.Split(": ")[1].Trim();

                // Part 1

                var gamenumber = int.Parse(game.Split(": ")[0].Trim().Remove(0, 5));

                var validGame = true;
                var draws = gameDraws.Split(";");

                foreach ( var draw in draws)
                {
                    var arr1 = gameDraws.Split(" ");

                    var validDraw = true;
                    for (int i = 0; i < arr1.Length; i += 2)
                    {
                        if (arr1[i + 1].Contains("red") && int.Parse(arr1[i]) > 12)
                        { 
                            validDraw = false;
                            break;
                        }
                        else if (arr1[i + 1].Contains("green") && int.Parse(arr1[i]) > 13)
                        {
                            validDraw = false;
                            break;
                        }
                        else if (arr1[i + 1].Contains("blue") && int.Parse(arr1[i]) > 14)
                        {
                            validDraw = false;
                            break;
                        }
                    }

                    if (!validDraw)
                    {
                        validGame = false;
                        break;
                    }
                }

                if (validGame)
                {
                    sumOfGameNumbers += gamenumber;
                }


                // Part 2                
                var red = 0;
                var green = 0;
                var blue = 0;
                
                var arr2 = gameDraws.Split(" ");

                for (int i = 0; i < arr2.Length; i += 2)
                {
                    if (arr2[i + 1].Contains("red"))
                    {
                        var redTemp = int.Parse(arr2[i]);
                        if (redTemp > red) red = redTemp;
                    }
                    else if (arr2[i + 1].Contains("green"))
                    {
                        var greenTemp = int.Parse(arr2[i]);
                        if (greenTemp > green) green = greenTemp;
                    }
                    else if (arr2[i + 1].Contains("blue"))
                    {
                        var blueTemp = int.Parse(arr2[i]);
                        if (blueTemp > blue) blue = blueTemp;
                    }
                }

                sumOfPowerSets += red * green * blue;
            }
        }

        Console.WriteLine("Part 1 answer: " + sumOfGameNumbers);

        Console.WriteLine("Part 2 answer: " + sumOfPowerSets);
    }

    enum Colors
    {
        red,
        green,
        blue
    }

    static readonly List<string> ColorStrings = ["red", "green", "blue"];
}