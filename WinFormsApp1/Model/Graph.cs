using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.Model
{
    public class Node
    {
        public (int, int) Addr { get; set; }
        public int Number { get; set; }
        public Color Color { get; set; }
        public Node(int row, int column, int number)
        {
            Addr = (row, column);
            Number = number;
            Color = Color.White;
        }
    }
    public class Graph
    {
        public List<Node> Nodes { get; set; }
        public Dictionary<Node, List<Node>> AdjacencyList { get; set; }
        public static List<Color> colors = new List<Color>
        {
            Color.Red,
            Color.Blue,
            Color.Green,
            Color.Yellow,
            Color.Orange,
            Color.Purple,
            Color.Brown,
            Color.Violet,
            Color.Gray
        };
        public int loopCount;
        public int recursionCount;
        public Graph(int[,] sudokuGrid)
        {
            Nodes = new List<Node>();
            AdjacencyList = new Dictionary<Node, List<Node>>();

            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    var node = new Node(row, col, sudokuGrid[row, col]);
                    Nodes.Add(node);
                    AdjacencyList[node] = new List<Node>();
                }
            }
            foreach (var node in Nodes)
            {
                AddNeighbors(node);
            }
        }

        private void AddNeighbors(Node node)
        {
            int row = node.Addr.Item1;
            int col = node.Addr.Item2;

            foreach (var other in Nodes)
            {
                if (other != node)
                {
                    if (other.Addr.Item1 == row || other.Addr.Item2 == col)
                    {
                        AdjacencyList[node].Add(other);
                    }

                    if ((other.Addr.Item1 / 3 == row / 3) && (other.Addr.Item2 / 3 == col / 3))
                    {
                        AdjacencyList[node].Add(other);
                    }
                }
            }
        }
        public bool IsSafe(Node node, Color color)
        {
            foreach (var neighbor in AdjacencyList[node])
            {
                if (neighbor.Color == color)
                    return false;
            }
            return true;
        }
        public List<Color> ColorUnUsed(Node node)
        {
            List<Color> availableColors = new List<Color>(colors);

            foreach (var neighbor in AdjacencyList[node])
            {
                if (neighbor.Color != Color.White)
                {
                    availableColors.Remove(neighbor.Color);
                }
            }

            return availableColors;
        }

        public bool BacktrackingColoringUtil(int index)
        {
            recursionCount++;
            if (index >= Nodes.Count)
                return true;

            var node = Nodes[index];

            if (node.Number != 0)
            {
                return BacktrackingColoringUtil(index + 1);
            }

            foreach (var color in ColorUnUsed(node))
            {
                loopCount++;
                node.Color = color;
                node.Number = colors.IndexOf(color) + 1;
                if (BacktrackingColoringUtil(index + 1))
                    return true;
                node.Color = Color.White;
                node.Number = 0;
            }
            return false;
        }

        public bool BacktrackingColoring()
        {
            foreach(var node in Nodes)
            {
                if(node.Number != 0)
                node.Color = colors[node.Number-1];
            }
            bool result = BacktrackingColoringUtil(0);

            return result;
        }


    }
}
