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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }        

        private void label7_Click(object sender, EventArgs e)
        {

        }
        //Comparison of methods.
        public Visualization visualization = new Visualization();
        /// <summary>
        /// для визуализации результатов обработки последовательностей
        /// </summary>
        public Form2 form2 = new Form2();
        /// <summary>
        /// about
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }
        /// <summary>
        /// для визуализации результатов обработки последовательностей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (MetodosAUX.programador.Length!=0)
            {
                form2.Refresh();
                form2.Show();
                form2.Focus();
            }
            else
            {
                MessageBox.Show("There are not entered data!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {            
            visualization.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        /// <summary>
        /// Edit data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            String ap, nom, snom, fn, len, pos, el, sal;
            ap= Microsoft.VisualBasic.Interaction.InputBox("фамилия", "Apellido!", dataGridView1[1, e.RowIndex].Value.ToString());
            if (ap=="")
            {
                ap = dataGridView1[1, e.RowIndex].Value.ToString();
            }
            nom= Microsoft.VisualBasic.Interaction.InputBox("имя", "Nombre!", dataGridView1[2, e.RowIndex].Value.ToString());
            if (nom=="")
            {
                nom= dataGridView1[2, e.RowIndex].Value.ToString();
            }
            snom= Microsoft.VisualBasic.Interaction.InputBox("отчество", "Segundo nombre!", dataGridView1[3, e.RowIndex].Value.ToString());
            if (snom=="")
            {
                snom= dataGridView1[3, e.RowIndex].Value.ToString();
            }
            fn= Microsoft.VisualBasic.Interaction.InputBox("дата рождения", "Fecha de nacimiento!", dataGridView1[4, e.RowIndex].Value.ToString());
            if (fn=="")
            {
                fn= dataGridView1[4, e.RowIndex].Value.ToString();
            }
            len= Microsoft.VisualBasic.Interaction.InputBox("язык программирования", "Lenguaje de programacion!", dataGridView1[5, e.RowIndex].Value.ToString());
            if (len=="")
            {
                len= dataGridView1[5, e.RowIndex].Value.ToString();
            }
            pos= Microsoft.VisualBasic.Interaction.InputBox("должность", "Posicion!", dataGridView1[6, e.RowIndex].Value.ToString());
            if (pos=="")
            {
                pos = dataGridView1[6, e.RowIndex].Value.ToString();
            }
            el= Microsoft.VisualBasic.Interaction.InputBox("стаж работы", "Experencia Laboral!", dataGridView1[7, e.RowIndex].Value.ToString());
            if (el=="")
            {
                el= dataGridView1[7, e.RowIndex].Value.ToString();
            }
            sal = Microsoft.VisualBasic.Interaction.InputBox("зарплата", "Salario!", dataGridView1[8, e.RowIndex].Value.ToString());
            if (sal=="")
            {
                sal= dataGridView1[8, e.RowIndex].Value.ToString();
            }
            dataGridView1[1, e.RowIndex].Value = ap;
            dataGridView1[2, e.RowIndex].Value = nom;
            dataGridView1[3, e.RowIndex].Value = snom;
            dataGridView1[4, e.RowIndex].Value = fn;
            dataGridView1[5, e.RowIndex].Value = len;
            dataGridView1[6, e.RowIndex].Value = pos;
            dataGridView1[7, e.RowIndex].Value = el;
            dataGridView1[8, e.RowIndex].Value = sal;
            Programador progra = new Programador(e.RowIndex, dataGridView1[1, e.RowIndex].Value.ToString(), dataGridView1[2, e.RowIndex].Value.ToString(),
                dataGridView1[3, e.RowIndex].Value.ToString(), dataGridView1[4, e.RowIndex].Value.ToString(), dataGridView1[5, e.RowIndex].Value.ToString(), 
                dataGridView1[6, e.RowIndex].Value.ToString(),Int32.Parse(dataGridView1[7, e.RowIndex].Value.ToString()),double.Parse(dataGridView1[8, e.RowIndex].Value.ToString()));
            MetodosAUX.programador[e.RowIndex] =progra;
        }

        private void maskedTextBox1_Validating(object sender, CancelEventArgs e)
        {
            
        }
        /// <summary>
        /// Verify data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void maskedTextBox1_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
        {
            if (!e.IsValidInput)
            {
                MessageBox.Show("The data you supplied must be a valid date in the format mm/dd/yyyy.");
                maskedTextBox1.Text = "";
            }
            else
            {
                DateTime userDate = (DateTime)e.ReturnValue;
                if (userDate > DateTime.Now)
                {
                    MessageBox.Show("Invalid Date");
                    //MessageBox.Show("The date in this field must be less than today's date.");
                    maskedTextBox1.Text = "";
                    e.Cancel = true;
                }
            }
        }
        /// <summary>
        /// Add data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Trim()!=""&&textBox3.Text.Trim()!=""&&textBox4.Text.Trim()!=""&&textBox6.Text.Trim()!=""&&textBox7.Text.Trim()!=""&&textBox8.Text.Trim()!=""&&textBox9.Text.Trim()!=""&&maskedTextBox1.Text!= "  /  /")
            {                
                Array.Resize<Programador>(ref MetodosAUX.programador, MetodosAUX.programador.Length + 1);
                Programador _programador = new Programador(MetodosAUX.id+1,textBox2.Text.Trim(),textBox3.Text.Trim(),textBox4.Text.Trim(),maskedTextBox1.Text,textBox6.Text,textBox7.Text,Int32.Parse(textBox8.Text.Trim()),double.Parse(textBox9.Text.Trim()));
                MetodosAUX.programador[MetodosAUX.id] = _programador;
                dataGridView1.Rows.Add();
                dataGridView1[0, MetodosAUX.id].Value = _programador.id;
                dataGridView1[1, MetodosAUX.id].Value = _programador.familia;
                dataGridView1[2, MetodosAUX.id].Value = _programador.nombre;
                dataGridView1[3, MetodosAUX.id].Value = _programador.snombre;
                dataGridView1[4, MetodosAUX.id].Value = _programador.fnacimiento;
                dataGridView1[5, MetodosAUX.id].Value = _programador.lenguajeP;
                dataGridView1[6, MetodosAUX.id].Value = _programador.posicion;
                dataGridView1[7, MetodosAUX.id].Value = _programador.exLaboral;
                dataGridView1[8, MetodosAUX.id].Value = _programador.salario;
                MessageBox.Show("Information added!");
                MetodosAUX.id = MetodosAUX.id + 1;
            }
            else
            {
                MessageBox.Show("Enter all the data!");
            }
        }

        private void maskedTextBox1_Click(object sender, EventArgs e)
        {
        }
        //Validate the last textboxs
        private void textBox8_Validated(object sender, EventArgs e)
        {
            try
            {
                int tmp = Int32.Parse(textBox8.Text);
            }
            catch
            {
                MessageBox.Show("Insert only numbers!");
                textBox8.Text = "";
            }
        }

        private void textBox9_Validated(object sender, EventArgs e)
        {
            try
            {
                int tmp = Int32.Parse(textBox9.Text);
            }
            catch
            {
                MessageBox.Show("Insert only decimal numbers!");
                textBox9.Text = "";                
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
