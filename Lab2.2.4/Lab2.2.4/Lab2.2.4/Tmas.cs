using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2._2._4
{
    public class Tmas
    {
        public static int[,] FA;//Array entered 
        public static int _min;//min interval
        public static int _max;//max interval
        public static int op=0;//Operation selected

        public static int[,] FA_sort;//1 Operation => array with sort rows
        public static double var;//2 Operation => Определить дисперсию элементов массива
        public static int[,] new_elements;//3 Operation => Элементы массива с суммой индексов кратной 3 умножить на (-g).
        public static int _g;//3 Operation => g value of the numericUpDown

        //Variables to change to true when they are execute to enable the save button results
        public static bool op1 = false;
        public static bool op2 = false;
        public static bool op3 = false;

        //Method to save the array entered and the results of each operations of my variant
        public static void SaveResults(string file)
        {
            FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write);
            if (fs != null)
            {
                StreamWriter wr = new StreamWriter(fs);
                if (FA != null)
                {
                    wr.WriteLine("Input array");
                    for (int i = 0; i < FA.GetLength(0); i++)
                    {
                        for (int c = 0; c < FA.GetLength(1); c++)
                        {
                            wr.Write(FA[i, c]+" ");
                        }
                        wr.Write("\r\n");
                    }
                    wr.Write("\r\n");
                }                
                wr.WriteLine("Operation 1:");
                if (FA_sort!=null)
                {
                    wr.WriteLine("Sort array by rows");
                    for (int i = 0; i < FA.GetLength(0); i++)
                    {
                        for (int c = 0; c < FA.GetLength(1); c++)
                        {
                            wr.Write(FA_sort[i, c]+" ");
                        }
                        wr.Write("\r\n");
                    }
                    wr.Write("\r\n");
                }
                wr.WriteLine("Operation 2:");
                wr.WriteLine("дисперсию элементов массива: "+var);
                wr.Write("\r\n");
                wr.WriteLine("Operation 3:");
                if (new_elements != null)
                {
                    wr.WriteLine("Элементы массива с суммой индексов кратной 3 умножить на (-g).");
                    for (int i = 0; i < FA.GetLength(0); i++)
                    {
                        for (int c = 0; c < FA.GetLength(1); c++)
                        {
                            wr.Write(new_elements[i, c]+" ");
                        }
                        wr.Write("\r\n");
                    }
                    wr.Write("\r\n");
                }
                wr.Flush();
                wr.Close();
                fs.Close();
            }
        }

        //Method to generate and save random numbers into array
        public static void GenerarElementos(int _l,int min,int max)
        {
            _min = min;
            _max = max;
            FA = new int[_l,_l];
            Random rm = new Random();
            for (int c=0;c<_l;c++)
            {
                for (int i=0;i<_l;i++)
                {
                    FA[c, i] = rm.Next(min,max);
                }
            }
        }

        //Write the array entered into the text file
        public static void Write_Input_Array(string file)
        {
            FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write);
            if (fs != null)
            {
                StreamWriter wr = new StreamWriter(fs);
                if (FA != null)
                { 
                    for (int i = 0; i < FA.GetLength(0); i++)
                    {
                        for (int c=0;c<FA.GetLength(1);c++)
                        {
                            wr.WriteLine(FA[i,c]);
                        }                        
                    }
                }
                else { MessageBox.Show("Массив не создан!", "Сообщение"); }
                wr.Flush();
                wr.Close();
                fs.Close();
            }
        }

        //Read an array from textfile with the correct format
        public static void Read_File_Array(string file)
        {
            try
            {
                FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                if (fs != null)
                {
                    StreamReader r = new StreamReader(fs);
                    string values = r.ReadToEnd();
                    int size = 0;
                    for (int c = 0; c < values.Length; c++)
                    {
                        if (values[c] == '\n')
                            size = size + 1;
                    }
                    size = (int)Math.Sqrt(size);
                    FA = new int[size, size];
                    fs.Position = 0;
                    for (int k = 0; k < size; k++)
                    {
                        for (int i = 0; i < size; i++)
                        {
                            FA[k, i] = Convert.ToInt32(r.ReadLine());
                        }

                    }
                    r.Close();
                    fs.Close();
                    //ADD RANGE
                }
            }
            catch
            {
                MessageBox.Show("The text file does not have the correct format!");
            }
        }

        //First operation of my variant: Отсортировать элементы массива по возрастанию в строках, представить их в квадратном массиве.
        public static void OP1()
        {
            FA_sort = (int[,])FA.Clone();
            int t;
            for (int c=0;c<FA.GetLength(0);c++)
            {
                for (int i=1;i<FA.GetLength(1);i++)
                {
                    for (int ii=FA.GetLength(1)-1;ii>=i;ii--)
                    {
                        if (FA_sort[c,ii-1]>FA_sort[c,ii])
                        {
                            t = FA_sort[c,ii-1];
                            FA_sort[c, ii - 1] = FA_sort[c,ii];
                            FA_sort[c,ii] = t;
                        }
                    }
                }
            }
        }

        //Second operation of my variant: Определить дисперсию элементов массива.
        public static void OP2()
        {
            double med, sum = 0, sum1=0;
            for (int c=0;c<FA.GetLength(0);c++)
            {
                for (int i=0;i<FA.GetLength(1);i++)
                {
                    sum += FA[c,i];
                    sum1 += Math.Pow(FA[c, i],2);
                }
            }
            med = sum / Math.Pow(FA.GetLength(0),2);
            var = (sum1 / (Math.Pow(FA.GetLength(0), 2))) - Math.Pow(med,2);
        }

        //3 Operation of my variant: Элементы массива с суммой индексов кратной 3 умножить на (-g).
        public static void OP3()
        {
            new_elements = (int[,])FA.Clone();
            for (int c = 0;c<new_elements.GetLength(0);c++)
            {
                for (int i=0;i<new_elements.GetLength(1);i++)
                {
                    if ((c+i)%3==0)
                    {
                        new_elements[c, i] = new_elements[c, i] * (-1) * (_g);
                    }
                }
            }
        }
    }
}