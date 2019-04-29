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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        //Instance of windows form with the results
        public Results results=new Results();

        //Instance of information about the program
        public Information information;

        //Button to show the information about the program
        private void button6_Click(object sender, EventArgs e)
        {
            Information info = new Information();
            info.ShowDialog();
        }

        //Button to show the windows with the result of the each operations 
        private void button5_Click(object sender, EventArgs e)
        {
            Tmas._g = (int)numericUpDown1.Value;
            results.Refresh();
            results.Show();
            results.Focus();
        }

        //Method to verify if the user select the correct minimium number of the interval
        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown5.Value > numericUpDown6.Value)
            {
                MessageBox.Show("This number can be higher than Max!");
                //numericUpDown5.Value -= 1;
                numericUpDown5.Value = numericUpDown6.Value - 1;
            }
            else
            {
                if (numericUpDown6.Value == 0 || numericUpDown6.Value <= numericUpDown5.Value)
                {
                    numericUpDown6.Value = numericUpDown5.Value + 1;
                }
            }
        }

        //Method to verify if the user select the correct maximium number of the interval
        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown6.Value <= numericUpDown5.Value)
            {
                MessageBox.Show("This number can be less than Min!");
                numericUpDown6.Value = numericUpDown5.Value + 1;
            }
        }

        //Button to generate the array with random numbers
        private void button1_Click(object sender, EventArgs e)
        {
            if (numericUpDown4.Value!=0&&numericUpDown6.Value!=numericUpDown5.Value)
            {
                Tmas.GenerarElementos((int)numericUpDown4.Value,(int)numericUpDown5.Value,(int)numericUpDown6.Value);
                results.Show();
                results.Focus();
                button5.Enabled = true;
                button3.Enabled = true;
            }
            else
            {
                MessageBox.Show("Incorrect parameters!");
            }
        }

        //Button to write the array in the text file
        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog file_input_array = new SaveFileDialog();
            file_input_array.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            file_input_array.FilterIndex = 1;
            file_input_array.RestoreDirectory = true;
            file_input_array.Title = "File to save input array";
            if (file_input_array.ShowDialog()==DialogResult.OK)
            {
                Tmas.Write_Input_Array(file_input_array.FileName);
            }            
        }

        //Button to open a text file with an array
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog file_with_array = new OpenFileDialog();
                file_with_array.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                file_with_array.FilterIndex = 1;
                file_with_array.Title = "File with the array";
                if (file_with_array.ShowDialog() == DialogResult.OK)
                {
                    Tmas.Read_File_Array(file_with_array.FileName);
                    //MessageBox.Show("-"+file_with_array.FileName+"-");
                    results.Refresh();
                    results.Show();
                    results.Focus();
                }
            }
            catch
            {
                MessageBox.Show("Incorrect format of the file!");
            }
        }

        //General method to select and do the operations of my variant
        private void radioButton1_Click(object sender, EventArgs e)
        {
            RadioButton rd = (RadioButton)sender;
            if (Tmas.FA!=null)
            {
                if (rd==radioButton1)
                {
                    Tmas.op = 1;
                }
                if (rd==radioButton2)
                {
                    Tmas.op = 2;
                }
                if (rd==radioButton3)
                {                    
                    Tmas.op = 3;
                }
            }
            else
            {
                rd.Checked = false;
                MessageBox.Show("Array is not entered!");
            }
        }

        //Write a text file with an array entered and the results of the operations ultil had done all the operations
        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog file_input_array = new SaveFileDialog();
            file_input_array.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            file_input_array.FilterIndex = 1;
            file_input_array.RestoreDirectory = true;
            file_input_array.Title = "File to save results!";
            if (file_input_array.ShowDialog() == DialogResult.OK)
            {
                Tmas.SaveResults(file_input_array.FileName);
            }
        }

        //Enable the button to save the array and results when all the operations had done.
        private void Form1_Activated(object sender, EventArgs e)
        {
            if (Tmas.op1 && Tmas.op2 && Tmas.op3 == true)
                button4.Enabled = true;
        }
    }
}