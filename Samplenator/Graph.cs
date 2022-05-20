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

        public int EdgeCount
        {
            get => nodes.Sum(x => x.Value.Count) / 2;
        }

        public double Density
        {
            get => EdgeCount / ((NodeCount * (NodeCount - 1)) / 2d);
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
                //Console.WriteLine($"Edge {nodeA} -> {nodeB} already exists");
            }

            if (undirected && !edgesB.Contains(nodeA))
            {
                edgesB.Add(nodeA);
            }
            else
            {
                //Console.WriteLine($"Edge {nodeB} -> {nodeA} already exists");
            }
        }


        private double ClusteringCoefficient(int node, List<int> edges)
        {
            int links = 0;
            for(int i = 0; i < edges.Count; i++)
            {
                for(int j = 0; j < i; j++)
                {
                    if (nodes[edges[i]].Contains(edges[j]))
                        links++;
                }
            }

            return edges.Count > 1 ? (2 * links / (edges.Count * (edges.Count - 1))) : 0;
        }

        public double AverageClusteringCoefficient()
        {
            return nodes.Average(x => ClusteringCoefficient(x.Key, x.Value));
        }

        public List<List<int>> GetComponents()
        {
            var components = new List<List<int>>();

            var nodeList = nodes.Keys.ToList();

            while (nodeList.Count > 0)
            {
                List<int> component = new List<int> { nodeList[0] };
                int k = 0, nodeId;

                while(k < component.Count)
                {
                    nodeId = component[k];
                    nodeList.Remove(nodeId);

                    foreach(int neighbour in nodes[nodeId])
                    {
                        if(!component.Contains(neighbour))
                            component.Add(neighbour);
                    }

                    k++;
                }

                components.Add(component);
            }

            return components;
        }

        public double AverageDegree()
        {
            return nodes.Average(x => x.Value.Count);
        }

        public int MinDegree()
        {
            return nodes.Min(x => x.Value.Count);
        }

        public int MaxDegree()
        {
            return nodes.Max(x => x.Value.Count);
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

        public List<int> CumulativeDegreeDistribution(int length = -1)
        {
            var dd = DegreeDistribution();
            int cum = dd[0];

            for(int i = 1; i < dd.Count; i++)
            {
                cum += dd[i];
                dd[i] = cum;
            }

            if(length > 0 && dd.Count < length)
            {
                for (int i = dd.Count; i < length; i++)
                {
                    dd.Add(cum);
                }
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
