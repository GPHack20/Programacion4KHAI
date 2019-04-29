using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculadoraCompleta;
using System.Windows.Forms;

namespace UnitTestLAB1
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void b_borrarDIG_Click_()
        {
            var valorTextbox = "543";
            var newvalorTextbox = "54";
            Form1 main = new Form1();
            Button button = main.b_borrarDIG;
            TextBox tb = main.textbox_result;
            tb.Text = valorTextbox;
            main.b_borrarDIG_Click(button,new EventArgs());
            Assert.AreEqual(newvalorTextbox,tb.Text);
        }
        [TestMethod]
        public void b_borrarTODO_Click_()
        {
            var valorTextbox = "543";
            var newvalorTextbox = "";
            var valorLabel = "3+(4)";
            var newvalorLabel = "";
            Form1 main = new Form1();
            Button button = main.b_borrarTODO;
            TextBox tb = main.textbox_result;
            Label lb = main.l_historial;
            tb.Text = valorTextbox;
            lb.Text = valorLabel;
            main.b_borrarTODO_Click(lb,new EventArgs());
            Assert.AreEqual(newvalorLabel,lb.Text, newvalorTextbox, tb.Text);
        }
        [TestMethod]
        public void b_borrarTODO_S_Click_()
        {
            var valorTextbox = "543";
            var newvalorTextbox = "";
            Form1 main = new Form1();
            Button button = main.b_borrarTODO_S;
            TextBox tb = main.textbox_result;
            tb.Text = valorTextbox;
            main.b_borrarTODO_S_Click(button, new EventArgs());
            Assert.AreEqual(newvalorTextbox, tb.Text);
        }
        [TestMethod]
        public void button_Eql_Click_()
        {
            var num1 = 15.5;
            var num2 = 75.5;
            var result = num1+num2; //Add
            Form1 main = new Form1();
            Button button = main.b_igual;
            main.firstVar =num1;
            TextBox tb = main.textbox_result;
            tb.Text = num2.ToString();
            main.operation = "+";            
            main.button_Eql_Click(button,new EventArgs());
            Assert.AreEqual(result.ToString(),tb.Text);

        }
        [TestMethod]
        public void button_num_Click_()
        {

        }
        [TestMethod]
        public void button_Oper_Click_()
        {

        }
        [TestMethod]
        public void button_OpMyVariant_Click_()
        {

        }
        [TestMethod]
        public void Form1_Load_()
        {

        }
        [TestMethod]
        public void textbox_result_Click_()
        {

        }
        [TestMethod]
        public void textbox_result_KeyDown_()
        {

        }
        [TestMethod]
        public void textbox_result_KeyPress_()
        {

        }
    }
}
