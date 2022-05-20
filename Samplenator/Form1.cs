using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Samplenator.Util;

namespace Samplenator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            comboMethod.DisplayMember = "Name";

            comboMethod.Items.Add(new RWSampler());
            comboMethod.Items.Add(new RWRSampler());
            comboMethod.Items.Add(new RWJSampler());
            comboMethod.Items.Add(new RWMHSampler());

            comboMethod.SelectedIndex = 0;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private LinearBarSeries ListToSeries(List<int> data)
        {
            LinearBarSeries series = new LinearBarSeries();

            for (int i = 0; i < data.Count; i++)
            {
                series.Points.Add(new DataPoint(i, data[i]));
            }

            return series;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var result = openFileDialog1.ShowDialog();

            if(result == DialogResult.OK)
            {
                txtFilename.Text = openFileDialog1.FileName;

                // load file
                Graph g = Graph.LoadFromCSV(openFileDialog1.FileName);

                MessageBox.Show(g.AverageDegree().ToString());

                var distr = g.DegreeDistribution();
                var cum = g.CumulativeDegreeDistribution();

                var aaa = new PlotModel { Title = "Distr" };
                //aaa.Axes.Add(new LogarithmicAxis { Position = AxisPosition.Left });
                
                
                aaa.Series.Add(ListToSeries(distr));
                this.plotDgrDistr.Model = aaa;

                var bbb = new PlotModel { Title = "Cum Distr" };
                bbb.Series.Add(ListToSeries(cum));
                this.plotCumDgrDistr.Model = bbb;
            }
        }

        private void comboMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboMethod.SelectedItem != null) {
                var set = (comboMethod.SelectedItem as Sampler).GetSettings();
                propertyGrid.SelectedObject = new DictionaryPropertyGridAdapter(set);
            }
        }
    }
}