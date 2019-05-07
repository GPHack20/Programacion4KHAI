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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }
        /// <summary>
        /// Set data
        /// </summary>
        public void CargarData()
        {
            dataGridView1.Rows.Clear();
            for (int i=0;i<MetodosAUX.programador.Length;i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1[0, i].Value = MetodosAUX.programador[i].id;
                dataGridView1[1, i].Value = MetodosAUX.programador[i].familia;
                dataGridView1[2, i].Value = MetodosAUX.programador[i].nombre;
                dataGridView1[3, i].Value = MetodosAUX.programador[i].snombre;
                dataGridView1[4, i].Value = MetodosAUX.programador[i].fnacimiento;
                dataGridView1[5, i].Value = MetodosAUX.programador[i].lenguajeP;
                dataGridView1[6, i].Value = MetodosAUX.programador[i].posicion;
                dataGridView1[7, i].Value = MetodosAUX.programador[i].exLaboral;
                dataGridView1[8, i].Value = MetodosAUX.programador[i].salario;
            }
        }

        private void Form2_Activated(object sender, EventArgs e)
        {
            CargarData();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
        //////Radiobuttons with the filters, groups,sort*****
        private void radioButton1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            var s1 = from ap in MetodosAUX.programador orderby ap.familia select ap;
            foreach (var s in s1)
            {
                dataGridView1.Rows.Add();
                dataGridView1[0, i].Value = s.id;
                dataGridView1[1, i].Value = s.familia;
                dataGridView1[2, i].Value = s.nombre;
                dataGridView1[3, i].Value = s.snombre;
                dataGridView1[4, i].Value = s.fnacimiento;
                dataGridView1[5, i].Value = s.lenguajeP;
                dataGridView1[6, i].Value = s.posicion;
                dataGridView1[7, i].Value = s.exLaboral;
                dataGridView1[8, i].Value = s.salario;
                i = i + 1;
            }
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            var s2 = from ap in MetodosAUX.programador orderby ap.nombre select ap;
            foreach (var s in s2)
            {
                dataGridView1.Rows.Add();
                dataGridView1[0, i].Value = s.id;
                dataGridView1[1, i].Value = s.familia;
                dataGridView1[2, i].Value = s.nombre;
                dataGridView1[3, i].Value = s.snombre;
                dataGridView1[4, i].Value = s.fnacimiento;
                dataGridView1[5, i].Value = s.lenguajeP;
                dataGridView1[6, i].Value = s.posicion;
                dataGridView1[7, i].Value = s.exLaboral;
                dataGridView1[8, i].Value = s.salario;
                i = i + 1;
            }
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            var s3 = from ap in MetodosAUX.programador orderby ap.lenguajeP select ap;
            foreach (var s in s3)
            {
                dataGridView1.Rows.Add();
                dataGridView1[0, i].Value = s.id;
                dataGridView1[1, i].Value = s.familia;
                dataGridView1[2, i].Value = s.nombre;
                dataGridView1[3, i].Value = s.snombre;
                dataGridView1[4, i].Value = s.fnacimiento;
                dataGridView1[5, i].Value = s.lenguajeP;
                dataGridView1[6, i].Value = s.posicion;
                dataGridView1[7, i].Value = s.exLaboral;
                dataGridView1[8, i].Value = s.salario;
                i = i + 1;
            }
        }

        private void radioButton4_Click(object sender, EventArgs e)
        {
            this.Refresh();
            var fil = from ap in MetodosAUX.programador group ap by ap.lenguajeP;
            List<String> len = new List<string>();         
            foreach (var i in fil)
            {
                len.Add(i.Key);
            }
            Filtro f = new Filtro(len.ToArray());
            f.ShowDialog();
            int ii = 0;
            var newdata = from j in MetodosAUX.programador where j.lenguajeP==f.fil_selec select j;
            dataGridView1.Rows.Clear();
            foreach (var jr in newdata)
            {
                dataGridView1.Rows.Add();
                dataGridView1[0, ii].Value = jr.id;
                dataGridView1[1, ii].Value = jr.familia;
                dataGridView1[2, ii].Value = jr.nombre;
                dataGridView1[3, ii].Value = jr.snombre;
                dataGridView1[4, ii].Value = jr.fnacimiento;
                dataGridView1[5, ii].Value = jr.lenguajeP;
                dataGridView1[6, ii].Value = jr.posicion;
                dataGridView1[7, ii].Value = jr.exLaboral;
                dataGridView1[8, ii].Value = jr.salario;
                ii = ii + 1;
            }
        }

        private void radioButton8_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            var s3 = from ap in MetodosAUX.programador group ap by ap.familia;
            foreach (var ss in s3)
            {
                foreach (Programador s in ss) {
                    dataGridView1.Rows.Add();
                    dataGridView1[0, i].Value = s.id;
                    dataGridView1[1, i].Value = s.familia;
                    dataGridView1[2, i].Value = s.nombre;
                    dataGridView1[3, i].Value = s.snombre;
                    dataGridView1[4, i].Value = s.fnacimiento;
                    dataGridView1[5, i].Value = s.lenguajeP;
                    dataGridView1[6, i].Value = s.posicion;
                    dataGridView1[7, i].Value = s.exLaboral;
                    dataGridView1[8, i].Value = s.salario;
                    i = i + 1;
                }
            }
        }

        private void radioButton9_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            var s3 = from ap in MetodosAUX.programador group ap by ap.salario;
            foreach (var ss in s3)
            {
                foreach (Programador s in ss)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1[0, i].Value = s.id;
                    dataGridView1[1, i].Value = s.familia;
                    dataGridView1[2, i].Value = s.nombre;
                    dataGridView1[3, i].Value = s.snombre;
                    dataGridView1[4, i].Value = s.fnacimiento;
                    dataGridView1[5, i].Value = s.lenguajeP;
                    dataGridView1[6, i].Value = s.posicion;
                    dataGridView1[7, i].Value = s.exLaboral;
                    dataGridView1[8, i].Value = s.salario;
                    i = i + 1;
                }
            }
        }

        private void radioButton10_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            var s3 = from ap in MetodosAUX.programador group ap by ap.lenguajeP;
            foreach (var ss in s3)
            {
                foreach (Programador s in ss)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1[0, i].Value = s.id;
                    dataGridView1[1, i].Value = s.familia;
                    dataGridView1[2, i].Value = s.nombre;
                    dataGridView1[3, i].Value = s.snombre;
                    dataGridView1[4, i].Value = s.fnacimiento;
                    dataGridView1[5, i].Value = s.lenguajeP;
                    dataGridView1[6, i].Value = s.posicion;
                    dataGridView1[7, i].Value = s.exLaboral;
                    dataGridView1[8, i].Value = s.salario;
                    i = i + 1;
                }
            }
        }

        private void radioButton11_Click(object sender, EventArgs e)
        {
            var s3 = (from st in MetodosAUX.programador select st.salario).Max();
            MessageBox.Show("MAX зарплата: "+s3);
        }

        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void radioButton13_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void radioButton12_Click(object sender, EventArgs e)
        {
            var s3 = (from st in MetodosAUX.programador select st.exLaboral).Max();
            MessageBox.Show("MAX стаж работы: " + s3);
        }

        private void radioButton13_Click(object sender, EventArgs e)
        {        
            var s3 = MetodosAUX.programador.GroupBy(s => s.lenguajeP).OrderByDescending(s => s.Count()).First().Key;
            MessageBox.Show("MAX язык программирования: " + s3);
        }

        private void radioButton14_Click(object sender, EventArgs e)
        {
            var s3 = from d in MetodosAUX.programador where (DateTime.Now - Convert.ToDateTime(d.fnacimiento)).Days / 365 >= 20 select d.nombre;
            foreach (var i in s3)
            {
                MessageBox.Show("Name: "+i);
            }
            
        }

        private void radioButton14_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton5_Click(object sender, EventArgs e)
        {
            this.Refresh();
            var fil = from ap in MetodosAUX.programador group ap by ap.posicion;
            List<String> len = new List<string>();
            foreach (var i in fil)
            {
                len.Add(i.Key);
            }
            Filtro f = new Filtro(len.ToArray());
            f.ShowDialog();
            int ii = 0;
            var newdata = from j in MetodosAUX.programador where j.posicion == f.fil_selec select j;
            dataGridView1.Rows.Clear();
            foreach (var jr in newdata)
            {
                dataGridView1.Rows.Add();
                dataGridView1[0, ii].Value = jr.id;
                dataGridView1[1, ii].Value = jr.familia;
                dataGridView1[2, ii].Value = jr.nombre;
                dataGridView1[3, ii].Value = jr.snombre;
                dataGridView1[4, ii].Value = jr.fnacimiento;
                dataGridView1[5, ii].Value = jr.lenguajeP;
                dataGridView1[6, ii].Value = jr.posicion;
                dataGridView1[7, ii].Value = jr.exLaboral;
                dataGridView1[8, ii].Value = jr.salario;
                ii = ii + 1;
            }
        }

        private void radioButton6_Click(object sender, EventArgs e)
        {
            this.Refresh();
            var fil = from ap in MetodosAUX.programador group ap by ap.familia;
            List<String> len = new List<string>();
            foreach (var i in fil)
            {
                len.Add(i.Key);
            }
            Filtro f = new Filtro(len.ToArray());
            f.ShowDialog();
            int ii = 0;
            var newdata = from j in MetodosAUX.programador where j.familia == f.fil_selec select j;
            dataGridView1.Rows.Clear();
            foreach (var jr in newdata)
            {
                dataGridView1.Rows.Add();
                dataGridView1[0, ii].Value = jr.id;
                dataGridView1[1, ii].Value = jr.familia;
                dataGridView1[2, ii].Value = jr.nombre;
                dataGridView1[3, ii].Value = jr.snombre;
                dataGridView1[4, ii].Value = jr.fnacimiento;
                dataGridView1[5, ii].Value = jr.lenguajeP;
                dataGridView1[6, ii].Value = jr.posicion;
                dataGridView1[7, ii].Value = jr.exLaboral;
                dataGridView1[8, ii].Value = jr.salario;
                ii = ii + 1;
            }
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void radioButton7_Click(object sender, EventArgs e)
        {
            this.Refresh();
            var fil = from ap in MetodosAUX.programador group ap by ap.salario;
            List<double> len = new List<double>();
            foreach (var i in fil)
            {
                len.Add(i.Key);
            }
            Filtro f = new Filtro(len.ToArray());
            f.ShowDialog();
            int ii = 0;
            var newdata = from j in MetodosAUX.programador where j.salario == double.Parse(f.fil_selec) select j;
            dataGridView1.Rows.Clear();
            foreach (var jr in newdata)
            {
                dataGridView1.Rows.Add();
                dataGridView1[0, ii].Value = jr.id;
                dataGridView1[1, ii].Value = jr.familia;
                dataGridView1[2, ii].Value = jr.nombre;
                dataGridView1[3, ii].Value = jr.snombre;
                dataGridView1[4, ii].Value = jr.fnacimiento;
                dataGridView1[5, ii].Value = jr.lenguajeP;
                dataGridView1[6, ii].Value = jr.posicion;
                dataGridView1[7, ii].Value = jr.exLaboral;
                dataGridView1[8, ii].Value = jr.salario;
                ii = ii + 1;
            }
        }
        ///////*******
    }
}
