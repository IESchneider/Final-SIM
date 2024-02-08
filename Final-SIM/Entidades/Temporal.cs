using Final_SIM.Entidades.Estados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_SIM.Entidades
{
    public class Temporal
    {
        public double Numero { get; set; }
        public double OldNumero { get; set; }
        public string Tipo1 { get; set; }
        public string Tipo2 { get; set; }
        public string OtraAtencion { get; set; }
        public string TipoResumido { get; set; }
        public Estado Estado { get; set; }
        public int EnFilaNumero { get; set; }
        public Servidor SiendoAtendidoPor { get; set; }
        public double HoraIngreso { get; set; }

        public Temporal CopiarCliente(Temporal cliente)
        {
            Temporal clienteCopiado = new Temporal();

            clienteCopiado.Numero = cliente.Numero;
            clienteCopiado.OldNumero = cliente.OldNumero;

            clienteCopiado.Tipo1 = cliente.Tipo1;
            clienteCopiado.Tipo2 = cliente.Tipo2;

            clienteCopiado.OtraAtencion = cliente.OtraAtencion;

            clienteCopiado.TipoResumido = cliente.TipoResumido;

            clienteCopiado.Estado = cliente.Estado;

            clienteCopiado.EnFilaNumero = cliente.EnFilaNumero;

            clienteCopiado.SiendoAtendidoPor = cliente.SiendoAtendidoPor;

            clienteCopiado.HoraIngreso = cliente.HoraIngreso;

            return clienteCopiado;
        }

        public Temporal DestruirCliente(Temporal cliente)
        {
            Temporal clienteDestruido = new Temporal();

            clienteDestruido.Numero = cliente.Numero;
            clienteDestruido.OldNumero = cliente.OldNumero;

            clienteDestruido.Tipo1 = cliente.Tipo1;
            clienteDestruido.Tipo2 = cliente.Tipo2;

            clienteDestruido.OtraAtencion = cliente.OtraAtencion;

            clienteDestruido.TipoResumido = cliente.TipoResumido;

            clienteDestruido.Estado = new Destruido();

            clienteDestruido.EnFilaNumero = cliente.EnFilaNumero;

            clienteDestruido.SiendoAtendidoPor = cliente.SiendoAtendidoPor;

            clienteDestruido.HoraIngreso = cliente.HoraIngreso;

            cliente.Estado = new Destruido();
            return clienteDestruido;
        }
    }
}
