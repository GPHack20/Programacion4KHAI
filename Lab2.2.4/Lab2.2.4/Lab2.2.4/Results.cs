using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2._2._4
{
    public partial class Results : Form
    {
        public Results()
        {
            InitializeComponent();
        }

        //In this method the program is going to put the array entered into the datagridview and the second part is going to execute the selected operation and show into the datagridview and/or textbox
        private void Results_Activated(object sender, EventArgs e)
        {
            textBox1.Text = "";
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView1.ColumnCount = Tmas.FA.GetLength(0);
            dataGridView2.ColumnCount = Tmas.FA.GetLength(0);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            for (int c = 0; c < Tmas.FA.GetLength(0); c++)
            {
                string[] row = new string[Tmas.FA.GetLength(1)];
                for (int i = 0; i < Tmas.FA.GetLength(1); i++)
                {
                    row[i] = Tmas.FA[c, i].ToString();
                }
                dataGridView1.Rows.Add(row);
                dataGridView2.Rows.Add(row);
            }
            label1.Text = "N = "+Tmas.FA.GetLength(0)+"x"+Tmas.FA.GetLength(1)+"; Min = "+Tmas._min+"; Max = "+Tmas._max;
            switch (Tmas.op)
            {
                case 1:
                    Tmas.op1 = true;
                    Tmas.OP1();
                    dataGridView2.Rows.Clear();
                    for (int c = 0; c < Tmas.FA.GetLength(0); c++)
                    {
                        string[] row = new string[Tmas.FA.GetLength(1)];
                        for (int i = 0; i < Tmas.FA.GetLength(1); i++)
                        {
                            row[i] = Tmas.FA_sort[c, i].ToString();
                        }
                        dataGridView2.Rows.Add(row);
                        textBox1.Text = "Array is sorted ==>";
                    }
                    break;
                case 2:
                    Tmas.op2 = true;
                    Tmas.OP2();
                    textBox1.Text = "Дисперсию элементов массива:" + Tmas.var.ToString();
                    break;
                case 3:
                    Tmas.op3 = true;
                    Tmas.OP3();
                    dataGridView2.Rows.Clear();
                    for (int c = 0; c < Tmas.FA.GetLength(0); c++)
                    {
                        string[] row = new string[Tmas.FA.GetLength(1)];
                        for (int i = 0; i < Tmas.FA.GetLength(1); i++)
                        {
                            row[i] = Tmas.new_elements[c, i].ToString();
                        }
                        dataGridView2.Rows.Add(row);
                        textBox1.Text = "Элементы массива с суммой индексов кратной 3 умножить на (-g) ==>";
                    }
                    break;
            }
        }

        //Button to hide the form
        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
}