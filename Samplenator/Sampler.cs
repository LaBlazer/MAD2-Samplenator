using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samplenator
{
    abstract class Sampler
    {
        public Graph SampleGraph(Graph g, Dictionary<string, double> settings)
        {
            if(!CheckSettings(settings))
            {
                throw new ArgumentException("Invalid settings");
            }

            Graph sampled = new Graph();

            DoSampling(g, sampled, settings);

            return sampled;
        }

        private bool CheckSettings(Dictionary<string, double> settings)
        {
            var s = GetSettings();

            return s.Count == settings.Count && s.Keys.All(settings.ContainsKey);
        }


        public abstract string Name { get; }

        public abstract Dictionary<string, double> GetSettings();


        protected abstract void DoSampling(Graph original, Graph sampled, Dictionary<string, double> settings);
    }

    class RWSampler : Sampler
    {
        readonly internal Random rnd = new Random();

        public override string Name => "Random Walk";

        public override Dictionary<string, double> GetSettings()
        {
            return new Dictionary<string, double>()
            {
                ["size"] = 0.1
            };
        }

        internal T RandomElement<T>(List<T> list)
        {
            return list[rnd.Next(list.Count)];
        }

        protected override void DoSampling(Graph original, Graph sampled, Dictionary<string, double> settings)
        {
            var nodes = original.GetNodes();

            int size = (int)(settings["size"] * nodes.Count);

            int currentNode = RandomElement(nodes);
            int nextNode, k = 0;

            while (sampled.NodeCount < size)
            {
                nextNode = RandomElement(original.GetEdges(currentNode));

                sampled.AddEdge(currentNode, nextNode);

                currentNode = nextNode;

                k++;
                if(k > (100 * original.NodeCount))
                {
                    //Console.WriteLine("Resetting walker");
                    currentNode = RandomElement(nodes);
                    k = 0;
                }
            }

        }
    }

    class RWRSampler : RWSampler
    {
        public override string Name => "Random Walk with Restart";

        public override Dictionary<string, double> GetSettings()
        {
            return new Dictionary<string, double>()
            {
                ["probability"] = 0.15,
                ["size"] = 0.1
            };
        }

        protected override void DoSampling(Graph original, Graph sampled, Dictionary<string, double> settings)
        {
            var nodes = original.GetNodes();

            int size = (int)(settings["size"] * nodes.Count);
            double p = settings["probability"];


            int currentNode = RandomElement(nodes);
            int startNode = currentNode;
            int nextNode, k = 0;

            while (sampled.NodeCount < size)
            {
                nextNode = RandomElement(original.GetEdges(currentNode));

                sampled.AddEdge(currentNode, nextNode);

                currentNode = nextNode;

                if (rnd.NextDouble() < p)
                {
                    //Console.WriteLine("Resetting walker");
                    currentNode = startNode;
                }

                k++;
                if (k > (1000 * original.NodeCount))
                {
                    Console.WriteLine("CAN'T DO");
                    return;
                }
            }
        }
    }

    class RWJSampler : RWSampler
    {
        public override string Name => "Random Walk with Jump";

        public override Dictionary<string, double> GetSettings()
        {
            return new Dictionary<string, double>()
            {
                ["probability"] = 0.05,
                ["size"] = 0.1
            };
        }

        protected override void DoSampling(Graph original, Graph sampled, Dictionary<string, double> settings)
        {
            var nodes = original.GetNodes();

            int size = (int)(settings["size"] * nodes.Count);
            double p = settings["probability"];


            int currentNode = RandomElement(nodes);
            int nextNode, k = 0;

            while (sampled.NodeCount < size)
            {
                nextNode = RandomElement(original.GetEdges(currentNode));

                sampled.AddEdge(currentNode, nextNode);

                currentNode = nextNode;

                if (rnd.NextDouble() < p)
                {
                    //Console.WriteLine("Resetting walker");
                    currentNode = RandomElement(nodes);
                }

                k++;
                if (k > (100 * original.NodeCount))
                {
                    Console.WriteLine("CAN'T DO");
                    return;
                }
            }
        }
    }

    class RWMHSampler : RWSampler
    {
        public override string Name => "Metropolis-Hastings Random Walk";

        public override Dictionary<string, double> GetSettings()
        {
            return new Dictionary<string, double>()
            {
                ["size"] = 0.1
            };
        }

        protected override void DoSampling(Graph original, Graph sampled, Dictionary<string, double> settings)
        {
            var nodes = original.GetNodes();

            int size = (int)(settings["size"] * nodes.Count);

            int currentNode = RandomElement(nodes);
            int nextNode, k = 0;

            List<int> currentNodeEdges = original.GetEdges(currentNode);

            while (sampled.NodeCount < size)
            {
                nextNode = RandomElement(currentNodeEdges);

                // p ≤ d(v)/d(u)
                var r = (currentNodeEdges.Count / (double)original.GetEdges(nextNode).Count);
                if (rnd.NextDouble() > (currentNodeEdges.Count / (double)original.GetEdges(nextNode).Count))
                    continue;

                sampled.AddEdge(currentNode, nextNode);

                currentNode = nextNode;
                currentNodeEdges = original.GetEdges(currentNode);

                k++;
                if (k > (100 * original.NodeCount))
                {
                    Console.WriteLine("CAN'T DO");
                    return;
                }
            }
        }
    }
}
