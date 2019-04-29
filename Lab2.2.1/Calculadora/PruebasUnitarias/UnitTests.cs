using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculadoraCompleta;
using System.Windows.Forms;

namespace PruebasUnitarias
{
    [TestClass]
    public class UnitTest1
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
            main.b_borrarDIG_Click(button, new EventArgs());
            Assert.AreEqual(newvalorTextbox, tb.Text);
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
            main.b_borrarTODO_Click(lb, new EventArgs());
            Assert.AreEqual(newvalorLabel, lb.Text, newvalorTextbox, tb.Text);
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
            var result = num1 + num2; //Add
            Form1 main = new Form1();
            Button button = main.b_igual;
            main.firstVar = num1;
            TextBox tb = main.textbox_result;
            tb.Text = num2.ToString();
            main.operation = "+";
            main.button_Eql_Click(button, new EventArgs());
            Assert.AreEqual(result.ToString(), tb.Text);
        }
        [TestMethod]
        public void button_num_Click_()
        {
            var previoVAL = "-22";
            var newVAL = "4";
            var result = previoVAL + newVAL;
            Form1 main = new Form1();
            Button button = main.b_4;
            TextBox tb = main.textbox_result;
            tb.Text = previoVAL.ToString();
            main.button_num_Click(button,new EventArgs());
            Assert.AreEqual(result,tb.Text);
        }
        [TestMethod]
        public void button_Oper_Click_()
        {
            var firstVAL = "";
            var secondVAL = "34";
            var esp = secondVAL + " ÷ ";
            Form1 main = new Form1();
            Button button = main.b_dividir;
            Label his = main.l_historial;
            his.Text = firstVAL.ToString();
            TextBox tb = main.textbox_result;
            tb.Text = secondVAL;
            main.button_Oper_Click(button,new EventArgs());
            Assert.AreEqual(esp,his.Text);
        }
        [TestMethod]
        public void button_OpMyVariant_Click_()
        {
            var value = "5";
            var espVALTEXBOX = "120";
            var espVALLABEL = "(5)!";
            Form1 main = new Form1();
            main.textbox_result.Text = value;
            main.l_historial.Text = "";
            main.b_factorial.Text = "x!";
            main.b_igual.Text = "=";
            main.button_OpMyVariant_Click(main.b_factorial,new EventArgs());
            Assert.AreEqual(espVALLABEL,main.l_historial.Text,espVALTEXBOX,main.textbox_result);
            
        }
        [TestMethod]
        public void textbox_result_Click_()
        {
            Form1 main = new Form1();
            TextBox tb = main.textbox_result;
            var prevVAL = "0";
            var espVAL = "";
            tb.Text = prevVAL;
            main.textbox_result_Click(new object(),new EventArgs());
            Assert.AreEqual(espVAL,tb.Text);
        }
    }
}
