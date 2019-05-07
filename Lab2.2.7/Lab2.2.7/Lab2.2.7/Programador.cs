using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2._2._7
{
    public class Programador
    {
        public int id;
        public String familia;
        public String nombre;
        public String snombre;
        public String fnacimiento;
        public String lenguajeP;
        public String posicion;
        public int exLaboral;
        public double salario;
        public Programador(int _id,String _familia,String _nombre, String _snombre,String _fnacimiento,String _lenguajeP,
                            String _posicion,int _exLaboral,double _salario)
        {
            id = _id;
            familia = _familia;
            nombre = _nombre;
            snombre = _snombre;
            fnacimiento = _fnacimiento;
            lenguajeP = _lenguajeP;
            posicion = _posicion;
            exLaboral = _exLaboral;
            salario = _salario;
        }
    }
}
