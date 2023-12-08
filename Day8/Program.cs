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

            var currentNodes = nodeList.Where(x => x.NodeLoction.EndsWith('A')).Select(x => (node: x, period: 0)).ToList() ;
            foreach ((Node node, int p) in currentNodes)
            {
                step = 0;
                index = -1;
                currentNode = node;
                while (currentNode.NodeLoction.EndsWith("Z"))
                {
                    step++;
                    index++;

                    //reset the step if the current step is more than the length
                    if (index >= steps.Length)
                        index = 0;

                    currentNode = FindNextNode(steps[index], currentNode, nodeList);

                }

                p = step;
            }

            //while (!currentNodes.All(x => x.NodeLoction.EndsWith('Z')))
            //{
            //    step++;
            //    index++;

            //    //reset the step if the current step is more than the length
            //    if (index >= steps.Length)
            //        index = 0;

            //    var newNodes = new List<Node>();
            //    foreach (Node node in currentNodes)
            //    {
            //       newNodes.Add(FindNextNode(steps[index], node, nodeList));
            //    }

            //    currentNodes = newNodes;
            //}

            part2Answer = step;
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

    static int gcd(int n1, int n2)
    {
        if (n2 == 0)
        {
            return n1;
        }
        else
        {
            return gcd(n2, n1 % n2);
        }
    }

    static int lcm(int[] numbers)
    {
        return numbers.Aggregate((S, val) => S * val / gcd(S, val));
    }
}