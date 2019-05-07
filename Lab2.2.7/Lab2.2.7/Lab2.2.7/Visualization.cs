using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2._2._7
{
    public partial class Visualization : Form
    {
        public Visualization()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        public Random random = new Random();
        public List<int> DATA = new List<int>();
        /// <summary>
        /// Generate and upload information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Visualization_Activated(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            foreach (var s in chart1.Series)
            {
                s.Points.Clear();
            }
            Stopwatch sw = new Stopwatch();
            for (int i=0;i< random.Next(7, 10); i++) {
                for (int c = 0; c < 1000*(i+1); c++)
                {
                    int val = random.Next(-1000, 1000);
                    DATA.Add(val);
                }
                double c1, c2, l1, l2, timeSpan;
                sw.Reset();
                dataGridView1.Rows.Add();
                sw.Start();
                c1 = VarianzaCS1(DATA);
                sw.Stop();
                timeSpan = sw.ElapsedMilliseconds;
                dataGridView1[0, i*4].Value ="C#";
                dataGridView1[1, i*4].Value =c1;
                dataGridView1[2, i*4].Value = "дисперсии";
                dataGridView1[3, i*4].Value =timeSpan;
                chart1.Series[0].Points.AddXY(1000*(i+1), timeSpan);

                sw.Reset();
                dataGridView1.Rows.Add();
                sw.Start();
                c2 = VarianzaCS2(DATA);
                sw.Stop();
                timeSpan = sw.ElapsedMilliseconds;
                dataGridView1[0, i * 4+1].Value = "C# 2";
                dataGridView1[1, i * 4+1].Value = c2;
                dataGridView1[2, i * 4+1].Value = "дисперсии";
                dataGridView1[3, i * 4+1].Value = timeSpan;
                chart1.Series[1].Points.AddXY(1000 * (i + 1), timeSpan);

                sw.Reset();               
                dataGridView1.Rows.Add();
                sw.Start();
                l1 = VarianzaLINQ(DATA);
                sw.Stop();
                timeSpan = sw.ElapsedMilliseconds;
                dataGridView1[0, i * 4+2].Value = "LINQ";
                dataGridView1[1, i * 4+2].Value = l1;
                dataGridView1[2, i * 4+2].Value = "дисперсии";
                dataGridView1[3, i * 4+2].Value = timeSpan;
                chart1.Series[2].Points.AddXY(1000 * (i + 1), timeSpan);


                sw.Reset();
                dataGridView1.Rows.Add();
                sw.Start();
                l2 = VarianzaPLINQ(DATA);
                sw.Stop();
                timeSpan = sw.ElapsedMilliseconds;
                dataGridView1[0, i * 4+3].Value = "PLINQ";
                dataGridView1[1, i * 4+3].Value = l2;
                dataGridView1[2, i * 4+3].Value = "дисперсии";
                dataGridView1[3, i * 4+3].Value = timeSpan;
                chart1.Series[3].Points.AddXY(1000 * (i + 1), timeSpan);

            }
        }
        /////Methodos to analize
        public double VarianzaCS1(IEnumerable<int> values)
        {
            double ret = 0;
            for (int c=0;c<values.Count();c++)
            {
                ret = ret + Math.Pow((values.ElementAt(c)-values.Average()),2);
            }
            ret = ret / (values.Count()-1);
            return ret;
        }
        public double VarianzaCS2(IEnumerable<int> values)
        {
            double ret = 0;
            double Sr = 0;
            int[] arr = values.ToArray<int>();
            for (int i=0;i<arr.Length;i++)
            {
                Sr += arr[i];
            }
            Sr = Sr / arr.Length;
            for (int c=0;c<arr.Length;c++)
            {
                ret = ret + Math.Pow((arr[c]-Sr),2);
            }
            ret = ret / (arr.Length-1);
            return ret;
        }
        public double VarianzaPLINQ(IEnumerable<int> values)
        {
            double ret = 0;
            int count = values.Count();
            if (count > 1)
            {
                double avg = values.Average();
                double sum = values.AsParallel().Sum(d => Math.Pow((d - avg), 2));
                ret = sum / (count-1);
            }
            return ret;
        }
        public double VarianzaLINQ(IEnumerable<int> values)
        {
            double ret = 0;
            int count = values.Count();
            if (count > 1)
            {
                double avg = values.Average();
                double sum = values.Sum(d => Math.Pow((d - avg),2));
                ret = sum / (count - 1);
            }
            return ret;
        }
    }
}
