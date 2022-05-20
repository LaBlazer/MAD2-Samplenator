using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Samplenator.Util;
using System.Text;

namespace Samplenator
{

    public partial class Form1 : Form
    {
        Graph originalGraph;
        Graph sampledGraph;
        Dictionary<string, double> settings;

        List<int> originalDeg;
        List<int> originalCumDeg;

        public class ControlWriter : TextWriter
        {
            private Control textbox;
            public ControlWriter(Control textbox)
            {
                this.textbox = textbox;
            }

            //public override void Write(char value)
            //{
            //    textbox.Text += value;
            //}

            public override void Write(string? value)
            {
                if(value?.Length > 0)
                    textbox.Text += value;
            }

            public override Encoding Encoding
            {
                get { return Encoding.ASCII; }
            }
        }

        public Form1()
        {
            InitializeComponent();

            comboMethod.DisplayMember = "Name";

            comboMethod.Items.Add(new RWSampler());
            comboMethod.Items.Add(new RWRSampler());
            comboMethod.Items.Add(new RWJSampler());
            comboMethod.Items.Add(new RWMHSampler());

            comboMethod.SelectedIndex = 0;

            var modelDistr = new PlotModel { Title = "Distribution" };

            modelDistr.Axes.Add(new LogarithmicAxis()
            {
                Unit = "Degree",
                MinorStep = 1,
                Minimum = 1,
                //Base = 2,
                Position = AxisPosition.Bottom,
                MinorTickSize = 0,
                MajorTickSize = 0
            });

            modelDistr.Axes.Add(new LogarithmicAxis()
            {
                Unit = "Nodes",
                AxislineStyle = LineStyle.None,
                Minimum = 1,
                //Base = 1d,
                Position = AxisPosition.Left,      
            });

            var modelCumDistr = new PlotModel { Title = "Cumulative Distribution" };

            modelCumDistr.Axes.Add(new LogarithmicAxis()
            {
                Unit = "Degree",
                MinorStep = 1,
                Minimum = 1,
                Position = AxisPosition.Bottom,
                MinorTickSize = 0,
                MajorTickSize = 0,
                
               
            });

            modelCumDistr.Axes.Add(new LinearAxis()
            {
                Unit = "Cumulative Nodes",
                AxislineStyle = LineStyle.None,
                Minimum = 1,
                //Base = 1d,
                Position = AxisPosition.Left
            });

            this.plotDgrDistr.Model = modelDistr;
            this.plotCumDgrDistr.Model = modelCumDistr;

            Console.SetOut(new ControlWriter(txtOutput));
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private StemSeries ListToSeries(List<int> data, string name = "Series")
        {
            StemSeries series = new StemSeries()
            {
                StrokeThickness = 1,
                MarkerSize = 4,
                MarkerType = MarkerType.Circle,
                Base = 1d,
                Title = name
            };

            for (int i = 1; i < data.Count; i++)
            {
                series.Points.Add(new DataPoint(i, data[i]));
            }

            return series;
        }

        private void DoStatics(Graph g, string name, bool original = false)
        {
            int len = original ? -1 : originalCumDeg.Count;
            
            var distr = g.DegreeDistribution();
            var cum = g.CumulativeDegreeDistribution(len);

            plotDgrDistr.Model.Series.Add(ListToSeries(distr, name));
            plotCumDgrDistr.Model.Series.Add(ListToSeries(cum, name));

            plotDgrDistr.Model.InvalidatePlot(true);
            plotCumDgrDistr.Model.InvalidatePlot(true);

            if (original)
            {
                originalDeg = distr;
                originalCumDeg = cum;
            }

            var comps = g.GetComponents();

            Print($"\r\nStatistics for graph {name}");
            Print($"Node count: {g.NodeCount}");
            Print($"Edge count: {g.EdgeCount}");
            Print($"Density: {g.Density}");
            Print($"Max degree: {g.MaxDegree()}");
            Print($"Average degree: {g.AverageDegree()}");
            Print($"Average clustering coefficient: {g.AverageClusteringCoefficient()}");

            Print($"Component amount: {comps.Count}");
            Print($"Component biggest size: {comps.Max(x => x.Count)}");

            if (!original)
            {
                Print($"Kolmogorov-Smirnov Test (D-value): {Statistics.CalculateDval(cum, originalCumDeg)}");
            }

            Print("");
        }

        private void Print(string text)
        {
            txtOutput.Text += text + "\r\n";
            txtOutput.SelectionStart = txtOutput.TextLength;
            //scroll to the caret
            txtOutput.ScrollToCaret();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var result = openFileDialog1.ShowDialog();

            if(result == DialogResult.OK)
            {
                txtFilename.Text = openFileDialog1.FileName;

                // load file
                originalGraph = Graph.LoadFromCSV(openFileDialog1.FileName);

                Print($"Loading file {openFileDialog1.FileName} ...");

                DoStatics(originalGraph, "Original", true);
            }
        }

        private void comboMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboMethod.SelectedItem != null) {
                settings = (comboMethod.SelectedItem as Sampler).GetSettings();
                propertyGrid.SelectedObject = new DictionaryPropertyGridAdapter(settings);
            }
        }

        private void btnSample_Click(object sender, EventArgs e)
        {
            if (comboMethod.SelectedItem != null && originalGraph != null)
            {
                sampledGraph = (comboMethod.SelectedItem as Sampler).SampleGraph(originalGraph, settings);

                var name = (comboMethod.SelectedItem as Sampler).Name;

                Print($"Sampling graph using {name}");

                DoStatics(sampledGraph, name);
            }
        }
    }
}