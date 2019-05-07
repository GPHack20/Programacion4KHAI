using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2._2._7
{
    /// <summary>
    /// Filters for radiobuttons
    /// </summary>
    public partial class Filtro : Form
    {
        public Array a;
        public String fil_selec;
        public Filtro(Array array)
        {
            a = array;
            InitializeComponent();
        }

        private void Filtro_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            foreach (var i in a)
            {
                comboBox1.Items.Add(i);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            fil_selec = comboBox1.SelectedItem.ToString();
        }
    }
}
