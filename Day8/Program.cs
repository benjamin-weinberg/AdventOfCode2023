using System;
using System.Reflection;

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

            var nodeList = new List<Node>();
            var steps = file[0];

            foreach (var line in file)
            {
                // Account for the first and second line of the file
                if (line.Equals(file[0])) continue;
                if (line.Equals(file[1])) continue;

                var newNode = new Node();

                newNode.NodeLoction = line.Split(" = ")[0].Trim();
                newNode.LeftNode = line.Split(" = ")[1].Split(',')[0].Replace("(", "").Trim();
                newNode.RightNode = line.Split(" = ")[1].Split(',')[1].Replace(")", "").Trim();

                nodeList.Add(newNode);
            }

            // Part 1
            var currentNode = nodeList.Where(x => x.NodeLoction == "AAA").First();
            var index = -1;
            var step = 0; // set to -1 so we can start at 0 in the while loop
            while (currentNode.NodeLoction != "ZZZ")
            {
                step++;
                index++;

                //reset the step if the current step is more than the length
                if (index >= steps.Length)
                    index = 0;

                currentNode = FindNextNode(steps[index], currentNode, nodeList);

            }

            part1Answer = step;


            // Part 2

            var currentNodes = nodeList.Where(x => x.NodeLoction.EndsWith('A')).ToList();
            int[] periods = new int[currentNodes.Count];
            int i = 0;
            foreach (Node node in currentNodes)
            {
                step = 0;
                index = -1;
                currentNode = node;
                while (!currentNode.NodeLoction.EndsWith('Z'))
                {
                    step++;
                    index++;

                    //reset the step if the current step is more than the length
                    if (index >= steps.Length)
                        index = 0;

                    currentNode = FindNextNode(steps[index], currentNode, nodeList);

                }

                periods[i] = step;
                i++;
            }

            part2Answer = FindLcm(periods);

        }


        Console.WriteLine("Part 1 answer: " + part1Answer);

        Console.WriteLine("Part 2 answer: " + part2Answer);
    }

    public class Node
    {
        public string NodeLoction { get; set; }
        public string LeftNode { get; set; }
        public string RightNode { get; set; }
    }

    public static Node FindNextNode(char instruction, Node currentNode, List<Node> nodeList)
    {
        switch (instruction)
        {
            case 'R':
                return nodeList.Where(x => x.NodeLoction == currentNode.RightNode).First();
            case 'L':
                return nodeList.Where(x => x.NodeLoction == currentNode.LeftNode).First();
            default:
                return new Node(); // THIS SHOULD NEVER HAPPEN
        }

    }

    static long Gcd(long a, long b)
    {
        while (b != 0)
        {
            long t = b;
            b = a % b;
            a = t;
        }
        return a;
    }

    static long Lcm(long a, long b)
    {
        return (a / Gcd(a, b)) * b;
    }

    static long FindLcm(int[] numbers)
    {
        long result = numbers[0];
        for (long i = 1; i < numbers.Length; i++)
        {
            result = Lcm(result, numbers[i]);
        }
        return result;
    }
}