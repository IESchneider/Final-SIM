using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_SIM.Entidades
{
    public class Servidor
    {
        public string Nombre { get; set; }

        public bool Empleado1_Surt { get; set; }
        public bool Empleado2_Surt { get; set; }
        public bool Empleado3_Surt { get; set; }

        public bool Empleado1_Gome { get; set; }
        public bool Empleado2_Gome { get; set; }

        public bool Empleado_Vent { get; set; }

        public Estado Estado { get; set; }
    }
}
