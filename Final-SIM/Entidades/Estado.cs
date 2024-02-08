using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_SIM.Entidades
{
    public class Estado
    {
        public string Nombre { get; }
        public bool Libre { get; }
        public bool Ocupado { get; }
        public bool EsperandoSurtidor { get; }
        public bool EsperandoGomeria { get; }
        public bool EsperandoVentas { get; }
        public bool SiendoAtendidoSurt { get; }
        public bool SiendoAtendidoGome { get; }
        public bool SiendoAtendidoVent { get; }
        public bool Destruido { get; }

        protected Estado(string nombre, bool libre, bool ocupado, bool esperandoSurtidor, bool esperandoGomeria,
                         bool esperandoVentas, bool siendoAtendidoSurt, bool siendoAtendidoGome, bool siendoAtendidoVent, bool destruido)
        {
            Nombre = nombre;

            Libre = libre;
            Ocupado = ocupado;

            EsperandoSurtidor = esperandoSurtidor;
            EsperandoGomeria = esperandoGomeria;
            EsperandoVentas = esperandoVentas;

            SiendoAtendidoSurt = siendoAtendidoSurt;
            SiendoAtendidoGome = siendoAtendidoGome;
            SiendoAtendidoVent = siendoAtendidoVent;

            Destruido = destruido;
        }
    }
}
