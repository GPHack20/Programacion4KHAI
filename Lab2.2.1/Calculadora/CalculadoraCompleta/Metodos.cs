using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Threading;
using System.Globalization;

namespace CalculadoraCompleta
{
    public class Metodos
    {
        public bool Intervalo(double n)//Interval of numbers
        {
            return n >= -400000.000 && n <= 900000.000;
        }
        public string Formato(string n)//Check the formart
        {
            Regex reg_dec_nul = new Regex("^(-)?[0-9]+([.,][0-9]{1,3})?$");
            string input = n;
            return reg_dec_nul.IsMatch(n) ? "dec" : "nul";
        }       
    }
}
