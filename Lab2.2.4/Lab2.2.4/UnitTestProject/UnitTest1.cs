using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab2._2._4;
using System.Windows;
using System.IO;
namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        //* Отсортировать элементы массива по возрастанию в строках, представить их в квадратном массиве.
        [TestMethod]
        public void Test_firstOP_1()
        {
            int[,] arrayI = new int[,] { { 6, 4, 3, 2, 1 },{ 8, 7, 6, 5, 4 }, { 6, 4, 3, 2, 1 }, { 8, 7, 6, 5, 4 }, { 6, 4, 3, 2, 1 } };
            int[,] arrayE = new int [,] { {1,2,3,4,6 },{4,5,6,7,8 }, { 1, 2, 3, 4, 6 }, { 4, 5, 6, 7, 8 }, { 1, 2, 3, 4, 6 } };
            Tmas.FA = (int[,])arrayI.Clone();
            Tmas.OP1();
            int[,]arraSort = (int[,])Tmas.FA_sort.Clone();
            //Assert.AreEqual(arrayE, arraSort);
            CollectionAssert.AreEqual(arrayE,arraSort);

        }
        [TestMethod]
        public void Test_firstOP_2()
        {
            int[,] arrayI = new int[,] { { 6, 4, 3, 2, 0 }, { 18, 7, 6, 5, 4 }, { 16, 4, 3, 2, 1 }, { 9, 7, 6, 5, 4 }, { 16, 4, 3, 2, 1 } };
            int[,] arrayE = new int[,] { { 0, 2, 3, 4, 6 }, { 4, 5, 6, 7, 18 }, { 1, 2, 3, 4, 16 }, { 4, 5, 6, 7, 9 }, { 1, 2, 3, 4, 16 } };
            Tmas.FA = (int[,])arrayI.Clone();
            Tmas.OP1();
            int[,] arraSort = (int[,])Tmas.FA_sort.Clone();
            //Assert.AreEqual(arrayE, arraSort);
            CollectionAssert.AreEqual(arrayE, arraSort);
        }

        //* Определить дисперсию элементов массива.
        [TestMethod]
        public void Test_secondOP_1()
        {
            int[,] arrayI = new int[,] { { 6, 4, 3, 2, 0 }, { 18, 7, 6, 5, 4 }, { 16, 4, 3, 2, 1 }, { 9, 7, 6, 5, 4 }, { 16, 4, 3, 2, 1 } };
            Tmas.FA = (int[,])arrayI.Clone();
            double var_ex = 21.289600000000004;
            Tmas.OP2();
            double var_ob = (double)Tmas.var;
            Assert.AreEqual(var_ex,var_ob);
        }
        [TestMethod]
        public void Test_secondOP_2()
        {
            int[,] arrayI = new int[,] { { 6, 41, 3, 2, 0 }, { 18, 7, 6, 5, 4 }, { 16, 4, 3, 12, 1 }, { 9, 7, 6, 15, 4 }, { 16, 4, 3, 2, 1 } };
            Tmas.FA = (int[,])arrayI.Clone();
            double var_ex = 71.12;
            Tmas.OP2();
            double var_ob = (double)Tmas.var;
            Assert.AreEqual(var_ex, var_ob);
        }

        //* Элементы массива с суммой индексов кратной 3 умножить на (-g).
        [TestMethod]
        public void Test_thirdOP_1()
        {
            int[,] arrayI = new int[,] { { 6, 4, 3, 2, 0 }, { 18, 7, 6, 5, 4 }, { 16, 4, 3, 2, 1 }, { 9, 7, 6, 5, 4 }, { 16, 4, 3, 2, 1 } };
            int[,] arrayE = new int[,] { { -18, 4, 3, -6, 0 }, { 18, 7, -18, 5, 4 }, { 16, -12, 3, 2, -3 }, { -27, 7, 6, -15, 4 }, { 16, 4, -9, 2, 1 } };
            Tmas.FA = (int[,])arrayI.Clone();
            Tmas._g = 3;
            Tmas.OP3();            
            int[,] newelements = (int[,])Tmas.new_elements.Clone();
            //Assert.AreEqual(arrayE, arraSort);
            CollectionAssert.AreEqual(arrayE, newelements);

        }
        [TestMethod]
        public void Test_thirdOP_2()
        {
            int[,] arrayI = new int[,] { { 11, 4, 3, 2, 1 }, { 18, 7, 6, 3, 4 }, { 6, 4, 3, 2, 1 }, { 9, 7, 6, 5, 4 }, { 16, 4, 3, 2, 1 } };
            int[,] arrayE = new int[,] { { -33, 4, 3, -6, 1 }, { 18, 7, -18, 3, 4 }, { 6, -12, 3, 2, -3 }, { -27, 7, 6, -15, 4 }, { 16, 4, -9, 2, 1 } };
            Tmas.FA = (int[,])arrayI.Clone();
            Tmas._g = 3;
            Tmas.OP3();
            int[,] newelements = (int[,])Tmas.new_elements.Clone();
            //Assert.AreEqual(arrayE, arraSort);
            CollectionAssert.AreEqual(arrayE, newelements);
        }

        //[TestMethod]
        //public void Read_File_Array()
        //{
        //    int[,] arrayE = new int[,] { { -33, 4, 3, -6, 1 }, { 18, 7, -18, 3, 4 }, { 6, -12, 3, 2, -3 }, { -27, 7, 6, -15, 4 }, { 16, 4, -9, 2, 1 } };
        //    Tmas.Read_File_Array("file.txt");
        //    CollectionAssert.AreEqual(arrayE, Tmas.FA);
        //}
        //[TestMethod]
        //public void Read_File_Array2()
        //{
        //    int[,] arrayE = new int[,] { { 11, 4, 4, -6, 1 }, { 18, 7, -18, 3, 4 }, { 6, -12, 3, 2, -3 }, { -27, 7, 6, -15, 4 }, { 16, 4, -9, 2, 1 } };
        //    Tmas.Read_File_Array("file1.txt");
        //    CollectionAssert.AreEqual(arrayE, Tmas.FA);
        //}

        //[TestMethod]
        //public void Write_Input_Array()
        //{
        //    Tmas.FA = new int[,] { { 11, 4, 4, -6, 1 }, { 18, 7, -18, 3, 4 }, { 6, -12, 3, 2, -3 }, { -27, 7, 6, -15, 4 }, { 16, 4, -9, 2, 1 } };
        //    Tmas.Write_Input_Array("test.txt");
        //}
        //[TestMethod]
        //public void GenerarElementos()
        //{

        //}
        //[TestMethod]
        //public void SaveResults()
        //{

        //}
    }
}
