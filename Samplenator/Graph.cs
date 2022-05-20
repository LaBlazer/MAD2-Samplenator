using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samplenator
{
    internal class Graph
    {
        private Dictionary<int, List<int>> nodes;

        public Graph()
        {
            nodes = new Dictionary<int, List<int>>();
        }

        public bool HasNode(int node)
        {
            return nodes.ContainsKey(node);
        }

        public List<int> GetEdges(int node)
        {
            List<int>? n = null;

            if(!nodes.TryGetValue(node, out n))
            {
                n = new List<int>();
                nodes[node] = n;
            }

            return n;
        }

        public int NodeCount
        {
            get => nodes.Count;
        }

        public List<int> GetNodes()
        {
            return nodes.Keys.ToList();
        }

        public void AddEdge(int nodeA, int nodeB, bool undirected = true)
        {
            var edgesA = GetEdges(nodeA);
            var edgesB = GetEdges(nodeB);

            if (!edgesA.Contains(nodeB))
            {
                edgesA.Add(nodeB);
            }
            else
            {
                Console.WriteLine($"Edge {nodeA} -> {nodeB} already exists");
            }

            if (undirected && !edgesB.Contains(nodeA))
            {
                edgesB.Add(nodeA);
            }
            else
            {
                Console.WriteLine($"Edge {nodeB} -> {nodeA} already exists");
            }
        }

        public double AverageDegree()
        {
            int degree = 0;
            foreach(var n in nodes.Values)
            {
                degree += n.Count;
            }

            return degree / (double)nodes.Count;
        }

        public int MinDegree()
        {
            int degree = 999999;

            foreach (var n in nodes.Values)
            {
                if(n.Count < degree)
                    degree = n.Count;
            }

            return degree;
        }

        public int MaxDegree()
        {
            int degree = 0;

            foreach (var n in nodes.Values)
            {
                if (n.Count > degree)
                    degree = n.Count;
            }

            return degree;
        }

        public List<int> DegreeDistribution()
        {
            var d = new List<int>();

            int max = MaxDegree();

            for(int i = 0; i <= max; i++)
            {
                d.Add(0);
            }

            foreach (var n in nodes.Values)
            {
                d[n.Count] += 1;
            }

            return d;
        }

        public List<int> CumulativeDegreeDistribution()
        {
            var dd = DegreeDistribution();
            int cum = dd[0];

            for(int i = 1; i < dd.Count; i++)
            {
                cum += dd[i];
                dd[i] = cum;
            }

            return dd;
        }

        public static Graph LoadFromCSV(string filename)
        {
            Graph g = new Graph();

            using (var reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    if (line != null)
                    {
                        var values = line.Split(',');

                        if(values.Length > 2)
                        {
                            throw new Exception("HUH??????????????");
                        }

                        int a = int.Parse(values[0]) - 1;
                        int b = int.Parse(values[1]) - 1;

                        // undirected
                        g.AddEdge(a, b);
                    }
                }
            }

            return g;

        }

    }
}
