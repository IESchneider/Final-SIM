using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_SIM.Entidades
{
    public class Fila
    {
        // Atributos del vector de estado para cada fila. Se utilizarán dos objetos filas.


        // Evento, Reloj y llegadas.
        public string Evento { get; set; }
        public double Reloj { get; set; }
        public double RND1_llegada { get; set; }
        public double RND2_llegada { get; set; }
        public double tiempoEntreLlegadas { get; set; }
        public double proxLlegada { get; set; }
        public double RND_tipo1Servicio { get; set; }
        public string tipo1Servicio { get; set; }
        public double RND_tipo2Servicio { get; set; }
        public string tipo2Servicio { get; set; }


        // Fin atención y empleados Surtidor.
        public double RND_finAtencionSurt { get; set; }
        public double tiempoAtencionSurt { get; set; }
        public double Emple1_finAt_Surt { get; set; }
        public double Emple2_finAt_Surt { get; set; }
        public double Emple3_finAt_Surt { get; set; }
        public Estado Emple1_Estado_Surt { get; set; }
        public Estado Emple2_Estado_Surt { get; set; }
        public Estado Emple3_Estado_Surt { get; set; }
        public int Cola_Surtidor { get; set; }


        // Fin atención y empleados Gomeria.
        public double RND_finAtencionGome { get; set; }
        public double tiempoAtencionGome { get; set; }
        public double Emple1_finAt_Gome { get; set; }
        public double Emple2_finAt_Gome { get; set; }
        public Estado Emple1_Estado_Gome { get; set; }
        public Estado Emple2_Estado_Gome { get; set; }
        public int Cola_Gomeria { get; set; }


        // Fin atención y empleados Ventas Accesorios.
        public double RND_finAtencionVent { get; set; }
        public double tiempoAtencionVent { get; set; }
        public double Emple_finAt_Vent { get; set; }
        public Estado Emple_Estado_Vent { get; set; }
        public int Cola_VentasAccesorios { get; set; }


        // Metricas.
        public int CantClientesVanSinComprar { get; set; }

        public int CantidadTotalClientes { get; set; }

        public int ColaMax_Surt { get; set; }
        public int ColaMax_Gome { get; set; }
        public int ColaMax_Vent { get; set; }


        // Lista de todas las personas.
        public List<Temporal> Persona { get; set; } = new List<Temporal>();

        public void Reiniciar()
        {
            Evento = null;
            Reloj = 0.0;

            RND1_llegada = 0.0;
            RND2_llegada = 0.0;
            tiempoEntreLlegadas = 0.0;
            proxLlegada = 0.0;

            RND_tipo1Servicio = 0.0;
            tipo1Servicio = "";
            RND_tipo2Servicio = 0.0;
            tipo2Servicio = "";

            RND_finAtencionSurt = 0.0;
            tiempoAtencionSurt = 0.0;

            Emple1_finAt_Surt = 0.0;
            Emple1_Estado_Surt = null;

            Emple2_finAt_Surt = 0.0;
            Emple2_Estado_Surt = null;

            Emple3_finAt_Surt = 0.0;
            Emple3_Estado_Surt = null;

            Cola_Surtidor = 0;

            RND_finAtencionGome = 0.0;
            tiempoAtencionGome = 0.0;

            Emple1_finAt_Gome = 0.0;
            Emple1_Estado_Gome = null;

            Emple2_finAt_Gome = 0.0;
            Emple2_Estado_Gome = null;

            Cola_Gomeria = 0;

            RND_finAtencionVent = 0.0;
            tiempoAtencionVent = 0.0;

            Emple_finAt_Vent = 0.0;
            Emple_Estado_Vent = null;

            Cola_VentasAccesorios = 0;

            CantClientesVanSinComprar = 0;

            CantidadTotalClientes = 0;

            ColaMax_Surt = 0;
            ColaMax_Gome = 0;
            ColaMax_Vent = 0;

            Persona.Clear();
        }
    }
}
