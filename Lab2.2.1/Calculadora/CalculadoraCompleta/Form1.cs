using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculadoraCompleta
{
    public partial class Form1 : Form
    {
        public ContextMenu contxt_menu = new ContextMenu();    //Create an empty context menu object
        public string operation = "";  //Actual Operation
        public double lastOpe;         //Last operations
        public double firstVar;        //Number entered
        public double secondVar;       //Var to add
        public bool CanClear_MainWindow;
        Metodos metodos = new Metodos();

        public Form1()
        {
            InitializeComponent();
        }

        public void textbox_result_Click(object sender, EventArgs e)   //Set cursor in textbox
        {
            if (!(textbox_result.Text == "0"))
                return;
            textbox_result.Text = "";
        }

        public void textbox_result_KeyPress(object sender, KeyPressEventArgs e)        //Detect key entered according to the variant.
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-EC"); //Gets or sets the current culture used by the Resource Manager to look up culture-specific resources at run time.
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ',' && (e.KeyChar != '.' && e.KeyChar != '-') && (e.KeyChar != '\b' && e.KeyChar != '.'))
            {
                e.KeyChar = char.MinValue;
            }            
            else
            {
                if (l_historial.Text == "unknown format!" || l_historial.Text == "too big value!" || (l_historial.Text == "less than zero!" || l_historial.Text == "div by zero!"))
                    l_historial.Text = "";
                else if (l_historial.Text.Contains("unknown format!") || l_historial.Text.Contains("too big value!") || (l_historial.Text.Contains("less than zero!") || l_historial.Text.Contains("div by zero!")))
                    l_historial.Text = string.Format("{0} {1} ", (object)firstVar, (object)operation);
                if (e.KeyChar == ',' || e.KeyChar == '.')
                {
                    if (e.KeyChar == '.')
                        e.KeyChar = ',';
                    if (textbox_result.Text.IndexOf(',') != -1)
                    {
                        e.KeyChar = char.MinValue;
                    }
                }
                else if (e.KeyChar == '-')
                {
                    if (textbox_result.Text.Length == 0)
                        return;
                    e.KeyChar = char.MinValue;
                }
                else
                {
                    if (!char.IsDigit(e.KeyChar))
                        return;
                    if (!metodos.Intervalo(Convert.ToDouble(textbox_result.Text + e.KeyChar.ToString(), (IFormatProvider)Thread.CurrentThread.CurrentCulture.NumberFormat)))
                    {
                        if (l_historial.Text == "")
                            l_historial.Text = "too big value!";
                        else if (l_historial.Text != "" && l_historial.Text.IndexOf(string.Format(" {0} ", (object)operation)) == l_historial.Text.Length - 3)
                            l_historial.Text = string.Format("{0} {1} too big value!", (object)firstVar, (object)operation);
                        else if (l_historial.Text != "" && l_historial.Text.IndexOf(string.Format(" {0} ", (object)operation)) != l_historial.Text.Length - 3)
                            l_historial.Text = string.Format("{0} {1} too big value!", (object)lastOpe, (object)operation);
                        e.KeyChar = char.MinValue;
                    }
                    else
                    {
                        string text = textbox_result.Text;
                        int num = text.IndexOf(',');
                        if (num == -1 || text.Length - 1 - num <= 2)
                            return;
                        e.KeyChar = char.MinValue;
                    }
                }
            }
        }

        public void b_borrarTODO_Click(object sender, EventArgs e)//Clean all
        {
            l_historial.Text = "";
            textbox_result.Text = "0";
            operation = "";
        }

        public void textbox_result_KeyDown(object sender, KeyEventArgs e)//Event when press control + c or control + v to copy and paste
        {
            if (e.Control && (e.KeyCode == Keys.V))
            {
                string n = Clipboard.GetText();
                if (n.Length > 11)
                    e.Handled = false;
                string str = metodos.Formato(n);
                if (!(str == "dec"))
                {
                    e.Handled = true;
                }
                else
                {
                    if (n.Contains("."))
                        n = n.Replace('.', ',');
                    if (!metodos.Intervalo(Convert.ToDouble(n, (IFormatProvider)Thread.CurrentThread.CurrentCulture.NumberFormat)))
                        e.Handled = true;
                    else
                        textbox_result.Text = n;
                }
            }
            else
            {
                if (!e.Control || e.KeyValue != 67)
                    return;
                Clipboard.SetText(textbox_result.Text);
                Help.ShowPopup(textbox_result, "text copied!", new Point(textbox_result.Right, textbox_result.Left));
            }
        }

        public void b_borrarDIG_Click(object sender, EventArgs e)//Delete one by one digit
        {
            if (textbox_result.Text.Length <= 1)
            {
                textbox_result.Text = "0";
            }
            else
            {
                if (l_historial.Text == "unknown format!" || l_historial.Text == "too big value!" || (l_historial.Text == "less than zero!" || l_historial.Text == "div by zero!"))
                    l_historial.Text = "";
                else if (l_historial.Text.Contains("unknown format!") || l_historial.Text.Contains("too big value!") || (l_historial.Text.Contains("less than zero!") || l_historial.Text.Contains("div by zero!")))
                    l_historial.Text = string.Format("{0} {1} ", (object)firstVar, (object)operation);
                else if (l_historial.Text.Contains("sqrt") || l_historial.Text.Contains("1/") || l_historial.Text.Contains("^2")||l_historial.Text.Contains("!"))
                    l_historial.Text = "";
                else if (l_historial.Text != "" && l_historial.Text.IndexOf(string.Format(" {0} ", (object)operation)) != l_historial.Text.Length - 3)
                    l_historial.Text = "";
                textbox_result.Text = textbox_result.Text.Remove(textbox_result.Text.Length - 1, 1);
            }
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            textbox_result.ContextMenu = this.contxt_menu;  //Set an empty context menu object
        }

        public void button_num_Click(object sender, EventArgs e)       //Enter all digits 0-9
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-EC");
            if (CanClear_MainWindow)//Limpia los digitos actuales del texbox, para poder ingresar un nuevos luego de una operacion.
            {
                textbox_result.Text = "";
                CanClear_MainWindow = false;                
            }
            Button button = (Button)sender;//Estable el boton presionado
            if (l_historial.Text == "unknown format!" || l_historial.Text == "too big value!" || (l_historial.Text == "less than zero!" || l_historial.Text == "div by zero!"))//Limpia el historial si hubo error
                l_historial.Text = "";
            else if (l_historial.Text.Contains("unknown format!") || l_historial.Text.Contains("too big value!") || (l_historial.Text.Contains("less than zero!") || l_historial.Text.Contains("div by zero!")))//Limpia el mensaje de error si se ingresa una coma
                l_historial.Text = string.Format("{0} {1} ", (object)firstVar, (object)operation);
            if (textbox_result.Text == "0")
                textbox_result.Text = "";
            if (button.Text == ",")
            {
                if (textbox_result.Text == "")
                {
                    textbox_result.Text = "0" + button.Text;
                }
                else
                {
                    if (!(metodos.Formato(textbox_result.Text) == "dec") || textbox_result.Text.Contains(","))//Verifica si ya existe una coma.
                        return;
                    textbox_result.Text += button.Text;
                }
            }
            else
            {
                if (!char.IsDigit(Convert.ToChar(button.Text)))
                    return;
                if (!metodos.Intervalo(Convert.ToDouble(textbox_result.Text + button.Text, (IFormatProvider)Thread.CurrentThread.CurrentCulture.NumberFormat)))
                {
                    if (l_historial.Text == "")
                        l_historial.Text = "too big value!";
                    else if (l_historial.Text != "" && l_historial.Text.IndexOf(string.Format(" {0} ", (object)operation)) == l_historial.Text.Length - 3)
                    {
                        l_historial.Text = string.Format("{0} {1} too big value!", (object)firstVar, (object)operation);
                    }
                    else
                    {
                        if (!(l_historial.Text != "") || l_historial.Text.IndexOf(string.Format(" {0} ", (object)operation)) == l_historial.Text.Length - 3)//Luego de una operacion e ingreso de un numero erroneo, se visualiza en el historial el resultado de la anterior mas mensaje de error
                            return;
                        l_historial.Text = string.Format("{0} {1} too big value!", (object)lastOpe, (object)operation);
                    }
                }
                else
                {
                    string str = textbox_result.Text + button.Text;
                    int num = str.IndexOf(',');
                    if (num != -1)//Verificar el numero de decimales
                    {
                        if (str.Length - 1 - num > 3)
                            return;
                        textbox_result.Text = str;
                    }
                    else
                        textbox_result.Text = str;
                }
            }
        }

        public void button_Oper_Click(object sender, EventArgs e)//Basic operations
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-EC");
            Button button = (Button)sender;
            string text = textbox_result.Text;
            string str = metodos.Formato(textbox_result.Text);
            if (!(str == "dec"))
            {
                if (str == "nul")
                {
                    if (textbox_result.Text == "")
                    {
                        textbox_result.Text = "0";
                        return;
                    }
                    if (this.l_historial.Text == "")
                        this.l_historial.Text = "unknown format!";
                    else if (this.l_historial.Text == "unknown format!" || this.l_historial.Text == "too big value!" || (this.l_historial.Text == "less than zero!" || this.l_historial.Text == "div by zero!"))
                        this.l_historial.Text = "unknown format!";
                    else if (this.l_historial.Text != "" && this.l_historial.Text.IndexOf(string.Format(" {0} ", (object)button.Text)) == this.l_historial.Text.Length - 3)
                        this.l_historial.Text = string.Format("{0} {1} unknown format!", (object)this.firstVar, (object)button.Text);
                    else if (this.l_historial.Text.Contains("unknown format!") || this.l_historial.Text.Contains("too big value!") || (this.l_historial.Text.Contains("less than zero!") || this.l_historial.Text.Contains("div by zero!")))
                        this.l_historial.Text = string.Format("{0} {1} unknown format!", (object)this.firstVar, (object)button.Text);
                    else if (this.l_historial.Text != "" && this.l_historial.Text.IndexOf(string.Format(" {0} ", (object)button.Text)) != this.l_historial.Text.Length - 3)
                        this.l_historial.Text = "unknown format!";
                }
            }
            else
            {
                double n = double.Parse(this.textbox_result.Text, (IFormatProvider)Thread.CurrentThread.CurrentCulture.NumberFormat);
                bool interval = metodos.Intervalo(n);
                if (interval)
                {
                        if (this.l_historial.Text == "unknown format!" || this.l_historial.Text == "too big value!" || (this.l_historial.Text == "less than zero!" || this.l_historial.Text == "div by zero!"))
                            this.l_historial.Text = "";
                        else if (this.l_historial.Text.Contains("unknown format!") || this.l_historial.Text.Contains("too big value!") || (this.l_historial.Text.Contains("less than zero!") || this.l_historial.Text.Contains("div by zero!")))
                            this.l_historial.Text = string.Format("{0} {1} ", (object)this.firstVar, (object)button.Text);
                        if (this.l_historial.Text == "")
                        {
                            this.firstVar = n;
                            this.l_historial.Text = string.Format("{0} {1} ", (object)text, (object)button.Text);
                        }
                        else if (this.l_historial.Text != "" && this.l_historial.Text.IndexOf(string.Format(" {0} ", (object)button.Text)) == this.l_historial.Text.Length - 3)
                        {
                            if (this.operation == "÷")
                            {
                                this.secondVar = n;
                                if (this.secondVar != 0.0)
                                {
                                    this.operation = string.Format("{0}", (object)button.Text);
                                    this.b_igual.PerformClick();//LLama al boton = para realizar la division anterior y dividir nuevamente.
                                    this.l_historial.Text = string.Format("{0} {1} ({2})", (object)this.firstVar, (object)button.Text, (object)text);
                                }
                                else if (this.l_historial.Text == "")
                                    this.l_historial.Text = "div by zero!";
                                else if (this.l_historial.Text != "" && this.l_historial.Text.IndexOf(string.Format(" {0} ", (object)this.operation)) == this.l_historial.Text.Length - 3)
                                    this.l_historial.Text = "div by zero!";
                            }
                            else
                            {
                                this.secondVar = n;
                                this.operation = string.Format("{0}", (object)button.Text);
                                this.b_igual.PerformClick();
                                if (this.l_historial.Text != "too big value!")
                                    this.l_historial.Text = string.Format("{0} {1} ({2})", (object)this.firstVar, (object)button.Text, (object)text);
                            }
                        }
                        else if (this.l_historial.Text != "" && this.l_historial.Text.IndexOf(string.Format(" {0} ", (object)button.Text)) != this.l_historial.Text.Length - 3)
                        {
                            this.firstVar = n;
                            this.l_historial.Text = string.Format("{0} {1} ", (object)this.firstVar, (object)button.Text);
                        }
                }
                else if (this.l_historial.Text == "")
                    this.l_historial.Text = "too big value!";
                else if (this.l_historial.Text == "unknown format!" || this.l_historial.Text == "too big value!" || (this.l_historial.Text == "less than zero!" || this.l_historial.Text == "div by zero!"))
                    this.l_historial.Text = "too big value!";
                else if (this.l_historial.Text != "" && this.l_historial.Text.IndexOf(string.Format(" {0} ", (object)button.Text)) == this.l_historial.Text.Length - 3)
                    this.l_historial.Text = string.Format("{0} {1} too big value!", (object)this.firstVar, (object)button.Text);
                else if (this.l_historial.Text.Contains("unknown format!") || this.l_historial.Text.Contains("too big value!") || (this.l_historial.Text.Contains("less than zero!") || this.l_historial.Text.Contains("div by zero!")))
                    this.l_historial.Text = string.Format("{0} {1} too big value!", (object)this.firstVar, (object)button.Text);
                else if (this.l_historial.Text != "" && this.l_historial.Text.IndexOf(string.Format(" {0} ", (object)button.Text)) != this.l_historial.Text.Length - 3)
                    this.l_historial.Text = "too big value!";
            }
            this.operation = string.Format("{0}", (object)button.Text);
            this.CanClear_MainWindow = true;
        }

        public void button_Eql_Click(object sender, EventArgs e)//Button =
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-EC");
            string text = this.textbox_result.Text;
            string str = metodos.Formato(this.textbox_result.Text);
            if (!(str == "dec"))
            {
                if (this.textbox_result.Text == "")
                {
                    this.textbox_result.Text = "0";
                    return;
                }
                if (this.l_historial.Text == "")
                    this.l_historial.Text = "unknown format!";
                else if (this.l_historial.Text == "unknown format!" || this.l_historial.Text == "too big value!" || (this.l_historial.Text == "less than zero!" || this.l_historial.Text == "div by zero!"))
                    this.l_historial.Text = "unknown format!";
                else if (this.l_historial.Text != "" && this.l_historial.Text.IndexOf(string.Format(" {0} ", (object)this.operation)) == this.l_historial.Text.Length - 3)
                    this.l_historial.Text = string.Format("{0} {1} unknown format!", (object)this.firstVar, (object)this.operation);
                else if (this.l_historial.Text != "" && this.l_historial.Text.IndexOf(string.Format(" {0} ", (object)this.operation)) != this.l_historial.Text.Length - 3)
                    this.l_historial.Text = "unknown format!";
                else if (this.l_historial.Text.Contains("unknown format!") || this.l_historial.Text.Contains("too big value!") || (this.l_historial.Text.Contains("less than zero!") || this.l_historial.Text.Contains("div by zero!")))
                    this.l_historial.Text = string.Format("{0} {1} unknown format!", (object)this.firstVar, (object)this.operation);
            }
            else
            {
                double n = double.Parse(textbox_result.Text, (IFormatProvider)Thread.CurrentThread.CurrentCulture.NumberFormat);
                bool interval = metodos.Intervalo(n);
                if (interval)
                {
                    this.secondVar = n;
                    string operation = this.operation;
                    if (!(operation == "+"))
                    {
                        if (!(operation == "-"))
                        {
                            if (!(operation == "×"))
                            {
                                if (operation == "÷")
                                {
                                    if (secondVar != 0.0)
                                    {
                                        this.lastOpe = this.firstVar / this.secondVar;
                                        this.l_historial.Text = string.Format("{0} {1} ({2})", (object)this.firstVar, (object)this.operation, (object)this.secondVar);
                                        this.lastOpe = Math.Round(this.lastOpe, 3);
                                        if (!metodos.Intervalo(this.lastOpe))
                                            this.l_historial.Text = "too big value!";
                                        else
                                            this.textbox_result.Text = Convert.ToString(this.lastOpe);
                                    }
                                    else if (this.l_historial.Text == "")
                                        this.l_historial.Text = "div by zero!";
                                    else if (this.l_historial.Text != "" && this.l_historial.Text.IndexOf(string.Format(" {0} ", (object)this.operation)) == this.l_historial.Text.Length - 3)
                                        this.l_historial.Text = "div by zero!";
                                }
                            }
                            else
                            {
                                this.lastOpe = this.firstVar * this.secondVar;
                                this.l_historial.Text = string.Format("{0} {1} ({2})", (object)this.firstVar, (object)this.operation, (object)this.secondVar);
                                this.lastOpe = Math.Round(this.lastOpe, 3);
                                if (!metodos.Intervalo(this.lastOpe))
                                    this.l_historial.Text = "too big value!";
                                else
                                    this.textbox_result.Text = Convert.ToString(this.lastOpe);
                            }
                        }
                        else
                        {
                            this.lastOpe = this.firstVar - this.secondVar;
                            this.l_historial.Text = string.Format("{0} {1} ({2})", (object)this.firstVar, (object)this.operation, (object)this.secondVar);
                            this.lastOpe = Math.Round(this.lastOpe, 3);
                            if (!metodos.Intervalo(this.lastOpe))
                                this.l_historial.Text = "too big value!";
                            else
                                this.textbox_result.Text = Convert.ToString(this.lastOpe);
                        }
                    }
                    else
                    {
                        this.lastOpe = this.firstVar + this.secondVar;
                        this.l_historial.Text = string.Format("{0} {1} ({2})", (object)this.firstVar, (object)this.operation, (object)this.secondVar);
                        this.lastOpe = Math.Round(this.lastOpe, 3);
                        if (!metodos.Intervalo(this.lastOpe))
                            this.l_historial.Text = "too big value!";
                        else
                            this.textbox_result.Text = Convert.ToString(this.lastOpe);
                    }
                }
                else if (this.l_historial.Text == "")
                    this.l_historial.Text = "too big value!";
                else if (this.l_historial.Text == "unknown format!" || this.l_historial.Text == "too big value!" || (this.l_historial.Text == "less than zero!" || this.l_historial.Text == "div by zero!"))
                    this.l_historial.Text = "too big value!";
                else if (this.l_historial.Text.Contains("unknown format!") || this.l_historial.Text.Contains("too big value!") || (this.l_historial.Text.Contains("less than zero!") || this.l_historial.Text.Contains("div by zero!")))
                    this.l_historial.Text = string.Format("{0} {1} too big value!", (object)this.firstVar, (object)this.operation);
                else if (this.l_historial.Text != "" && this.l_historial.Text.IndexOf(string.Format(" {0} ", (object)this.operation)) == this.l_historial.Text.Length - 3)
                    this.l_historial.Text = string.Format("{0} {1} too big value!", (object)this.firstVar, (object)this.operation);
                else if (this.l_historial.Text != "" && this.l_historial.Text.IndexOf(string.Format(" {0} ", (object)this.operation)) != this.l_historial.Text.Length - 3)
                    this.l_historial.Text = "too big value!";
            }
            this.CanClear_MainWindow = true;
        }

        public void b_borrarTODO_S_Click(object sender, EventArgs e)//clean the number entered 
        {
            if (!(this.l_historial.Text=="")&&!(this.textbox_result.Text==""))
            {
                if (this.l_historial.Text.Contains('('))
                {
                    b_borrarTODO.PerformClick();
                }
                else
                {
                    this.textbox_result.Text = "";
                }
            }
            else if ((this.l_historial.Text=="")&&!(this.textbox_result.Text==""))
            {
                textbox_result.Text = "";
            }
        }

        public void button_Neg_Click(object sender, EventArgs e)//Put positve or negative in from the number
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-EC");
            string str = metodos.Formato(this.textbox_result.Text);
            if (!(str == "dec"))
            {
                if (!(str == "nul"))
                    return;
                if (this.textbox_result.Text == "")
                    this.textbox_result.Text = "0";
                else if (this.l_historial.Text == "")
                    this.l_historial.Text = "unknown format!";
                else if (this.l_historial.Text == "unknown format!" || this.l_historial.Text == "too big value!" || (this.l_historial.Text == "less than zero!" || this.l_historial.Text == "div by zero!"))
                    this.l_historial.Text = "unknown format!";
                else if (this.l_historial.Text != "" && this.l_historial.Text.IndexOf(string.Format(" {0} ", (object)this.operation)) == this.l_historial.Text.Length - 3)//Si se inserta un solo signo menos
                    this.l_historial.Text = string.Format("{0} {1} unknown format!", (object)this.firstVar, (object)this.operation);
                else if (this.l_historial.Text.Contains("unknown format!") || this.l_historial.Text.Contains("too big value!") || (this.l_historial.Text.Contains("less than zero!") || this.l_historial.Text.Contains("div by zero!")))
                {
                    this.l_historial.Text = string.Format("{0} {1} unknown format!", (object)this.firstVar, (object)this.operation);
                }
                else
                {
                    if (!(this.l_historial.Text != "") || this.l_historial.Text.IndexOf(string.Format(" {0} ", (object)this.operation)) == this.l_historial.Text.Length - 3)
                        return;
                    this.l_historial.Text = string.Format("{0} {1} unknown format!", (object)this.lastOpe, (object)this.operation);
                }
            }
            else
            {
                double n = double.Parse(this.textbox_result.Text, (IFormatProvider)Thread.CurrentThread.CurrentCulture.NumberFormat);
                bool interval = metodos.Intervalo(n);
                if (interval)
                {
                    if (!interval)
                        return;
                    if (this.l_historial.Text == "unknown format!" || this.l_historial.Text == "too big value!" || (this.l_historial.Text == "less than zero!" || this.l_historial.Text == "div by zero!"))
                        this.l_historial.Text = "";
                    else if (this.l_historial.Text.Contains("unknown format!") || this.l_historial.Text.Contains("too big value!") || (this.l_historial.Text.Contains("less than zero!") || this.l_historial.Text.Contains("div by zero!")))
                        this.l_historial.Text = string.Format("{0} {1} ", (object)this.firstVar, (object)this.operation);
                    if (this.l_historial.Text == "")
                        this.l_historial.Text = "";
                    else if (this.l_historial.Text != "" && this.l_historial.Text.IndexOf(string.Format(" {0} ", (object)this.operation)) != this.l_historial.Text.Length - 3)
                        this.l_historial.Text = "";
                    if (n == 0.0)
                        return;
                    if (this.textbox_result.Text[0] == '-')
                        this.textbox_result.Text = this.textbox_result.Text.Remove(0, 1);
                    else if (metodos.Intervalo(Convert.ToDouble("-" + this.textbox_result.Text, (IFormatProvider)Thread.CurrentThread.CurrentCulture.NumberFormat)))
                    {
                        this.textbox_result.Text = "-" + this.textbox_result.Text;
                    }
                    else
                    {
                        if (this.l_historial.Text == "")
                            this.l_historial.Text = "too big value!";
                        if (this.l_historial.Text != "" && this.l_historial.Text.IndexOf(string.Format(" {0} ", (object)this.operation)) == this.l_historial.Text.Length - 3)
                        {
                            this.l_historial.Text = string.Format("{0} {1} too big value!", (object)this.firstVar, (object)this.operation);
                        }
                        else
                        {
                            if (!(this.l_historial.Text != "") || this.l_historial.Text.IndexOf(string.Format(" {0} ", (object)this.operation)) == this.l_historial.Text.Length - 3)
                                return;
                            this.l_historial.Text = "too big value!";
                        }
                    }
                }
                else if (this.l_historial.Text == "")
                    this.l_historial.Text = "too big value!";
                else if (this.l_historial.Text == "unknown format!" || this.l_historial.Text == "too big value!" || (this.l_historial.Text == "less than zero!" || this.l_historial.Text == "div by zero!"))
                    this.l_historial.Text = "too big value!";
                else if (this.l_historial.Text != "" && this.l_historial.Text.IndexOf(string.Format(" {0} ", (object)this.operation)) == this.l_historial.Text.Length - 3)
                    this.l_historial.Text = string.Format("{0} {1} too big value!", (object)this.firstVar, (object)this.operation);
                else if (this.l_historial.Text.Contains("unknown format!") || this.l_historial.Text.Contains("too big value!") || (this.l_historial.Text.Contains("less than zero!") || this.l_historial.Text.Contains("div by zero!")))
                {
                    this.l_historial.Text = string.Format("{0} {1} too big value!", (object)this.firstVar, (object)this.operation);
                }
                else
                {
                    if (!(this.l_historial.Text != "") || this.l_historial.Text.IndexOf(string.Format(" {0} ", (object)this.operation)) == this.l_historial.Text.Length - 3)
                        return;
                    this.l_historial.Text = "too big value!";
                }
            }
        }

        public void button_OpMyVariant_Click(object sender, EventArgs e)//Operations accouding my variant
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-EC");
            Button button = (Button)sender;
            string text1 = this.textbox_result.Text;
            string str = metodos.Formato(this.textbox_result.Text);
            if (!(str == "dec"))
            {
                if (!(str == "nul"))
                    return;
                if (this.textbox_result.Text == "")
                    this.textbox_result.Text = "0";
                else if (this.l_historial.Text == "" || this.l_historial.Text != "" && this.l_historial.Text.IndexOf(string.Format(" {0} ", (object)this.operation)) != this.l_historial.Text.Length - 3)
                {
                    this.l_historial.Text = "unknown format!";
                }
                else
                {
                    if (!(this.l_historial.Text != "") || this.l_historial.Text.IndexOf(string.Format(" {0} ", (object)this.operation)) != this.l_historial.Text.Length - 3)
                        return;
                    this.l_historial.Text = string.Format("{0} {1} unknown format!", (object)this.firstVar, (object)button.Text);
                }
            }
            else
            {
                double num = double.Parse(this.textbox_result.Text, (IFormatProvider)Thread.CurrentThread.CurrentCulture.NumberFormat);
                bool interval = metodos.Intervalo(num);
                if (interval)
                {
                    if (!interval)
                        return;
                    if (this.l_historial.Text == "" || this.l_historial.Text != "" && this.l_historial.Text.IndexOf(string.Format(" {0} ", (object)this.operation)) != this.l_historial.Text.Length - 3)
                    {
                        string text2 = button.Text;
                        if (!(text2 == "sqrt"))
                        {
                            if (!(text2 == "1/x"))
                            {
                                if (!(text2 == "x^2"))
                                {
                                    if (text2=="x!")
                                    {
                                        if (((decimal)num - Decimal.Truncate((decimal)num)) != 0)
                                        {
                                            this.secondVar = Math.Pow(num / Math.E, num) * Math.Sqrt(Math.PI*(2*(num)+1/3.0));//https://www.quora.com/How-do-I-calculate-decimal-factorials-like-1-7
                                            this.l_historial.Text = string.Format("({0})!", (object)text1);
                                        }
                                        else
                                        {
                                            this.secondVar = MathNet.Numerics.SpecialFunctions.Factorial((int)num);
                                            this.l_historial.Text = string.Format("({0})!", (object)text1);
                                        }                                        
                                    }                                    
                                }
                                else 
                                {
                                    this.secondVar = Math.Pow(num,2);
                                    this.l_historial.Text = string.Format("({0})^2", (object)text1);
                                }
                            }
                            else if (num != 0.0)
                            {
                                this.secondVar = 1.0 / num;
                                this.l_historial.Text = string.Format("1/({0})", (object)text1);
                            }
                            else
                            {
                                this.l_historial.Text = "div by zero!";
                                return;
                            }
                        }
                        else if (num >= 0.0)
                        {
                            this.secondVar = Math.Sqrt(num);
                            this.l_historial.Text = string.Format("sqrt({0})", (object)text1);
                        }
                        else
                        {
                            this.l_historial.Text = "less than zero!";
                            return;
                        }
                        this.secondVar = Math.Round(this.secondVar, 3);
                        if (!metodos.Intervalo(this.secondVar))
                        {
                            this.l_historial.Text = "too big value!";
                        }
                        else
                        {
                            this.textbox_result.Text = Convert.ToString(this.secondVar);
                            this.lastOpe = this.secondVar;
                        }
                    }
                    else
                    {
                        if (!(this.l_historial.Text != "") || this.l_historial.Text.IndexOf(string.Format(" {0} ", (object)this.operation)) != this.l_historial.Text.Length - 3)
                            return;
                        string text2 = button.Text;
                        if (!(text2 == "sqrt"))
                        {
                            if (!(text2 == "1/x"))
                            {

                                if (!(text2=="x^2"))
                                {

                                    if (!(text2 == "x!"))
                                        return;

                                    if (((decimal)num - Decimal.Truncate((decimal)num)) != 0)
                                    {
                                        if (!metodos.Intervalo(this.secondVar))
                                        {
                                            this.l_historial.Text = "too big value!";
                                        }
                                        else
                                        {
                                            this.secondVar = Math.Pow(num / Math.E, num) * Math.Sqrt(Math.PI * (2 * (num) + 1 / 3.0));//https://www.quora.com/How-do-I-calculate-decimal-factorials-like-1-7
                                            this.textbox_result.Text = Convert.ToString(this.secondVar);
                                            this.l_historial.Text = string.Format("{0} {1} ", (object)this.firstVar, (object)this.operation);
                                        }
                                    }
                                    else
                                    {
                                        if (!metodos.Intervalo(this.secondVar))
                                        {
                                            this.l_historial.Text = "too big value!";
                                        }
                                        else
                                        {
                                            this.secondVar = MathNet.Numerics.SpecialFunctions.Factorial((int)num);
                                            this.textbox_result.Text = Convert.ToString(this.secondVar);
                                            this.l_historial.Text = string.Format("{0} {1} ", (object)this.firstVar, (object)this.operation);
                                        }
                                    }
                                }
                                else
                                {
                                    this.secondVar = Math.Pow(num, 2);
                                    if (!metodos.Intervalo(this.secondVar))
                                    {
                                        this.l_historial.Text = "too big value!";
                                    }
                                    else
                                    {
                                        this.textbox_result.Text = Convert.ToString(this.secondVar);
                                        this.l_historial.Text = string.Format("{0} {1} ", (object)this.firstVar, (object)this.operation);
                                    }                                
                                }                                
                            }
                            else if (num != 0.0)
                            {
                                this.secondVar = 1.0 / num;
                                this.secondVar = Math.Round(this.secondVar, 3);
                                if (!metodos.Intervalo(this.secondVar))
                                {
                                    this.l_historial.Text = "too big value!";
                                }
                                else
                                {
                                    this.textbox_result.Text = Convert.ToString(this.secondVar);
                                    this.l_historial.Text = string.Format("{0} {1} ", (object)this.firstVar, (object)this.operation);
                                }
                            }
                            else
                                this.l_historial.Text = "div by zero!";
                        }
                        else if (num >= 0.0)
                        {
                            this.secondVar = Math.Sqrt(num);
                            this.secondVar = Math.Round(this.secondVar, 3);
                            if (!metodos.Intervalo(this.secondVar))
                            {
                                this.l_historial.Text = "too big value!";
                            }
                            else
                            {
                                this.textbox_result.Text = Convert.ToString(this.secondVar);
                                this.l_historial.Text = string.Format("{0} {1} ", (object)this.firstVar, (object)this.operation);
                            }
                        }
                        else
                            this.l_historial.Text = "less than zero!";
                    }
                }
                else if (this.l_historial.Text == "" || this.l_historial.Text != "" && this.l_historial.Text.IndexOf(string.Format(" {0} ", (object)this.operation)) != this.l_historial.Text.Length - 3)
                {
                    this.l_historial.Text = "too big value!";
                }
                else
                {
                    if (!(this.l_historial.Text != "") || this.l_historial.Text.IndexOf(string.Format(" {0} ", (object)this.operation)) != this.l_historial.Text.Length - 3)
                        return;
                    this.l_historial.Text = string.Format("{0} {1} too big value!", (object)this.firstVar, (object)button.Text);
                }
            }
        }

        private void textbox_result_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
