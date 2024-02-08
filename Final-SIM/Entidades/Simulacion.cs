using Final_SIM.Entidades.Estados;
using Final_SIM.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_SIM.Entidades
{
    public class Simulacion
    {
        private readonly Random RND = new Random();

        public double VariableAleatoriaNConvolucion(double media, double desviacion)
        {
            double sumaConvolucion = 0;
            double variableAleatoria;
            double random;

            for (int x = 0; x < 12; x++)
            {
                random = GenerarRND();
                sumaConvolucion += random;
            }

            variableAleatoria = ((sumaConvolucion - 6) * desviacion) + media;

            if (variableAleatoria > 0)
            {
                return Math.Round(variableAleatoria, 2);
            }

            return VariableAleatoriaNConvolucion(media, desviacion);
        }

        public double VariableAleatoriaUniforme(double minimo, double maximo, double random)
        {
            double variableAleatoria = minimo + (random * (maximo - minimo));

            return Math.Round(variableAleatoria, 2);
        }

        public double VariableAleatoriaExponencial(double media, double random)
        {
            double variableAleatoria;

            variableAleatoria = -media * Math.Log(1 - random);

            return Math.Round(variableAleatoria, 2);
        }

        public string CalcularTipoServicio(double random, double probSurtidor, double probGomeria, double probVentasAcce)
        {
            if (random >= 0 && random < probSurtidor)
            {
                return "Surtidor";
            }
            else if (random >= probSurtidor && random < (probSurtidor + probGomeria))
            {
                return "Gomeria";
            }
            return "Ventas Accesorios";
        }

        public double GenerarRND()
        {
            double random;

            do { random = Math.Round(RND.NextDouble(), 4); }
            while (random == 0 || random == 1);

            return random;
        }

        public Temporal proximoCliente(List<Temporal> personas)
        {
            Temporal cliente = new Temporal();
            cliente.Numero = 9999999;
            EsperandoSurtidor surtidor = new EsperandoSurtidor();
            EsperandoGomeria gomeria = new EsperandoGomeria();
            EsperandoVentas ventas = new EsperandoVentas();

            cliente = personas.OrderBy(o => o.Numero).Where(o => o.Estado.Nombre == surtidor.Nombre || o.Estado.Nombre == gomeria.Nombre || o.Estado.Nombre == ventas.Nombre).FirstOrDefault();
            personas.OrderBy(o => o.Numero).Where(o => o.Estado.Nombre == surtidor.Nombre || o.Estado.Nombre == gomeria.Nombre || o.Estado.Nombre == ventas.Nombre).First().Estado = new Destruido();
            return cliente;

        }


        // Nuevos atributos globales

        readonly Servidor Empleado1_Surt = new Servidor();
        readonly Servidor Empleado2_Surt = new Servidor();
        readonly Servidor Empleado3_Surt = new Servidor();

        readonly Servidor Empleado1_Gome = new Servidor();
        readonly Servidor Empleado2_Gome = new Servidor();

        readonly Servidor Empleado_Vent = new Servidor();


        // Atributos generales para simulaciones.

        private Fila fila1 = new Fila();
        private Fila fila2 = new Fila();

        public int FilaDesde { get; set; }
        public int FilaHasta { get; set; }

        public double MediaLlegada { get; set; }
        public double DesviacionLlegada { get; set; }

        public double MinTiempoSurt { get; set; }
        public double MaxTiempoSurt { get; set; }

        public double MinTiempoGome { get; set; }
        public double MaxTiempoGome { get; set; }

        public double MinTiempoVent { get; set; }
        public double MaxTiempoVent { get; set; }

        public double ProbSurtidor1 { get; set; }
        public double ProbGomeria1 { get; set; }
        public double ProbVentasAcce1 { get; set; }

        public double ProbSurtidor2 { get; set; }
        public double ProbGomeria2 { get; set; }
        public double ProbVentasAcce2 { get; set; }

        //public List<double> tiemposFinLectura = new List<double> { 0 };


        public FormSimulacion FormularioSimulacion { get; set; }
        public HashSet<int> iteracionesGrilla { get; set; }
        public DataGridView Grilla { get; set; }

        //readonly LogSimulacion log = new LogSimulacion();

        public int CantidadSimulaciones { get; set; } = 0;
        public int NumeroSimulacionActual { get; set; } = 0;
        public int numeroCliente { get; set; } = 0;
        public int CantClientesVanSinComprar { get; set; } = 0;
        public int CantidadTotalClientes { get; set; } = 0;

        private List<Temporal> TodosLosClientes = new List<Temporal>();


        // Diccionarios para guardar estados inmutables de los servidores.

        readonly Dictionary<string, Libre> estadosLibre = new Dictionary<string, Libre>();
        readonly Dictionary<string, Ocupado> estadosOcupado = new Dictionary<string, Ocupado>();


        // Estados inmutables para los clientes que se van generando.

        readonly EsperandoSurtidor EsperandoSurtidor = new EsperandoSurtidor();
        readonly EsperandoGomeria EsperandoGomeria = new EsperandoGomeria();
        readonly EsperandoVentas EsperandoVentas = new EsperandoVentas();

        readonly SiendoAtendidoSurt SiendoAtendidoSurt = new SiendoAtendidoSurt();
        readonly SiendoAtendidoGome SiendoAtendidoGome = new SiendoAtendidoGome();
        readonly SiendoAtendidoVent SiendoAtendidoVent = new SiendoAtendidoVent();

        readonly Destruido Destruido = new Destruido();

        public void Simular()
        {
            // Obtener la grilla y prepararla (mejora rendimiento).

            DataGridView grilla = FormularioSimulacion.DevolverGrilla();
            PrepararGrilla(grilla);
            this.Grilla = grilla;

            // Obtener iteraciones a agregar en un HashSet para solo agregar en la grilla los valores deseados.

            iteracionesGrilla = IteracionesParaGrilla();

            // Inicializar el booleano de los servidores para que sepamos cual es cual.

            Empleado1_Surt.Empleado1_Surt = true;
            Empleado1_Surt.Nombre = "Empleado1 Surt";
            Empleado2_Surt.Empleado2_Surt = true;
            Empleado2_Surt.Nombre = "Empleado2 Surt";
            Empleado3_Surt.Empleado3_Surt = true;
            Empleado3_Surt.Nombre = "Empleado3 Surt";

            Empleado1_Gome.Empleado1_Gome = true;
            Empleado1_Gome.Nombre = "Empleado1 Gome";
            Empleado2_Gome.Empleado2_Gome = true;
            Empleado2_Gome.Nombre = "Empleado2 Gome";

            Empleado_Vent.Empleado_Vent = true;
            Empleado_Vent.Nombre = "Empleado Vent";

            // Inicializar diccionarios de estados inmutables, son 2 estados por servidor.

            string[] nombresServidores = { "Empleado1 Surt", "Empleado2 Surt", "Empleado3 Surt",
                                           "Empleado1 Gome", "Empleado2 Gome", "Empleado Vent" };

            foreach (string nombre in nombresServidores)
            {
                Libre Libre = new Libre();
                Ocupado Ocupado = new Ocupado();

                estadosLibre.Add(nombre, Libre);
                estadosOcupado.Add(nombre, Ocupado);
            }

            // Cargar valores e iniciar simulación.

            Inicializar();

            // Empezar simulación para filas sucesivas.

            CargarFilas(fila1, fila2, iteracionesGrilla);

            // Mostrar formulario.

            MostrarFormulario(FormularioSimulacion, Grilla);
        }

        private void Inicializar()
        {
            // Volver a las filas a los valores por defecto.

            fila1.Reiniciar();
            fila2.Reiniciar();

            // Inicializar valores de filas.

            fila1.Evento = "Inicialización";

            // Obtiene los randoms y las próximas llegadas para la fila de inicialización.

            //fila1.RND1_llegada = GenerarRND();
            //fila1.RND2_llegada = GenerarRND();
            fila1.tiempoEntreLlegadas = VariableAleatoriaNConvolucion(MediaLlegada, DesviacionLlegada);
            fila1.proxLlegada = fila1.tiempoEntreLlegadas;

            // Obtiene el primer próximo reloj solo para mostrarlo en la grilla de otro color.

            double proximoReloj = fila1.tiempoEntreLlegadas;

            // Establece los estados a los servidores, los estados son inmutables.

            fila1.Emple1_Estado_Surt = estadosLibre["Empleado1 Surt"];
            fila1.Emple2_Estado_Surt = estadosLibre["Empleado2 Surt"];
            fila1.Emple3_Estado_Surt = estadosLibre["Empleado3 Surt"];

            fila1.Emple1_Estado_Gome = estadosLibre["Empleado1 Gome"];
            fila1.Emple2_Estado_Gome = estadosLibre["Empleado2 Gome"];

            fila1.Emple_Estado_Vent = estadosLibre["Empleado Vent"];

            // Inicializar eventos.

            // Aumentar uno al numero de simulaciones (la inicialización se toma como una simulación).
            if (NumeroSimulacionActual > 0)
            {
                ++NumeroSimulacionActual;
            }

            // Crear un cliente temporal para la inicialización.

            Temporal clienteFalsoInicializacion = new Temporal();

            //clienteFalsoInicializacion.Tipo = "Inicializacion";

            clienteFalsoInicializacion.EnFilaNumero = NumeroSimulacionActual;
            clienteFalsoInicializacion.Estado = Destruido;
            TodosLosClientes.Add(clienteFalsoInicializacion);

            // Cargar inicialización solo si es la primera o alguna dentro de la selección del usuario.

            if (NumeroSimulacionActual == 0 || iteracionesGrilla.Contains(NumeroSimulacionActual))
            {
                AgregarFilaEnGrilla(fila1, proximoReloj);
            }
        }

        private (double, string) ObtenerProximoReloj()
        {
            // Crea una matriz entre todos los posibles eventos. Luego busca en esa matriz cual es el menor tiempo para saber qué evento sigue.

            object[,] posiblesProximoReloj =
                     {
                      { fila1.proxLlegada, "Llegada_Persona"},

                      { fila1.Emple1_finAt_Surt, "FinAt_Emple1_Surt" },
                      { fila1.Emple2_finAt_Surt, "FinAt_Emple2_Surt" },
                      { fila1.Emple3_finAt_Surt, "FinAt_Emple3_Surt" },

                      { fila1.Emple1_finAt_Gome, "FinAt_Emple1_Gome" },
                      { fila1.Emple2_finAt_Gome, "FinAt_Emple2_Gome" },

                      { fila1.Emple_finAt_Vent, "FinAt_Emple_Vent" },
                     };

            double proximoReloj = (double)posiblesProximoReloj[0, 0];
            string evento = (string)posiblesProximoReloj[0, 1];

            for (int j = 1; j < posiblesProximoReloj.GetLength(0); j++)
            {
                double valorActual = (double)posiblesProximoReloj[j, 0];

                if (valorActual < proximoReloj && valorActual != 0 && proximoReloj != 0)
                {
                    proximoReloj = valorActual;
                    evento = (string)posiblesProximoReloj[j, 1];
                }
            }
            /*
            foreach (double numero in tiemposFinLectura)
            {
                if (numero < proximoReloj && numero != 0 && proximoReloj != 0)
                {
                    proximoReloj = numero;
                    evento = (string)posiblesProximoReloj[3, 1];
                }
            }
            */
            return (proximoReloj, evento);
        }

        private void CargarFilas(Fila fila1, Fila fila2, HashSet<int> iteracionesGrilla)
        {

            // Suspender layout para mejorar rendimiento de grilla.

            Grilla.SuspendLayout();

            // Cargar filas y actualizar la anterior.

            for (int i = 1; i <= CantidadSimulaciones; ++i)
            {

                ++NumeroSimulacionActual;

                // Obtener la tupla del próximo reloj y evento llamando a la función ObtenerProximoReloj().

                var resultadoProximoReloj = ObtenerProximoReloj();
                double proximoReloj = resultadoProximoReloj.Item1;
                string evento = resultadoProximoReloj.Item2;

                
                // En caso de ser el próximo reloj el valor '9999' significa que se debe cambiar de día.
                /*
                if (proximoReloj == 9999)
                {
                    //++DiaSimulacion;

                    // Generar una nueva fila de inicialización y obtener el próximo reloj.

                    Inicializar();

                    resultadoProximoReloj = ObtenerProximoReloj();
                    proximoReloj = resultadoProximoReloj.Item1;
                    evento = resultadoProximoReloj.Item2;
                }
                */

                fila2.Evento = evento;
                fila2.Reloj = proximoReloj;

                fila2.Cola_Surtidor = fila1.Cola_Surtidor;
                fila2.Cola_Gomeria = fila1.Cola_Gomeria;
                fila2.Cola_VentasAccesorios = fila1.Cola_VentasAccesorios;

                int resultado = Math.Max(fila2.Cola_Surtidor, fila1.ColaMax_Surt);
                fila2.ColaMax_Surt = resultado;

                int resultado2 = Math.Max(fila2.Cola_VentasAccesorios, fila1.ColaMax_Vent);
                fila2.ColaMax_Vent = resultado2;

                int resultado3 = Math.Max(fila2.Cola_Gomeria, fila1.ColaMax_Gome);
                fila2.ColaMax_Gome = resultado3;


                switch (evento)
                {
                    case "Llegada_Persona":

                        ++numeroCliente;
                        ++CantidadTotalClientes;

                        // Generar cliente temporal.

                        Temporal cliente = new Temporal();
                        cliente.Numero = numeroCliente;
                        cliente.EnFilaNumero = NumeroSimulacionActual;

                        //cliente.Numero = ++CantidadClientesIM;

                        cliente.HoraIngreso = fila2.Reloj;
                        fila2.RND_tipo1Servicio = 0;
                        fila2.tipo1Servicio = "";
                        fila2.RND_tipo2Servicio = 0;
                        fila2.tipo2Servicio = "";

                        // Obtener RNDs y Llegadas.

                        fila2.tiempoEntreLlegadas = VariableAleatoriaNConvolucion(MediaLlegada, DesviacionLlegada);
                        fila2.proxLlegada = fila2.Reloj + fila2.tiempoEntreLlegadas;

                        // Obtener RNDs y tipo servicio.
                        if (cliente.OtraAtencion != "SI")
                        {
                            fila2.RND_tipo1Servicio = GenerarRND();
                            fila2.tipo1Servicio = CalcularTipoServicio(fila2.RND_tipo1Servicio, ProbSurtidor1, ProbGomeria1, ProbVentasAcce1);
                            cliente.Tipo1 = fila2.tipo1Servicio;

                            if (cliente.Tipo1 == "Surtidor")
                            {
                                cliente.Estado = EsperandoSurtidor;
                            }
                            if (cliente.Tipo1 == "Gomeria")
                            {
                                cliente.Estado = EsperandoGomeria;
                            }
                            if (cliente.Tipo1 == "Ventas Accesorios")
                            {
                                cliente.Estado = EsperandoVentas;
                            }
                        }

                        if (!(fila1.Cola_Surtidor > 5 && fila2.tipo1Servicio == "Surtidor"))
                        {
                            switch (cliente.Tipo1)
                            {
                                case "Surtidor":

                                    if (fila1.Cola_Surtidor > 0)
                                    {
                                        // Si la cola es mayor que cero, implica que el servidor está ocupado y por ende se debe incrementar la cola.

                                        fila2.Cola_Surtidor = fila1.Cola_Surtidor + 1;

                                        fila2.Emple1_finAt_Surt = fila1.Emple1_finAt_Surt;
                                        fila2.Emple2_finAt_Surt = fila1.Emple2_finAt_Surt;
                                        fila2.Emple3_finAt_Surt = fila1.Emple3_finAt_Surt;

                                        fila2.Emple1_Estado_Surt = fila1.Emple1_Estado_Surt;
                                        fila2.Emple2_Estado_Surt = fila1.Emple2_Estado_Surt;
                                        fila2.Emple3_Estado_Surt = fila1.Emple3_Estado_Surt;

                                        fila2.Persona.Add(cliente);
                                        cliente.EnFilaNumero = NumeroSimulacionActual;

                                        //fila2.tipo1Servicio = CalcularTipoServicio(fila2.RND_tipo1Servicio, ProbSurtidor, ProbGomeria, ProbVentasAcce);
                                        //cliente.Tipo1 = fila2.tipo1Servicio;

                                        cliente.Estado = EsperandoSurtidor;
                                    }

                                    if (fila1.Cola_Surtidor == 0)
                                    {
                                        if (fila1.Emple1_Estado_Surt.Libre)
                                        {
                                            //cliente.Tipo1 = CalcularTipoServicio(fila2.RND_tipo1Servicio, ProbSurtidor, ProbGomeria, ProbVentasAcce);

                                            fila2.RND_finAtencionSurt = GenerarRND();

                                            fila2.tiempoAtencionSurt = VariableAleatoriaUniforme(MinTiempoSurt, MaxTiempoSurt, fila2.RND_finAtencionSurt);
                                            fila2.Emple1_finAt_Surt = fila2.Reloj + fila2.tiempoAtencionSurt;
                                            fila2.Emple1_Estado_Surt = estadosOcupado["Empleado1 Surt"];

                                            fila2.Emple2_finAt_Surt = fila1.Emple2_finAt_Surt;
                                            fila2.Emple2_Estado_Surt = fila1.Emple2_Estado_Surt;

                                            fila2.Emple3_finAt_Surt = fila1.Emple3_finAt_Surt;
                                            fila2.Emple3_Estado_Surt = fila1.Emple3_Estado_Surt;

                                            cliente.Estado = SiendoAtendidoSurt;
                                            cliente.SiendoAtendidoPor = Empleado1_Surt;
                                            fila2.Persona.Add(cliente);

                                            cliente.EnFilaNumero = NumeroSimulacionActual;
                                        }
                                        else if (fila1.Emple2_Estado_Surt.Libre)
                                        {
                                            //cliente.Tipo1 = CalcularTipoServicio(fila2.RND_tipo1Servicio, ProbSurtidor, ProbGomeria, ProbVentasAcce);

                                            fila2.RND_finAtencionSurt = GenerarRND();

                                            fila2.tiempoAtencionSurt = VariableAleatoriaUniforme(MinTiempoSurt, MaxTiempoSurt, fila2.RND_finAtencionSurt);
                                            fila2.Emple2_finAt_Surt = fila2.Reloj + fila2.tiempoAtencionSurt;
                                            fila2.Emple2_Estado_Surt = estadosOcupado["Empleado2 Surt"];

                                            fila2.Emple1_finAt_Surt = fila1.Emple1_finAt_Surt;
                                            fila2.Emple1_Estado_Surt = fila1.Emple1_Estado_Surt;

                                            fila2.Emple3_finAt_Surt = fila1.Emple3_finAt_Surt;
                                            fila2.Emple3_Estado_Surt = fila1.Emple3_Estado_Surt;

                                            cliente.Estado = SiendoAtendidoSurt;
                                            cliente.SiendoAtendidoPor = Empleado2_Surt;
                                            fila2.Persona.Add(cliente);

                                            cliente.EnFilaNumero = NumeroSimulacionActual;
                                        }
                                        else if (fila1.Emple3_Estado_Surt.Libre)
                                        {
                                            //cliente.Tipo1 = CalcularTipoServicio(fila2.RND_tipo1Servicio, ProbSurtidor, ProbGomeria, ProbVentasAcce);

                                            fila2.RND_finAtencionSurt = GenerarRND();

                                            fila2.tiempoAtencionSurt = VariableAleatoriaUniforme(MinTiempoSurt, MaxTiempoSurt, fila2.RND_finAtencionSurt);
                                            fila2.Emple3_finAt_Surt = fila2.Reloj + fila2.tiempoAtencionSurt;
                                            fila2.Emple3_Estado_Surt = estadosOcupado["Empleado3 Surt"];

                                            fila2.Emple1_finAt_Surt = fila1.Emple1_finAt_Surt;
                                            fila2.Emple1_Estado_Surt = fila1.Emple1_Estado_Surt;

                                            fila2.Emple2_finAt_Surt = fila1.Emple2_finAt_Surt;
                                            fila2.Emple2_Estado_Surt = fila1.Emple2_Estado_Surt;

                                            cliente.Estado = SiendoAtendidoSurt;
                                            cliente.SiendoAtendidoPor = Empleado3_Surt;
                                            fila2.Persona.Add(cliente);

                                            cliente.EnFilaNumero = NumeroSimulacionActual;
                                        }
                                        else
                                        {
                                            fila2.Cola_Surtidor = fila1.Cola_Surtidor + 1;

                                            fila2.Emple1_finAt_Surt = fila1.Emple1_finAt_Surt;
                                            fila2.Emple1_Estado_Surt = fila1.Emple1_Estado_Surt;

                                            fila2.Emple2_finAt_Surt = fila1.Emple2_finAt_Surt;
                                            fila2.Emple2_Estado_Surt = fila1.Emple2_Estado_Surt;

                                            fila2.Emple3_finAt_Surt = fila1.Emple3_finAt_Surt;
                                            fila2.Emple3_Estado_Surt = fila1.Emple3_Estado_Surt;

                                            fila2.Persona.Add(cliente);
                                            cliente.EnFilaNumero = NumeroSimulacionActual;

                                            //cliente.Tipo1 = CalcularTipoServicio(fila2.RND_tipo1Servicio, ProbSurtidor, ProbGomeria, ProbVentasAcce);

                                            cliente.Estado = EsperandoSurtidor;
                                            fila2.RND_finAtencionSurt = 0;
                                            fila2.tiempoAtencionSurt = 0;
                                        }
                                    }

                                    fila2.Emple1_finAt_Gome = fila1.Emple1_finAt_Gome;
                                    fila2.Emple1_Estado_Gome = fila1.Emple1_Estado_Gome;

                                    fila2.Emple2_finAt_Gome = fila1.Emple2_finAt_Gome;
                                    fila2.Emple2_Estado_Gome = fila1.Emple2_Estado_Gome;

                                    fila2.Emple_finAt_Vent = fila1.Emple_finAt_Vent;
                                    fila2.Emple_Estado_Vent = fila1.Emple_Estado_Vent;

                                    break;


                                case "Gomeria":

                                    if (fila1.Cola_Gomeria > 0)
                                    {
                                        // Si la cola es mayor que cero, implica que el servidor está ocupado y por ende se debe incrementar la cola.

                                        fila2.Cola_Gomeria = fila1.Cola_Gomeria + 1;

                                        fila2.Emple1_finAt_Gome = fila1.Emple1_finAt_Gome;
                                        fila2.Emple1_Estado_Gome = fila1.Emple1_Estado_Gome;

                                        fila2.Emple2_finAt_Gome = fila1.Emple2_finAt_Gome;
                                        fila2.Emple2_Estado_Gome = fila1.Emple2_Estado_Gome;

                                        fila2.Persona.Add(cliente);
                                        cliente.EnFilaNumero = NumeroSimulacionActual;

                                        //fila2.tipo1Servicio = CalcularTipoServicio(fila2.RND_tipo1Servicio, ProbSurtidor, ProbGomeria, ProbVentasAcce);
                                        //cliente.Tipo1 = fila2.tipo1Servicio;

                                        cliente.Estado = EsperandoGomeria;
                                    }

                                    if (fila1.Cola_Gomeria == 0)
                                    {
                                        if (fila1.Emple1_Estado_Gome.Libre)
                                        {
                                            //cliente.Tipo1 = CalcularTipoServicio(fila2.RND_tipo1Servicio, ProbSurtidor, ProbGomeria, ProbVentasAcce);

                                            fila2.RND_finAtencionGome = GenerarRND();

                                            fila2.tiempoAtencionGome = VariableAleatoriaUniforme(MinTiempoGome, MaxTiempoGome, fila2.RND_finAtencionGome);
                                            fila2.Emple1_finAt_Gome = fila2.Reloj + fila2.tiempoAtencionGome;
                                            fila2.Emple1_Estado_Gome = estadosOcupado["Empleado1 Gome"];

                                            fila2.Emple2_finAt_Gome = fila1.Emple2_finAt_Gome;
                                            fila2.Emple2_Estado_Gome = fila1.Emple2_Estado_Gome;

                                            cliente.Estado = SiendoAtendidoGome;
                                            cliente.SiendoAtendidoPor = Empleado1_Gome;
                                            fila2.Persona.Add(cliente);

                                            cliente.EnFilaNumero = NumeroSimulacionActual;
                                        }
                                        else if (fila1.Emple2_Estado_Gome.Libre)
                                        {
                                            //cliente.Tipo1 = CalcularTipoServicio(fila2.RND_tipo1Servicio, ProbSurtidor, ProbGomeria, ProbVentasAcce);

                                            fila2.RND_finAtencionGome = GenerarRND();

                                            fila2.tiempoAtencionGome = VariableAleatoriaUniforme(MinTiempoGome, MaxTiempoGome, fila2.RND_finAtencionGome);
                                            fila2.Emple2_finAt_Gome = fila2.Reloj + fila2.tiempoAtencionGome;
                                            fila2.Emple2_Estado_Gome = estadosOcupado["Empleado2 Gome"];

                                            fila2.Emple1_finAt_Gome = fila1.Emple1_finAt_Gome;
                                            fila2.Emple1_Estado_Gome = fila1.Emple1_Estado_Gome;

                                            cliente.Estado = SiendoAtendidoGome;
                                            cliente.SiendoAtendidoPor = Empleado2_Gome;
                                            fila2.Persona.Add(cliente);

                                            cliente.EnFilaNumero = NumeroSimulacionActual;
                                        }
                                        else
                                        {
                                            fila2.Cola_Gomeria = fila1.Cola_Gomeria + 1;

                                            fila2.Emple1_finAt_Gome = fila1.Emple1_finAt_Gome;
                                            fila2.Emple1_Estado_Gome = fila1.Emple1_Estado_Gome;

                                            fila2.Emple2_finAt_Gome = fila1.Emple2_finAt_Gome;
                                            fila2.Emple2_Estado_Gome = fila1.Emple2_Estado_Gome;

                                            fila2.Persona.Add(cliente);
                                            cliente.EnFilaNumero = NumeroSimulacionActual;

                                            //cliente.Tipo1 = CalcularTipoServicio(fila2.RND_tipo1Servicio, ProbSurtidor, ProbGomeria, ProbVentasAcce);

                                            cliente.Estado = EsperandoGomeria;
                                            fila2.RND_finAtencionGome = 0;
                                            fila2.tiempoAtencionGome = 0;
                                        }
                                    }

                                    fila2.Emple1_finAt_Surt = fila1.Emple1_finAt_Surt;
                                    fila2.Emple2_finAt_Surt = fila1.Emple2_finAt_Surt;
                                    fila2.Emple3_finAt_Surt = fila1.Emple3_finAt_Surt;

                                    fila2.Emple1_Estado_Surt = fila1.Emple1_Estado_Surt;
                                    fila2.Emple2_Estado_Surt = fila1.Emple2_Estado_Surt;
                                    fila2.Emple3_Estado_Surt = fila1.Emple3_Estado_Surt;

                                    fila2.Emple_finAt_Vent = fila1.Emple_finAt_Vent;
                                    fila2.Emple_Estado_Vent = fila1.Emple_Estado_Vent;

                                    break;


                                case "Ventas Accesorios":

                                    if (fila1.Cola_VentasAccesorios > 0)
                                    {
                                        // Si la cola es mayor que cero, implica que el servidor está ocupado y por ende se debe incrementar la cola.

                                        fila2.Cola_VentasAccesorios = fila1.Cola_VentasAccesorios + 1;

                                        fila2.Emple_finAt_Vent = fila1.Emple_finAt_Vent;
                                        fila2.Emple_Estado_Vent = fila1.Emple_Estado_Vent;

                                        fila2.Persona.Add(cliente);
                                        cliente.EnFilaNumero = NumeroSimulacionActual;

                                        //fila2.tipo1Servicio = CalcularTipoServicio(fila2.RND_tipo1Servicio, ProbSurtidor, ProbGomeria, ProbVentasAcce);
                                        //cliente.Tipo1 = fila2.tipo1Servicio;

                                        cliente.Estado = EsperandoVentas;
                                    }

                                    if (fila1.Cola_VentasAccesorios == 0)
                                    {
                                        if (fila1.Emple_Estado_Vent.Libre)
                                        {
                                            //cliente.Tipo1 = CalcularTipoServicio(fila2.RND_tipo1Servicio, ProbSurtidor, ProbGomeria, ProbVentasAcce);

                                            fila2.RND_finAtencionVent = GenerarRND();

                                            fila2.tiempoAtencionVent = VariableAleatoriaUniforme(MinTiempoVent, MaxTiempoVent, fila2.RND_finAtencionVent);
                                            fila2.Emple_finAt_Vent = fila2.Reloj + fila2.tiempoAtencionVent;
                                            fila2.Emple_Estado_Vent = estadosOcupado["Empleado Vent"];

                                            cliente.Estado = SiendoAtendidoVent;
                                            cliente.SiendoAtendidoPor = Empleado_Vent;
                                            fila2.Persona.Add(cliente);

                                            cliente.EnFilaNumero = NumeroSimulacionActual;
                                        }
                                        else
                                        {
                                            fila2.Cola_VentasAccesorios = fila1.Cola_VentasAccesorios + 1;

                                            fila2.Emple_finAt_Vent = fila1.Emple_finAt_Vent;
                                            fila2.Emple_Estado_Vent = fila1.Emple_Estado_Vent;

                                            fila2.Persona.Add(cliente);
                                            cliente.EnFilaNumero = NumeroSimulacionActual;

                                            //cliente.Tipo1 = CalcularTipoServicio(fila2.RND_tipo1Servicio, ProbSurtidor, ProbGomeria, ProbVentasAcce);

                                            cliente.Estado = EsperandoVentas;
                                            fila2.RND_finAtencionVent = 0;
                                            fila2.tiempoAtencionVent = 0;
                                        }
                                    }

                                    fila2.Emple1_finAt_Surt = fila1.Emple1_finAt_Surt;
                                    fila2.Emple2_finAt_Surt = fila1.Emple2_finAt_Surt;
                                    fila2.Emple3_finAt_Surt = fila1.Emple3_finAt_Surt;

                                    fila2.Emple1_Estado_Surt = fila1.Emple1_Estado_Surt;
                                    fila2.Emple2_Estado_Surt = fila1.Emple2_Estado_Surt;
                                    fila2.Emple3_Estado_Surt = fila1.Emple3_Estado_Surt;

                                    fila2.Emple1_finAt_Gome = fila1.Emple1_finAt_Gome;
                                    fila2.Emple1_Estado_Gome = fila1.Emple1_Estado_Gome;

                                    fila2.Emple2_finAt_Gome = fila1.Emple2_finAt_Gome;
                                    fila2.Emple2_Estado_Gome = fila1.Emple2_Estado_Gome;

                                    break;
                            }
                        }
                        TodosLosClientes.Add(cliente.CopiarCliente(cliente));
                        break;

                    case "FinAt_Emple1_Surt":

                        // Se destruye cliente ya atendido.

                        foreach (Temporal client in TodosLosClientes)
                        {
                            if (client.Estado == SiendoAtendidoSurt && client.SiendoAtendidoPor == Empleado1_Surt)
                            {
                                fila2.Emple1_finAt_Surt = 0;

                                fila2.RND_tipo2Servicio = GenerarRND();
                                fila2.tipo2Servicio = CalcularTipoServicio(fila2.RND_tipo2Servicio, ProbSurtidor2, ProbGomeria2, ProbVentasAcce2);
                                client.Tipo2 = fila2.tipo2Servicio;

                                if (client.Tipo2 == "Surtidor")
                                {   
                                    //client.OtraAtencion = "NO";
                                    //client.Estado = EsperandoSurtidor;

                                    ++CantClientesVanSinComprar;

                                    client.DestruirCliente(client);
                                }
                                if (client.Tipo2 == "Gomeria")
                                {
                                    //client.Estado = EsperandoGomeria;
                                    //client.Tipo1 = "Gomeria";
                                    //client.OtraAtencion = "SI";

                                    ++CantClientesVanSinComprar;

                                    if (fila1.Cola_Gomeria > 0)
                                    {
                                        // Si la cola es mayor que cero, implica que el servidor está ocupado y por ende se debe incrementar la cola.

                                        fila2.Cola_Gomeria = fila1.Cola_Gomeria + 1;

                                        fila2.Emple1_finAt_Gome = fila1.Emple1_finAt_Gome;
                                        fila2.Emple1_Estado_Gome = fila1.Emple1_Estado_Gome;

                                        fila2.Emple2_finAt_Gome = fila1.Emple2_finAt_Gome;
                                        fila2.Emple2_Estado_Gome = fila1.Emple2_Estado_Gome;

                                        fila2.Persona.Add(client);
                                        client.EnFilaNumero = NumeroSimulacionActual;

                                        //fila2.tipo1Servicio = CalcularTipoServicio(fila2.RND_tipo1Servicio, ProbSurtidor, ProbGomeria, ProbVentasAcce);
                                        //cliente.Tipo1 = fila2.tipo1Servicio;

                                        client.Estado = EsperandoGomeria;
                                    }

                                    if (fila1.Cola_Gomeria == 0)
                                    {
                                        if (fila1.Emple1_Estado_Gome.Libre)
                                        {
                                            //cliente.Tipo1 = CalcularTipoServicio(fila2.RND_tipo1Servicio, ProbSurtidor, ProbGomeria, ProbVentasAcce);

                                            fila2.RND_finAtencionGome = GenerarRND();

                                            fila2.tiempoAtencionGome = VariableAleatoriaUniforme(MinTiempoGome, MaxTiempoGome, fila2.RND_finAtencionGome);
                                            fila2.Emple1_finAt_Gome = fila2.Reloj + fila2.tiempoAtencionGome;
                                            fila2.Emple1_Estado_Gome = estadosOcupado["Empleado1 Gome"];

                                            fila1.tiempoAtencionGome = fila2.tiempoAtencionGome;
                                            fila1.Emple1_finAt_Gome = fila2.Emple1_finAt_Gome;
                                            fila1.Emple1_Estado_Gome = fila2.Emple1_Estado_Gome;

                                            fila2.Emple2_finAt_Gome = fila1.Emple2_finAt_Gome;
                                            fila2.Emple2_Estado_Gome = fila1.Emple2_Estado_Gome;

                                            client.Estado = SiendoAtendidoGome;
                                            client.SiendoAtendidoPor = Empleado1_Gome;
                                            fila2.Persona.Add(client);

                                            client.EnFilaNumero = NumeroSimulacionActual;
                                        }
                                        else if (fila1.Emple2_Estado_Gome.Libre)
                                        {
                                            //cliente.Tipo1 = CalcularTipoServicio(fila2.RND_tipo1Servicio, ProbSurtidor, ProbGomeria, ProbVentasAcce);

                                            fila2.RND_finAtencionGome = GenerarRND();

                                            fila2.tiempoAtencionGome = VariableAleatoriaUniforme(MinTiempoGome, MaxTiempoGome, fila2.RND_finAtencionGome);
                                            fila2.Emple2_finAt_Gome = fila2.Reloj + fila2.tiempoAtencionGome;
                                            fila2.Emple2_Estado_Gome = estadosOcupado["Empleado2 Gome"];

                                            fila1.tiempoAtencionGome = fila2.tiempoAtencionGome;
                                            fila1.Emple2_finAt_Gome = fila2.Emple2_finAt_Gome;
                                            fila1.Emple2_Estado_Gome = fila2.Emple2_Estado_Gome;

                                            fila2.Emple1_finAt_Gome = fila1.Emple1_finAt_Gome;
                                            fila2.Emple1_Estado_Gome = fila1.Emple1_Estado_Gome;

                                            client.Estado = SiendoAtendidoGome;
                                            client.SiendoAtendidoPor = Empleado2_Gome;
                                            fila2.Persona.Add(client);

                                            client.EnFilaNumero = NumeroSimulacionActual;
                                        }
                                        else
                                        {
                                            fila2.Cola_Gomeria = fila1.Cola_Gomeria + 1;

                                            fila2.Emple1_finAt_Gome = fila1.Emple1_finAt_Gome;
                                            fila2.Emple1_Estado_Gome = fila1.Emple1_Estado_Gome;

                                            fila2.Emple2_finAt_Gome = fila1.Emple2_finAt_Gome;
                                            fila2.Emple2_Estado_Gome = fila1.Emple2_Estado_Gome;

                                            fila2.Persona.Add(client);
                                            client.EnFilaNumero = NumeroSimulacionActual;

                                            //cliente.Tipo1 = CalcularTipoServicio(fila2.RND_tipo1Servicio, ProbSurtidor, ProbGomeria, ProbVentasAcce);

                                            client.Estado = EsperandoGomeria;
                                            fila2.RND_finAtencionGome = 0;
                                            fila2.tiempoAtencionGome = 0;
                                        }
                                    }
                                }
                                if (client.Tipo2 == "Ventas Accesorios")
                                {
                                    //client.Estado = EsperandoVentas;
                                    //client.Tipo1 = "Ventas Accesorios";
                                    //client.OtraAtencion = "SI";

                                    if (fila1.Cola_VentasAccesorios > 0)
                                    {
                                        // Si la cola es mayor que cero, implica que el servidor está ocupado y por ende se debe incrementar la cola.

                                        fila2.Cola_VentasAccesorios = fila1.Cola_VentasAccesorios + 1;

                                        fila2.Emple_finAt_Vent = fila1.Emple_finAt_Vent;
                                        fila2.Emple_Estado_Vent = fila1.Emple_Estado_Vent;

                                        fila2.Persona.Add(client);
                                        client.EnFilaNumero = NumeroSimulacionActual;

                                        //fila2.tipo1Servicio = CalcularTipoServicio(fila2.RND_tipo1Servicio, ProbSurtidor, ProbGomeria, ProbVentasAcce);
                                        //cliente.Tipo1 = fila2.tipo1Servicio;

                                        client.Estado = EsperandoVentas;
                                    }

                                    if (fila1.Cola_VentasAccesorios == 0)
                                    {
                                        if (fila1.Emple_Estado_Vent.Libre)
                                        {
                                            //cliente.Tipo1 = CalcularTipoServicio(fila2.RND_tipo1Servicio, ProbSurtidor, ProbGomeria, ProbVentasAcce);

                                            fila2.RND_finAtencionVent = GenerarRND();

                                            fila2.tiempoAtencionVent = VariableAleatoriaUniforme(MinTiempoVent, MaxTiempoVent, fila2.RND_finAtencionVent);
                                            fila2.Emple_finAt_Vent = fila2.Reloj + fila2.tiempoAtencionVent;
                                            fila2.Emple_Estado_Vent = estadosOcupado["Empleado Vent"];

                                            fila1.tiempoAtencionVent = fila2.tiempoAtencionVent;
                                            fila1.Emple_finAt_Vent = fila2.Emple_finAt_Vent;
                                            fila1.Emple_Estado_Vent = fila2.Emple_Estado_Vent;

                                            client.Estado = SiendoAtendidoVent;
                                            client.SiendoAtendidoPor = Empleado_Vent;
                                            fila2.Persona.Add(client);

                                            client.EnFilaNumero = NumeroSimulacionActual;
                                        }
                                        else
                                        {
                                            fila2.Cola_VentasAccesorios = fila1.Cola_VentasAccesorios + 1;

                                            fila2.Emple_finAt_Vent = fila1.Emple_finAt_Vent;
                                            fila2.Emple_Estado_Vent = fila1.Emple_Estado_Vent;

                                            fila2.Persona.Add(client);
                                            client.EnFilaNumero = NumeroSimulacionActual;

                                            //cliente.Tipo1 = CalcularTipoServicio(fila2.RND_tipo1Servicio, ProbSurtidor, ProbGomeria, ProbVentasAcce);

                                            client.Estado = EsperandoVentas;
                                            fila2.RND_finAtencionVent = 0;
                                            fila2.tiempoAtencionVent = 0;
                                        }
                                    }
                                }

                                //fila2.TiempoEnSistema = fila1.TiempoEnSistema + (fila2.Reloj - client.HoraIngreso);
                                break;
                            }
                        }

                        // Si hay cola se atiende a la siguiente persona.

                        if (fila1.Cola_Surtidor > 0)
                        {
                            var proxCliente = proximoCliente(TodosLosClientes);

                            fila2.RND_finAtencionSurt = GenerarRND();

                            fila2.tiempoAtencionSurt = VariableAleatoriaUniforme(MinTiempoSurt, MaxTiempoSurt, fila2.RND_finAtencionSurt);
                            fila2.Emple1_finAt_Surt = fila2.Reloj + fila2.tiempoAtencionSurt;
                            fila2.Emple1_Estado_Surt = estadosOcupado["Empleado1 Surt"];

                            //fila2.Emple2_finAt_Surt = fila1.Emple2_finAt_Surt;
                            //fila2.Emple2_Estado_Surt = fila1.Emple2_Estado_Surt;

                            //fila2.Emple3_finAt_Surt = fila1.Emple3_finAt_Surt;
                            //fila2.Emple3_Estado_Surt = fila1.Emple3_Estado_Surt;

                            proxCliente.Estado = SiendoAtendidoSurt;
                            proxCliente.SiendoAtendidoPor = Empleado1_Surt;

                            //proxCliente.EnFilaNumero = NumeroSimulacionActual;

                            fila2.Cola_Surtidor = fila1.Cola_Surtidor - 1;
                        }
                        else
                        {
                            fila2.Emple1_Estado_Surt = estadosLibre["Empleado1 Surt"];
                            fila2.Emple1_finAt_Surt = 0;

                        }

                        // Arrastrar valores que no se utilizan.

                        fila2.proxLlegada = fila1.proxLlegada;
                
                        fila2.Emple2_finAt_Surt = fila1.Emple2_finAt_Surt;
                        fila2.Emple2_Estado_Surt = fila1.Emple2_Estado_Surt;

                        fila2.Emple3_finAt_Surt = fila1.Emple3_finAt_Surt;
                        fila2.Emple3_Estado_Surt = fila1.Emple3_Estado_Surt;

                        fila2.Emple1_finAt_Gome = fila1.Emple1_finAt_Gome;
                        fila2.Emple1_Estado_Gome = fila1.Emple1_Estado_Gome;

                        fila2.Emple2_finAt_Gome = fila1.Emple2_finAt_Gome;
                        fila2.Emple2_Estado_Gome = fila1.Emple2_Estado_Gome;

                        fila2.Emple_finAt_Vent = fila1.Emple_finAt_Vent;
                        fila2.Emple_Estado_Vent = fila1.Emple_Estado_Vent;

                        fila2.RND_tipo1Servicio = 0;
                        fila2.tipo1Servicio = "";

                        break;

                    case "FinAt_Emple2_Surt":

                        // Se destruye cliente ya atendido.

                        foreach (Temporal client in TodosLosClientes)
                        {
                            if (client.Estado == SiendoAtendidoSurt && client.SiendoAtendidoPor == Empleado2_Surt)
                            {
                                fila2.Emple2_finAt_Surt = 0;

                                fila2.RND_tipo2Servicio = GenerarRND();
                                fila2.tipo2Servicio = CalcularTipoServicio(fila2.RND_tipo2Servicio, ProbSurtidor2, ProbGomeria2, ProbVentasAcce2);
                                client.Tipo2 = fila2.tipo2Servicio;

                                if (client.Tipo2 == "Surtidor")
                                {
                                    //client.OtraAtencion = "NO";
                                    //client.Estado = EsperandoSurtidor;

                                    ++CantClientesVanSinComprar;
                                    client.DestruirCliente(client);
                                }
                                if (client.Tipo2 == "Gomeria")
                                {
                                    //client.Estado = EsperandoGomeria;
                                    //client.Tipo1 = "Gomeria";
                                    //client.OtraAtencion = "SI";

                                    ++CantClientesVanSinComprar;

                                    if (fila1.Cola_Gomeria > 0)
                                    {
                                        // Si la cola es mayor que cero, implica que el servidor está ocupado y por ende se debe incrementar la cola.

                                        fila2.Cola_Gomeria = fila1.Cola_Gomeria + 1;

                                        fila2.Emple1_finAt_Gome = fila1.Emple1_finAt_Gome;
                                        fila2.Emple1_Estado_Gome = fila1.Emple1_Estado_Gome;

                                        fila2.Emple2_finAt_Gome = fila1.Emple2_finAt_Gome;
                                        fila2.Emple2_Estado_Gome = fila1.Emple2_Estado_Gome;

                                        fila2.Persona.Add(client);
                                        client.EnFilaNumero = NumeroSimulacionActual;

                                        //fila2.tipo1Servicio = CalcularTipoServicio(fila2.RND_tipo1Servicio, ProbSurtidor, ProbGomeria, ProbVentasAcce);
                                        //cliente.Tipo1 = fila2.tipo1Servicio;

                                        client.Estado = EsperandoGomeria;
                                    }

                                    if (fila1.Cola_Gomeria == 0)
                                    {
                                        if (fila1.Emple1_Estado_Gome.Libre)
                                        {
                                            //cliente.Tipo1 = CalcularTipoServicio(fila2.RND_tipo1Servicio, ProbSurtidor, ProbGomeria, ProbVentasAcce);

                                            fila2.RND_finAtencionGome = GenerarRND();

                                            fila2.tiempoAtencionGome = VariableAleatoriaUniforme(MinTiempoGome, MaxTiempoGome, fila2.RND_finAtencionGome);
                                            fila2.Emple1_finAt_Gome = fila2.Reloj + fila2.tiempoAtencionGome;
                                            fila2.Emple1_Estado_Gome = estadosOcupado["Empleado1 Gome"];

                                            fila1.tiempoAtencionGome = fila2.tiempoAtencionGome;
                                            fila1.Emple1_finAt_Gome = fila2.Emple1_finAt_Gome;
                                            fila1.Emple1_Estado_Gome = fila2.Emple1_Estado_Gome;

                                            fila2.Emple2_finAt_Gome = fila1.Emple2_finAt_Gome;
                                            fila2.Emple2_Estado_Gome = fila1.Emple2_Estado_Gome;

                                            client.Estado = SiendoAtendidoGome;
                                            client.SiendoAtendidoPor = Empleado1_Gome;
                                            fila2.Persona.Add(client);

                                            client.EnFilaNumero = NumeroSimulacionActual;
                                        }
                                        else if (fila1.Emple2_Estado_Gome.Libre)
                                        {
                                            //cliente.Tipo1 = CalcularTipoServicio(fila2.RND_tipo1Servicio, ProbSurtidor, ProbGomeria, ProbVentasAcce);

                                            fila2.RND_finAtencionGome = GenerarRND();

                                            fila2.tiempoAtencionGome = VariableAleatoriaUniforme(MinTiempoGome, MaxTiempoGome, fila2.RND_finAtencionGome);
                                            fila2.Emple2_finAt_Gome = fila2.Reloj + fila2.tiempoAtencionGome;
                                            fila2.Emple2_Estado_Gome = estadosOcupado["Empleado2 Gome"];

                                            fila1.tiempoAtencionGome = fila2.tiempoAtencionGome;
                                            fila1.Emple2_finAt_Gome = fila2.Emple2_finAt_Gome;
                                            fila1.Emple2_Estado_Gome = fila2.Emple2_Estado_Gome;

                                            fila2.Emple1_finAt_Gome = fila1.Emple1_finAt_Gome;
                                            fila2.Emple1_Estado_Gome = fila1.Emple1_Estado_Gome;

                                            client.Estado = SiendoAtendidoGome;
                                            client.SiendoAtendidoPor = Empleado2_Gome;
                                            fila2.Persona.Add(client);

                                            client.EnFilaNumero = NumeroSimulacionActual;
                                        }
                                        else
                                        {
                                            fila2.Cola_Gomeria = fila1.Cola_Gomeria + 1;

                                            fila2.Emple1_finAt_Gome = fila1.Emple1_finAt_Gome;
                                            fila2.Emple1_Estado_Gome = fila1.Emple1_Estado_Gome;

                                            fila2.Emple2_finAt_Gome = fila1.Emple2_finAt_Gome;
                                            fila2.Emple2_Estado_Gome = fila1.Emple2_Estado_Gome;

                                            fila2.Persona.Add(client);
                                            client.EnFilaNumero = NumeroSimulacionActual;

                                            //cliente.Tipo1 = CalcularTipoServicio(fila2.RND_tipo1Servicio, ProbSurtidor, ProbGomeria, ProbVentasAcce);

                                            client.Estado = EsperandoGomeria;
                                            fila2.RND_finAtencionGome = 0;
                                            fila2.tiempoAtencionGome = 0;
                                        }
                                    }
                                }
                                if (client.Tipo2 == "Ventas Accesorios")
                                {
                                    //client.Estado = EsperandoVentas;
                                    //client.Tipo1 = "Ventas Accesorios";
                                    //client.OtraAtencion = "SI";

                                    if (fila1.Cola_VentasAccesorios > 0)
                                    {
                                        // Si la cola es mayor que cero, implica que el servidor está ocupado y por ende se debe incrementar la cola.

                                        fila2.Cola_VentasAccesorios = fila1.Cola_VentasAccesorios + 1;

                                        fila2.Emple_finAt_Vent = fila1.Emple_finAt_Vent;
                                        fila2.Emple_Estado_Vent = fila1.Emple_Estado_Vent;

                                        fila2.Persona.Add(client);
                                        client.EnFilaNumero = NumeroSimulacionActual;

                                        //fila2.tipo1Servicio = CalcularTipoServicio(fila2.RND_tipo1Servicio, ProbSurtidor, ProbGomeria, ProbVentasAcce);
                                        //cliente.Tipo1 = fila2.tipo1Servicio;

                                        client.Estado = EsperandoVentas;
                                    }

                                    if (fila1.Cola_VentasAccesorios == 0)
                                    {
                                        if (fila1.Emple_Estado_Vent.Libre)
                                        {
                                            //cliente.Tipo1 = CalcularTipoServicio(fila2.RND_tipo1Servicio, ProbSurtidor, ProbGomeria, ProbVentasAcce);

                                            fila2.RND_finAtencionVent = GenerarRND();

                                            fila2.tiempoAtencionVent = VariableAleatoriaUniforme(MinTiempoVent, MaxTiempoVent, fila2.RND_finAtencionVent);
                                            fila2.Emple_finAt_Vent = fila2.Reloj + fila2.tiempoAtencionVent;
                                            fila2.Emple_Estado_Vent = estadosOcupado["Empleado Vent"];

                                            fila1.tiempoAtencionVent = fila2.tiempoAtencionVent;
                                            fila1.Emple_finAt_Vent = fila2.Emple_finAt_Vent;
                                            fila1.Emple_Estado_Vent = fila2.Emple_Estado_Vent;

                                            client.Estado = SiendoAtendidoVent;
                                            client.SiendoAtendidoPor = Empleado_Vent;
                                            fila2.Persona.Add(client);

                                            client.EnFilaNumero = NumeroSimulacionActual;
                                        }
                                        else
                                        {
                                            fila2.Cola_VentasAccesorios = fila1.Cola_VentasAccesorios + 1;

                                            fila2.Emple_finAt_Vent = fila1.Emple_finAt_Vent;
                                            fila2.Emple_Estado_Vent = fila1.Emple_Estado_Vent;

                                            fila2.Persona.Add(client);
                                            client.EnFilaNumero = NumeroSimulacionActual;

                                            //cliente.Tipo1 = CalcularTipoServicio(fila2.RND_tipo1Servicio, ProbSurtidor, ProbGomeria, ProbVentasAcce);

                                            client.Estado = EsperandoVentas;
                                            fila2.RND_finAtencionVent = 0;
                                            fila2.tiempoAtencionVent = 0;
                                        }
                                    }
                                }
                                //fila2.TiempoEnSistema = fila1.TiempoEnSistema + (fila2.Reloj - client.HoraIngreso);
                                break;
                            }
                        }

                        // Si hay cola se atiende a la siguiente persona.

                        if (fila1.Cola_Surtidor > 0)
                        {
                            var proxCliente = proximoCliente(TodosLosClientes);

                            fila2.RND_finAtencionSurt = GenerarRND();

                            fila2.tiempoAtencionSurt = VariableAleatoriaUniforme(MinTiempoSurt, MaxTiempoSurt, fila2.RND_finAtencionSurt);
                            fila2.Emple2_finAt_Surt = fila2.Reloj + fila2.tiempoAtencionSurt;
                            fila2.Emple2_Estado_Surt = estadosOcupado["Empleado2 Surt"];

                            //fila2.Emple2_finAt_Surt = fila1.Emple2_finAt_Surt;
                            //fila2.Emple2_Estado_Surt = fila1.Emple2_Estado_Surt;

                            //fila2.Emple3_finAt_Surt = fila1.Emple3_finAt_Surt;
                            //fila2.Emple3_Estado_Surt = fila1.Emple3_Estado_Surt;

                            proxCliente.Estado = SiendoAtendidoSurt;
                            proxCliente.SiendoAtendidoPor = Empleado2_Surt;

                            //proxCliente.EnFilaNumero = NumeroSimulacionActual;

                            fila2.Cola_Surtidor = fila1.Cola_Surtidor - 1;
                        }
                        else
                        {   
                            fila2.Emple2_Estado_Surt = estadosLibre["Empleado2 Surt"];
                            fila2.Emple2_finAt_Surt = 0;
                        }

                        // Arrastrar valores que no se utilizan.

                        fila2.proxLlegada = fila1.proxLlegada;

                        fila2.Emple1_finAt_Surt = fila1.Emple1_finAt_Surt;
                        fila2.Emple1_Estado_Surt = fila1.Emple1_Estado_Surt;

                        fila2.Emple3_finAt_Surt = fila1.Emple3_finAt_Surt;
                        fila2.Emple3_Estado_Surt = fila1.Emple3_Estado_Surt;

                        fila2.Emple1_finAt_Gome = fila1.Emple1_finAt_Gome;
                        fila2.Emple1_Estado_Gome = fila1.Emple1_Estado_Gome;

                        fila2.Emple2_finAt_Gome = fila1.Emple2_finAt_Gome;
                        fila2.Emple2_Estado_Gome = fila1.Emple2_Estado_Gome;

                        fila2.Emple_finAt_Vent = fila1.Emple_finAt_Vent;
                        fila2.Emple_Estado_Vent = fila1.Emple_Estado_Vent;

                        fila2.RND_tipo1Servicio = 0;
                        fila2.tipo1Servicio = "";

                        break;


                    case "FinAt_Emple3_Surt":

                        // Se destruye cliente ya atendido.

                        foreach (Temporal client in TodosLosClientes)
                        {
                            if (client.Estado == SiendoAtendidoSurt && client.SiendoAtendidoPor == Empleado3_Surt)
                            {
                                fila2.Emple3_finAt_Surt = 0;

                                fila2.RND_tipo2Servicio = GenerarRND();
                                fila2.tipo2Servicio = CalcularTipoServicio(fila2.RND_tipo2Servicio, ProbSurtidor2, ProbGomeria2, ProbVentasAcce2);
                                client.Tipo2 = fila2.tipo2Servicio;

                                if (client.Tipo2 == "Surtidor")
                                {
                                    //client.Estado = EsperandoSurtidor;
                                    //client.OtraAtencion = "NO";

                                    ++CantClientesVanSinComprar;

                                    client.DestruirCliente(client);
                                }
                                if (client.Tipo2 == "Gomeria")
                                {
                                    //client.Estado = EsperandoGomeria;
                                    //client.Tipo1 = "Gomeria";
                                    //client.OtraAtencion = "SI";

                                    ++CantClientesVanSinComprar;

                                    if (fila1.Cola_Gomeria > 0)
                                    {
                                        // Si la cola es mayor que cero, implica que el servidor está ocupado y por ende se debe incrementar la cola.

                                        fila2.Cola_Gomeria = fila1.Cola_Gomeria + 1;

                                        fila2.Emple1_finAt_Gome = fila1.Emple1_finAt_Gome;
                                        fila2.Emple1_Estado_Gome = fila1.Emple1_Estado_Gome;

                                        fila2.Emple2_finAt_Gome = fila1.Emple2_finAt_Gome;
                                        fila2.Emple2_Estado_Gome = fila1.Emple2_Estado_Gome;

                                        fila2.Persona.Add(client);
                                        client.EnFilaNumero = NumeroSimulacionActual;

                                        //fila2.tipo1Servicio = CalcularTipoServicio(fila2.RND_tipo1Servicio, ProbSurtidor, ProbGomeria, ProbVentasAcce);
                                        //cliente.Tipo1 = fila2.tipo1Servicio;

                                        client.Estado = EsperandoGomeria;
                                    }

                                    if (fila1.Cola_Gomeria == 0)
                                    {
                                        if (fila1.Emple1_Estado_Gome.Libre)
                                        {
                                            //cliente.Tipo1 = CalcularTipoServicio(fila2.RND_tipo1Servicio, ProbSurtidor, ProbGomeria, ProbVentasAcce);

                                            fila2.RND_finAtencionGome = GenerarRND();

                                            fila2.tiempoAtencionGome = VariableAleatoriaUniforme(MinTiempoGome, MaxTiempoGome, fila2.RND_finAtencionGome);
                                            fila2.Emple1_finAt_Gome = fila2.Reloj + fila2.tiempoAtencionGome;
                                            fila2.Emple1_Estado_Gome = estadosOcupado["Empleado1 Gome"];

                                            fila1.tiempoAtencionGome = fila2.tiempoAtencionGome;
                                            fila1.Emple1_finAt_Gome = fila2.Emple1_finAt_Gome;
                                            fila1.Emple1_Estado_Gome = fila2.Emple1_Estado_Gome;

                                            fila2.Emple2_finAt_Gome = fila1.Emple2_finAt_Gome;
                                            fila2.Emple2_Estado_Gome = fila1.Emple2_Estado_Gome;

                                            client.Estado = SiendoAtendidoGome;
                                            client.SiendoAtendidoPor = Empleado1_Gome;
                                            fila2.Persona.Add(client);

                                            client.EnFilaNumero = NumeroSimulacionActual;
                                        }
                                        else if (fila1.Emple2_Estado_Gome.Libre)
                                        {
                                            //cliente.Tipo1 = CalcularTipoServicio(fila2.RND_tipo1Servicio, ProbSurtidor, ProbGomeria, ProbVentasAcce);

                                            fila2.RND_finAtencionGome = GenerarRND();

                                            fila2.tiempoAtencionGome = VariableAleatoriaUniforme(MinTiempoGome, MaxTiempoGome, fila2.RND_finAtencionGome);
                                            fila2.Emple2_finAt_Gome = fila2.Reloj + fila2.tiempoAtencionGome;
                                            fila2.Emple2_Estado_Gome = estadosOcupado["Empleado2 Gome"];

                                            fila1.tiempoAtencionGome = fila2.tiempoAtencionGome;
                                            fila1.Emple2_finAt_Gome = fila2.Emple2_finAt_Gome;
                                            fila1.Emple2_Estado_Gome = fila2.Emple2_Estado_Gome;

                                            fila2.Emple1_finAt_Gome = fila1.Emple1_finAt_Gome;
                                            fila2.Emple1_Estado_Gome = fila1.Emple1_Estado_Gome;

                                            client.Estado = SiendoAtendidoGome;
                                            client.SiendoAtendidoPor = Empleado2_Gome;
                                            fila2.Persona.Add(client);

                                            client.EnFilaNumero = NumeroSimulacionActual;
                                        }
                                        else
                                        {
                                            fila2.Cola_Gomeria = fila1.Cola_Gomeria + 1;

                                            fila2.Emple1_finAt_Gome = fila1.Emple1_finAt_Gome;
                                            fila2.Emple1_Estado_Gome = fila1.Emple1_Estado_Gome;

                                            fila2.Emple2_finAt_Gome = fila1.Emple2_finAt_Gome;
                                            fila2.Emple2_Estado_Gome = fila1.Emple2_Estado_Gome;

                                            fila2.Persona.Add(client);
                                            client.EnFilaNumero = NumeroSimulacionActual;

                                            //cliente.Tipo1 = CalcularTipoServicio(fila2.RND_tipo1Servicio, ProbSurtidor, ProbGomeria, ProbVentasAcce);

                                            client.Estado = EsperandoGomeria;
                                            fila2.RND_finAtencionGome = 0;
                                            fila2.tiempoAtencionGome = 0;
                                        }
                                    }
                                }
                                if (client.Tipo2 == "Ventas Accesorios")
                                {
                                    //client.Estado = EsperandoVentas;
                                    //client.Tipo1 = "Ventas Accesorios";
                                    //client.OtraAtencion = "SI";

                                    if (fila1.Cola_VentasAccesorios > 0)
                                    {
                                        // Si la cola es mayor que cero, implica que el servidor está ocupado y por ende se debe incrementar la cola.

                                        fila2.Cola_VentasAccesorios = fila1.Cola_VentasAccesorios + 1;

                                        fila2.Emple_finAt_Vent = fila1.Emple_finAt_Vent;
                                        fila2.Emple_Estado_Vent = fila1.Emple_Estado_Vent;

                                        fila2.Persona.Add(client);
                                        client.EnFilaNumero = NumeroSimulacionActual;

                                        //fila2.tipo1Servicio = CalcularTipoServicio(fila2.RND_tipo1Servicio, ProbSurtidor, ProbGomeria, ProbVentasAcce);
                                        //cliente.Tipo1 = fila2.tipo1Servicio;

                                        client.Estado = EsperandoVentas;
                                    }

                                    if (fila1.Cola_VentasAccesorios == 0)
                                    {
                                        if (fila1.Emple_Estado_Vent.Libre)
                                        {
                                            //cliente.Tipo1 = CalcularTipoServicio(fila2.RND_tipo1Servicio, ProbSurtidor, ProbGomeria, ProbVentasAcce);

                                            fila2.RND_finAtencionVent = GenerarRND();

                                            fila2.tiempoAtencionVent = VariableAleatoriaUniforme(MinTiempoVent, MaxTiempoVent, fila2.RND_finAtencionVent);
                                            fila2.Emple_finAt_Vent = fila2.Reloj + fila2.tiempoAtencionVent;
                                            fila2.Emple_Estado_Vent = estadosOcupado["Empleado Vent"];

                                            fila1.tiempoAtencionVent = fila2.tiempoAtencionVent;
                                            fila1.Emple_finAt_Vent = fila2.Emple_finAt_Vent;
                                            fila1.Emple_Estado_Vent = fila2.Emple_Estado_Vent;

                                            client.Estado = SiendoAtendidoVent;
                                            client.SiendoAtendidoPor = Empleado_Vent;
                                            fila2.Persona.Add(client);

                                            client.EnFilaNumero = NumeroSimulacionActual;
                                        }
                                        else
                                        {
                                            fila2.Cola_VentasAccesorios = fila1.Cola_VentasAccesorios + 1;

                                            fila2.Emple_finAt_Vent = fila1.Emple_finAt_Vent;
                                            fila2.Emple_Estado_Vent = fila1.Emple_Estado_Vent;

                                            fila2.Persona.Add(client);
                                            client.EnFilaNumero = NumeroSimulacionActual;

                                            //cliente.Tipo1 = CalcularTipoServicio(fila2.RND_tipo1Servicio, ProbSurtidor, ProbGomeria, ProbVentasAcce);

                                            client.Estado = EsperandoVentas;
                                            fila2.RND_finAtencionVent = 0;
                                            fila2.tiempoAtencionVent = 0;
                                        }
                                    }
                                }
                                //fila2.TiempoEnSistema = fila1.TiempoEnSistema + (fila2.Reloj - client.HoraIngreso);
                                break;
                            }
                        }

                        // Si hay cola se atiende a la siguiente persona.

                        if (fila1.Cola_Surtidor > 0)
                        {
                            var proxCliente = proximoCliente(TodosLosClientes);

                            fila2.RND_finAtencionSurt = GenerarRND();

                            fila2.tiempoAtencionSurt = VariableAleatoriaUniforme(MinTiempoSurt, MaxTiempoSurt, fila2.RND_finAtencionSurt);
                            fila2.Emple3_finAt_Surt = fila2.Reloj + fila2.tiempoAtencionSurt;
                            fila2.Emple3_Estado_Surt = estadosOcupado["Empleado3 Surt"];

                            //fila2.Emple2_finAt_Surt = fila1.Emple2_finAt_Surt;
                            //fila2.Emple2_Estado_Surt = fila1.Emple2_Estado_Surt;

                            //fila2.Emple3_finAt_Surt = fila1.Emple3_finAt_Surt;
                            //fila2.Emple3_Estado_Surt = fila1.Emple3_Estado_Surt;

                            proxCliente.Estado = SiendoAtendidoSurt;
                            proxCliente.SiendoAtendidoPor = Empleado3_Surt;

                            //proxCliente.EnFilaNumero = NumeroSimulacionActual;

                            fila2.Cola_Surtidor = fila1.Cola_Surtidor - 1;
                        }
                        else
                        {
                            fila2.Emple3_Estado_Surt = estadosLibre["Empleado3 Surt"];
                            fila2.Emple3_finAt_Surt = 0;
                        }

                        // Arrastrar valores que no se utilizan.

                        fila2.proxLlegada = fila1.proxLlegada;

                        fila2.Emple1_finAt_Surt = fila1.Emple1_finAt_Surt;
                        fila2.Emple1_Estado_Surt = fila1.Emple1_Estado_Surt;

                        fila2.Emple2_finAt_Surt = fila1.Emple2_finAt_Surt;
                        fila2.Emple2_Estado_Surt = fila1.Emple2_Estado_Surt;

                        fila2.Emple1_finAt_Gome = fila1.Emple1_finAt_Gome;
                        fila2.Emple1_Estado_Gome = fila1.Emple1_Estado_Gome;

                        fila2.Emple2_finAt_Gome = fila1.Emple2_finAt_Gome;
                        fila2.Emple2_Estado_Gome = fila1.Emple2_Estado_Gome;

                        fila2.Emple_finAt_Vent = fila1.Emple_finAt_Vent;
                        fila2.Emple_Estado_Vent = fila1.Emple_Estado_Vent;

                        fila2.RND_tipo1Servicio = 0;
                        fila2.tipo1Servicio = "";

                        break;


                    case "FinAt_Emple1_Gome":

                        // Se destruye cliente ya atendido.

                        foreach (Temporal client in TodosLosClientes)
                        {
                            if (client.Estado == SiendoAtendidoGome && client.SiendoAtendidoPor == Empleado1_Gome)
                            {   
                                fila2.Emple1_finAt_Gome = 0;

                                ++CantClientesVanSinComprar;

                                client.DestruirCliente(client);
                                //fila2.TiempoEnSistema = fila1.TiempoEnSistema + (fila2.Reloj - client.HoraIngreso);
                                break;
                            }
                        }

                        // Si hay cola se atiende a la siguiente persona.

                        if (fila1.Cola_Gomeria > 0)
                        {
                            var proxCliente = proximoCliente(TodosLosClientes);

                            fila2.RND_finAtencionGome = GenerarRND();

                            fila2.tiempoAtencionGome = VariableAleatoriaUniforme(MinTiempoGome, MaxTiempoGome, fila2.RND_finAtencionGome);
                            fila2.Emple1_finAt_Gome = fila2.Reloj + fila2.tiempoAtencionGome;
                            fila2.Emple1_Estado_Gome = estadosOcupado["Empleado1 Gome"];

                            //fila2.Emple2_finAt_Surt = fila1.Emple2_finAt_Surt;
                            //fila2.Emple2_Estado_Surt = fila1.Emple2_Estado_Surt;

                            //fila2.Emple3_finAt_Surt = fila1.Emple3_finAt_Surt;
                            //fila2.Emple3_Estado_Surt = fila1.Emple3_Estado_Surt;

                            proxCliente.Estado = SiendoAtendidoGome;
                            proxCliente.SiendoAtendidoPor = Empleado1_Gome;

                            //proxCliente.EnFilaNumero = NumeroSimulacionActual;

                            fila2.Cola_Gomeria = fila1.Cola_Gomeria - 1;
                        }
                        else
                        {
                            fila2.Emple1_Estado_Gome = estadosLibre["Empleado1 Gome"];
                            fila2.Emple1_finAt_Gome = 0;
                        }

                        // Arrastrar valores que no se utilizan.

                        fila2.proxLlegada = fila1.proxLlegada;

                        fila2.Emple1_finAt_Surt = fila1.Emple1_finAt_Surt;
                        fila2.Emple1_Estado_Surt = fila1.Emple1_Estado_Surt;

                        fila2.Emple2_finAt_Surt = fila1.Emple2_finAt_Surt;
                        fila2.Emple2_Estado_Surt = fila1.Emple2_Estado_Surt;

                        fila2.Emple3_finAt_Surt = fila1.Emple3_finAt_Surt;
                        fila2.Emple3_Estado_Surt = fila1.Emple3_Estado_Surt;

                        fila2.Emple2_finAt_Gome = fila1.Emple2_finAt_Gome;
                        fila2.Emple2_Estado_Gome = fila1.Emple2_Estado_Gome;

                        fila2.Emple_finAt_Vent = fila1.Emple_finAt_Vent;
                        fila2.Emple_Estado_Vent = fila1.Emple_Estado_Vent;

                        fila2.RND_tipo1Servicio = 0;
                        fila2.tipo1Servicio = "";

                        break;


                    case "FinAt_Emple2_Gome":

                        // Se destruye cliente ya atendido.

                        foreach (Temporal client in TodosLosClientes)
                        {
                            if (client.Estado == SiendoAtendidoGome && client.SiendoAtendidoPor == Empleado2_Gome)
                            {   
                                fila2.Emple2_finAt_Gome = 0;

                                ++CantClientesVanSinComprar;

                                client.DestruirCliente(client);
                                //fila2.TiempoEnSistema = fila1.TiempoEnSistema + (fila2.Reloj - client.HoraIngreso);
                                break;
                            }
                        }

                        // Si hay cola se atiende a la siguiente persona.

                        if (fila1.Cola_Gomeria > 0)
                        {
                            var proxCliente = proximoCliente(TodosLosClientes);

                            fila2.RND_finAtencionGome = GenerarRND();

                            fila2.tiempoAtencionGome = VariableAleatoriaUniforme(MinTiempoGome, MaxTiempoGome, fila2.RND_finAtencionGome);
                            fila2.Emple2_finAt_Gome = fila2.Reloj + fila2.tiempoAtencionGome;
                            fila2.Emple2_Estado_Gome = estadosOcupado["Empleado2 Gome"];

                            //fila2.Emple2_finAt_Surt = fila1.Emple2_finAt_Surt;
                            //fila2.Emple2_Estado_Surt = fila1.Emple2_Estado_Surt;

                            //fila2.Emple3_finAt_Surt = fila1.Emple3_finAt_Surt;
                            //fila2.Emple3_Estado_Surt = fila1.Emple3_Estado_Surt;

                            proxCliente.Estado = SiendoAtendidoGome;
                            proxCliente.SiendoAtendidoPor = Empleado1_Gome;

                            //proxCliente.EnFilaNumero = NumeroSimulacionActual;

                            fila2.Cola_Gomeria = fila1.Cola_Gomeria - 1;
                        }
                        else
                        {
                            fila2.Emple2_Estado_Gome = estadosLibre["Empleado2 Gome"];
                            fila2.Emple2_finAt_Gome = 0;
                        }

                        // Arrastrar valores que no se utilizan.

                        fila2.proxLlegada = fila1.proxLlegada;

                        fila2.Emple1_finAt_Surt = fila1.Emple1_finAt_Surt;
                        fila2.Emple1_Estado_Surt = fila1.Emple1_Estado_Surt;

                        fila2.Emple2_finAt_Surt = fila1.Emple2_finAt_Surt;
                        fila2.Emple2_Estado_Surt = fila1.Emple2_Estado_Surt;

                        fila2.Emple3_finAt_Surt = fila1.Emple3_finAt_Surt;
                        fila2.Emple3_Estado_Surt = fila1.Emple3_Estado_Surt;

                        fila2.Emple1_finAt_Gome = fila1.Emple1_finAt_Gome;
                        fila2.Emple1_Estado_Gome = fila1.Emple1_Estado_Gome;

                        fila2.Emple_finAt_Vent = fila1.Emple_finAt_Vent;
                        fila2.Emple_Estado_Vent = fila1.Emple_Estado_Vent;

                        fila2.RND_tipo1Servicio = 0;
                        fila2.tipo1Servicio = "";

                        break;


                    case "FinAt_Emple_Vent":

                        // Se destruye cliente ya atendido.

                        foreach (Temporal client in TodosLosClientes)
                        {
                            if (client.Estado == SiendoAtendidoVent && client.SiendoAtendidoPor == Empleado_Vent)
                            {   
                                fila2.Emple_finAt_Vent = 0;
                                client.DestruirCliente(client);
                                //fila2.TiempoEnSistema = fila1.TiempoEnSistema + (fila2.Reloj - client.HoraIngreso);
                                break;
                            }
                        }

                        // Si hay cola se atiende a la siguiente persona.

                        if (fila1.Cola_VentasAccesorios > 0)
                        {
                            var proxCliente = proximoCliente(TodosLosClientes);

                            fila2.RND_finAtencionGome = GenerarRND();

                            fila2.tiempoAtencionVent = VariableAleatoriaUniforme(MinTiempoVent, MaxTiempoVent, fila2.RND_finAtencionVent);
                            fila2.Emple_finAt_Vent = fila2.Reloj + fila2.tiempoAtencionVent;
                            fila2.Emple_Estado_Vent = estadosOcupado["Empleado Vent"];

                            //fila2.Emple2_finAt_Surt = fila1.Emple2_finAt_Surt;
                            //fila2.Emple2_Estado_Surt = fila1.Emple2_Estado_Surt;

                            //fila2.Emple3_finAt_Surt = fila1.Emple3_finAt_Surt;
                            //fila2.Emple3_Estado_Surt = fila1.Emple3_Estado_Surt;

                            proxCliente.Estado = SiendoAtendidoVent;
                            proxCliente.SiendoAtendidoPor = Empleado_Vent;

                            //proxCliente.EnFilaNumero = NumeroSimulacionActual;

                            fila2.Cola_VentasAccesorios = fila1.Cola_VentasAccesorios - 1;
                        }
                        else
                        {
                            fila2.Emple_Estado_Vent = estadosLibre["Empleado Vent"];
                            fila2.Emple_finAt_Vent = 0;
                        }

                        // Arrastrar valores que no se utilizan.

                        fila2.proxLlegada = fila1.proxLlegada;

                        fila2.Emple1_finAt_Surt = fila1.Emple1_finAt_Surt;
                        fila2.Emple1_Estado_Surt = fila1.Emple1_Estado_Surt;

                        fila2.Emple2_finAt_Surt = fila1.Emple2_finAt_Surt;
                        fila2.Emple2_Estado_Surt = fila1.Emple2_Estado_Surt;

                        fila2.Emple3_finAt_Surt = fila1.Emple3_finAt_Surt;
                        fila2.Emple3_Estado_Surt = fila1.Emple3_Estado_Surt;

                        fila2.Emple1_finAt_Gome = fila1.Emple1_finAt_Gome;
                        fila2.Emple1_Estado_Gome = fila1.Emple1_Estado_Gome;

                        fila2.Emple2_finAt_Gome = fila1.Emple2_finAt_Gome;
                        fila2.Emple2_Estado_Gome = fila1.Emple2_Estado_Gome;

                        fila2.RND_tipo1Servicio = 0;
                        fila2.tipo1Servicio = "";

                        break;
                }

                // La fila anterior pasa a tener los nuevos valores para repetir el proceso.

                fila1.Evento = fila2.Evento;
                fila1.Reloj = fila2.Reloj;
                
                fila1.tiempoEntreLlegadas = fila2.tiempoEntreLlegadas;
                fila1.proxLlegada = fila2.proxLlegada;

                fila1.RND_tipo1Servicio = fila2.RND_tipo1Servicio;
                fila1.tipo1Servicio = fila2.tipo1Servicio;
                fila1.RND_tipo2Servicio = fila2.RND_tipo2Servicio;
                fila1.tipo2Servicio = fila2.tipo2Servicio;

                fila1.RND_finAtencionSurt = fila2.RND_finAtencionSurt;
                fila1.tiempoAtencionSurt = fila2.tiempoAtencionSurt;

                fila1.Emple1_finAt_Surt = fila2.Emple1_finAt_Surt;
                fila1.Emple2_finAt_Surt = fila2.Emple2_finAt_Surt;
                fila1.Emple3_finAt_Surt = fila2.Emple3_finAt_Surt;

                fila1.Emple1_Estado_Surt = fila2.Emple1_Estado_Surt;
                fila1.Emple2_Estado_Surt = fila2.Emple2_Estado_Surt;
                fila1.Emple3_Estado_Surt = fila2.Emple3_Estado_Surt;

                fila1.RND_finAtencionGome = fila2.RND_finAtencionGome;
                fila1.tiempoAtencionGome = fila2.tiempoAtencionGome;

                fila1.Emple1_finAt_Gome = fila2.Emple1_finAt_Gome;
                fila1.Emple2_finAt_Gome = fila2.Emple2_finAt_Gome;

                fila1.Emple1_Estado_Gome = fila2.Emple1_Estado_Gome;
                fila1.Emple2_Estado_Gome = fila2.Emple2_Estado_Gome;

                fila1.RND_finAtencionVent = fila2.RND_finAtencionVent;
                fila1.tiempoAtencionVent = fila2.tiempoAtencionVent;

                fila1.Emple_finAt_Vent = fila2.Emple_finAt_Vent;
                fila1.Emple_Estado_Vent = fila2.Emple_Estado_Vent;

                fila1.Cola_Surtidor = fila2.Cola_Surtidor;
                fila1.Cola_Gomeria = fila2.Cola_Gomeria;
                fila1.Cola_VentasAccesorios = fila2.Cola_VentasAccesorios;

                fila1.ColaMax_Surt = fila2.ColaMax_Surt;
                fila1.ColaMax_Gome = fila2.ColaMax_Gome;
                fila1.ColaMax_Vent = fila2.ColaMax_Vent;

                
                //fila1.CantClientesVanSinComprar = fila2.CantClientesVanSinComprar;

                fila1.Persona = fila2.Persona;

                // Carga la fila en la grilla. Revisa que sólo se cargue lo seleccionado.

                if (iteracionesGrilla.Contains(i))
                {
                    AgregarFilaEnGrilla(fila2, proximoReloj);
                    CargarColumnasClientes();
                }
            }

            // Reactivar layout de la grilla al finalizar de actualizar.

            Grilla.ResumeLayout(false);
        }

        private HashSet<int> IteracionesParaGrilla()
        {
            // Agrega a un HashSet los valores de iteraciones que la grilla debería mostrar
            // (la inicialización que se añade arriba, valores desde y hasta del usuario y el último).

            HashSet<int> iteracionesGrilla = new HashSet<int>();

            for (int i = FilaDesde; i <= FilaHasta; ++i)
            {
                iteracionesGrilla.Add(i);
            }

            iteracionesGrilla.Add(CantidadSimulaciones);

            //MessageBox.Show(string.Join(", ", iteracionesGrilla));

            return iteracionesGrilla;
        }

        private void PrepararGrilla(DataGridView grilla)
        {
            // Mejorar el rendimiento de la grilla.

            grilla.Rows.Clear();
            grilla.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grilla.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            grilla.RowHeadersVisible = false;
            grilla.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(grilla, true, null);
            grilla.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        }

        private void MostrarFormulario(Form formulario, DataGridView grilla)
        {
            grilla.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grilla.RowHeadersVisible = true;
            formulario.Show();
        }

        private void AgregarFilaEnGrilla(Fila fila, double proximoReloj)
        {
            string reloj = Math.Round(proximoReloj, 2).ToString();

            int indiceFila = Grilla.Rows.Add
                (
                fila.Evento,
                Math.Round(fila.Reloj, 2),

                Math.Round(fila.tiempoEntreLlegadas, 2),
                Math.Round(fila.proxLlegada, 2),

                Math.Round(fila.RND_tipo1Servicio, 2),
                fila.tipo1Servicio,
                Math.Round(fila.RND_tipo2Servicio, 2),
                fila.tipo2Servicio,


                Math.Round(fila.RND_finAtencionSurt, 2),
                Math.Round(fila.tiempoAtencionSurt, 2),

                Math.Round(fila.Emple1_finAt_Surt, 2),
                fila.Emple1_Estado_Surt.Nombre,

                Math.Round(fila.Emple2_finAt_Surt, 2),
                fila.Emple2_Estado_Surt.Nombre,

                Math.Round(fila.Emple3_finAt_Surt, 2),
                fila.Emple3_Estado_Surt.Nombre,


                Math.Round(fila.RND_finAtencionGome, 2),
                Math.Round(fila.tiempoAtencionGome, 2),

                Math.Round(fila.Emple1_finAt_Gome, 2),
                fila.Emple1_Estado_Gome.Nombre,

                Math.Round(fila.Emple2_finAt_Gome, 2),
                fila.Emple2_Estado_Gome.Nombre,


                Math.Round(fila.RND_finAtencionVent, 2),
                Math.Round(fila.tiempoAtencionVent, 2),

                Math.Round(fila.Emple_finAt_Vent, 2),
                fila.Emple_Estado_Vent.Nombre,

                fila.Cola_Surtidor,
                fila.Cola_Gomeria,
                fila.Cola_VentasAccesorios,

                fila.ColaMax_Surt,
                fila.ColaMax_Gome,
                fila.ColaMax_Vent,
                 
                fila.CantClientesVanSinComprar = CantClientesVanSinComprar,
                fila.CantidadTotalClientes = CantidadTotalClientes
                //fila.CantClientesVanSinComprar = CantClientesVanSinComprar / CantidadTotalClientes
                );

            // Colorea de rojo el valor del próximo reloj.

            if (indiceFila >= 1)
            {
                DataGridViewRow filaAgregada = Grilla.Rows[indiceFila - 1];

                foreach (DataGridViewCell cell in filaAgregada.Cells)
                {
                    if (cell.ColumnIndex > 2 && cell.ColumnIndex < 32 && cell.Value != null && cell.Value.ToString() == reloj)
                    {
                        cell.Style.ForeColor = Color.Red;
                        break;
                    }
                }
            }
        }

        private void CargarColumnasClientes()
        {

            foreach (Temporal cliente in TodosLosClientes)
            {
                if (cliente.Estado.Nombre == "EsperandoSurtidor") { cliente.TipoResumido = "ES"; }

                if (cliente.Estado.Nombre == "EsperandoGomeria") { cliente.TipoResumido = "EG"; }

                if (cliente.Estado.Nombre == "EsperandoVentas") { cliente.TipoResumido = "EV"; }

                if (cliente.Estado.Nombre == "SiendoAtendidoSurt") { cliente.TipoResumido = "SAS"; }

                if (cliente.Estado.Nombre == "SiendoAtendidoGome") { cliente.TipoResumido = "SAG"; }

                if (cliente.Estado.Nombre == "SiendoAtendidoVent") { cliente.TipoResumido = "SAV"; }
            }

            foreach (Temporal cliente in TodosLosClientes)
            {

                int fila = NumeroSimulacionActual;

                // Si se diese el caso de que se intenta modificar una fila pasado el 'hasta' mostrar la grilla directamente.

                if (cliente.EnFilaNumero > FilaHasta && cliente.EnFilaNumero >= Grilla.RowCount - 1)
                {
                    return;
                }

                // Si el cliente es de tipo especial "Inicializacion" entonces llenar la fila de espacios vacios.

                if (cliente.Estado.Nombre == "Destruido" && cliente.EnFilaNumero == 0)
                {
                    for (int indiceColumna = 34; indiceColumna < Grilla.Columns.Count; ++indiceColumna)
                    {
                        Grilla.Rows[fila].Cells[indiceColumna].Value = null;
                    }
                    continue;
                }


                if (cliente.Estado.Nombre == "EsperandoSurtidor" || cliente.Estado.Nombre == "EsperandoGomeria" || cliente.Estado.Nombre == "EsperandoVentas" ||
                    cliente.Estado.Nombre == "SiendoAtendidoSurt" || cliente.Estado.Nombre == "SiendoAtendidoGome" || cliente.Estado.Nombre == "SiendoAtendidoVent")
                {

                    bool existente = false;
                    int indiceDeHallazgo = 0;

                    // Recorrer todas las columnas de clientes a ver si el cliente ya había sido añadido antes.

                    for (int indiceColumna = 34; indiceColumna < Grilla.Columns.Count; ++indiceColumna)
                    {
                        string valor = Grilla.Rows[fila - 1].Cells[indiceColumna].Value?.ToString() ?? string.Empty;

                        if (valor.Contains("Número: (" + cliente.Numero.ToString() + ")"))
                        {

                            // Si ya había sido añadido antes, entonces agregarlo en la columna encontrada, en la fila donde debería reflejarse la actualización de estado.

                            existente = true;

                            if (cliente.Estado.Nombre == "SiendoAtendidoSurt" || cliente.Estado.Nombre == "SiendoAtendidoGome" || cliente.Estado.Nombre == "SiendoAtendidoVent")
                            {
                                Grilla.Rows[fila].Cells[indiceColumna].Value =
                                            "Número: ("
                                            + cliente.Numero.ToString()
                                            + ") (" + cliente.TipoResumido + ") (" + cliente.Estado.Nombre + ") ("
                                            + cliente.SiendoAtendidoPor.Nombre + ") HI: "
                                            + cliente.HoraIngreso.ToString();
                                indiceDeHallazgo = indiceColumna;
                            }
                            else
                            {
                                Grilla.Rows[fila].Cells[indiceColumna].Value =
                                            "Número: ("
                                            + cliente.Numero.ToString()
                                            + ") (" + cliente.TipoResumido + ") (" + cliente.Estado.Nombre + ") ("
                                            + "Nadie" + ") HI: "
                                            + cliente.HoraIngreso.ToString();
                                indiceDeHallazgo = indiceColumna;
                            }
                            break;
                        }
                    }

                    // Si no lo encontró entonces el cliente no había sido añadido anteriormente, significa que es la primera vez y hay que agregarlo.

                    if (!existente)
                    {
                        bool añadido = false;

                        // Buscar el primer lugar con cliente destruido y agregarlo ahí.

                        for (int indiceColumna = 34; indiceColumna < Grilla.Columns.Count; ++indiceColumna)
                        {
                            string valor = Grilla.Rows[fila - 1].Cells[indiceColumna].Value?.ToString() ?? string.Empty;

                            if (valor.Contains("Destruido") && !añadido)
                            {
                                if (cliente.Estado.Nombre == "SiendoAtendidoSurt" || cliente.Estado.Nombre == "SiendoAtendidoGome" || cliente.Estado.Nombre == "SiendoAtendidoVent")
                                {
                                    Grilla.Rows[fila].Cells[indiceColumna].Value =
                                                "Número: ("
                                                + cliente.Numero.ToString()
                                                + ") (" + cliente.TipoResumido + ") (" + cliente.Estado.Nombre + ") ("
                                                + cliente.SiendoAtendidoPor.Nombre + ") HI: "
                                                + cliente.HoraIngreso.ToString();
                                    añadido = true;
                                }
                                else
                                {
                                    Grilla.Rows[fila].Cells[indiceColumna].Value =
                                                "Número: ("
                                                + cliente.Numero.ToString()
                                                + ") (" + cliente.TipoResumido + ") (" + cliente.Estado.Nombre + ") ("
                                                + "Nadie" + ") HI: "
                                                + cliente.HoraIngreso.ToString();
                                    añadido = true;
                                }
                            }
                            else
                            {
                                // Como no es destruido, significa que ese lugar pertenece a otro cliente, entonces arrastrar el valor de la fila anterior a la fila actual.

                                Grilla.Rows[fila].Cells[indiceColumna].Value = Grilla.Rows[fila - 1].Cells[indiceColumna].Value;
                            }
                        }

                        // Si no fue añadido hasta ahora, es porque no había clientes destruidos, entonces debe crear una nueva columna y añadirse al final.

                        if (añadido != true)
                        {
                            int indiceColumnaCliente = 0;

                            for (int indiceColumna = 34; indiceColumna < Grilla.Columns.Count; ++indiceColumna)
                            {
                                if (Grilla.Rows[fila].Cells[indiceColumna].Value == null)
                                {
                                    indiceColumnaCliente = indiceColumna;
                                    break;
                                }
                            }

                            if (indiceColumnaCliente == 0)
                            {
                                DataGridViewColumn nuevaColumna = new DataGridViewTextBoxColumn();
                                nuevaColumna.Name = "Cliente";
                                Grilla.Columns.Add(nuevaColumna);

                                int indiceColumnaNueva = Grilla.Columns.Count - 1;


                                if (cliente.Estado.Nombre == "SiendoAtendidoSurt" || cliente.Estado.Nombre == "SiendoAtendidoGome" || cliente.Estado.Nombre == "SiendoAtendidoVent")
                                {
                                    Grilla.Rows[fila].Cells[indiceColumnaNueva].Value =
                                                "Número: ("
                                                + cliente.Numero.ToString()
                                                + ") (" + cliente.TipoResumido + ") (" + cliente.Estado.Nombre + ") ("
                                                + cliente.SiendoAtendidoPor.Nombre + ") HI: "
                                                + cliente.HoraIngreso.ToString();
                                }
                                else
                                {
                                    Grilla.Rows[fila].Cells[indiceColumnaNueva].Value =
                                                "Número: ("
                                                + cliente.Numero.ToString()
                                                + ") (" + cliente.TipoResumido + ") (" + cliente.Estado.Nombre + ") ("
                                                + "Nadie" + ") HI: "
                                                + cliente.HoraIngreso.ToString();
                                }

                                continue;
                            }

                            if (cliente.Estado.Nombre == "SiendoAtendidoSurt" || cliente.Estado.Nombre == "SiendoAtendidoGome" || cliente.Estado.Nombre == "SiendoAtendidoVent")
                            {
                                Grilla.Rows[fila].Cells[indiceColumnaCliente].Value =
                                            "Número: ("
                                            + cliente.Numero.ToString()
                                            + ") (" + cliente.TipoResumido + ") (" + cliente.Estado.Nombre + ") ("
                                            + cliente.SiendoAtendidoPor.Nombre + ") HI: "
                                            + cliente.HoraIngreso.ToString();
                            }
                            else
                            {
                                Grilla.Rows[fila].Cells[indiceColumnaCliente].Value =
                                            "Número: ("
                                            + cliente.Numero.ToString()
                                            + ") (" + cliente.TipoResumido + ") (" + cliente.Estado.Nombre + ") ("
                                            + "Nadie" + ") HI: "
                                            + cliente.HoraIngreso.ToString();
                            }
                        }

                    }
                }

                // Si el cliente que hay en lista es un cambio de estado a destruido, debe buscar donde estaba.

                if (cliente.Estado.Nombre == "Destruido")
                {
                    for (int indiceColumna = 34; indiceColumna < Grilla.Columns.Count; ++indiceColumna)
                    {
                        if (fila > 0)
                        {
                            string valor = Grilla.Rows[fila - 1].Cells[indiceColumna].Value?.ToString() ?? string.Empty;

                            if (valor.Contains("Número: (" + cliente.Numero.ToString() + ")"))
                            {
                                Console.WriteLine("Estado del cliente: " + cliente.Estado.Nombre);
                                Grilla.Rows[fila].Cells[indiceColumna].Value =
                                                "Número: ("
                                                + cliente.Numero.ToString()
                                                + ") (" + cliente.TipoResumido + ") (" + cliente.Estado.Nombre + ") ("
                                                + cliente.SiendoAtendidoPor.Nombre + ") HI: "
                                                + cliente.HoraIngreso.ToString();

                                // Cambiar el color a rojo.

                            }
                        }

                    }
                }
                /*
                // Si sucede que el objeto se destruye pero se muestra con un estado que no es, cambiarlo a destruido.
                
                if (true)
                {
                    for (int indiceColumna = 30; indiceColumna < Grilla.Columns.Count; ++indiceColumna)
                    {
                        if (Grilla.Rows[fila].Cells[indiceColumna].Style.BackColor == System.Drawing.Color.Red)
                        {
                            string textoActual = Grilla.Rows[fila].Cells[indiceColumna].Value.ToString();
                            string textoModificado = "";

                            if (textoActual.Contains("SiendoAtendidoSurt"))
                            {
                                textoModificado = textoActual.Replace("SiendoAtendidoSurt", "Destruido");
                            }

                            else if (textoActual.Contains("SiendoAtendidoGome"))
                            {
                                textoModificado = textoActual.Replace("SiendoAtendidoGome", "Destruido");
                            }

                            else if (textoActual.Contains("SiendoAtendidoVent"))
                            {
                                textoModificado = textoActual.Replace("SiendoAtendidoVent", "Destruido");
                            }
                            
                            else if (textoActual.Contains("EsperandoSurtidor"))
                            {
                                textoModificado = textoActual.Replace("EsperandoSurtidor", "Destruido");
                            }

                            else if (textoActual.Contains("EsperandoGomeria"))
                            {
                                textoModificado = textoActual.Replace("EsperandoGomeria", "Destruido");
                            }

                            else if (textoActual.Contains("EsperandoSurtidor"))
                            {
                                textoModificado = textoActual.Replace("EsperandoGomeria", "Destruido");
                            }

                            Grilla.Rows[fila].Cells[indiceColumna].Value = textoModificado;
                        }

                    }
                }
                */

            }
        }

        /*
        private void CargarColumnasClientes()
        {
            foreach (Temporal cliente in TodosLosClientes)
            {
                if (cliente.Estado.Nombre == "EsperandoSurtidor") { cliente.TipoResumido = "ES"; }

                if (cliente.Estado.Nombre == "EsperandoGomeria") { cliente.TipoResumido = "EG"; }

                if (cliente.Estado.Nombre == "EsperandoVentas") { cliente.TipoResumido = "EV"; }

                if (cliente.Estado.Nombre == "SiendoAtendidosSurt") { cliente.TipoResumido = "SAS"; }

                if (cliente.Estado.Nombre == "SiendoAtendidoGome") { cliente.TipoResumido = "SAG"; }

                if (cliente.Estado.Nombre == "SiendoAtendidoVent") { cliente.TipoResumido = "SAV"; }
            }

            foreach (Temporal cliente in TodosLosClientes)
            {

                int fila = NumeroSimulacionActual;

                // Si se diese el caso de que se intenta modificar una fila pasado el 'hasta' mostrar la grilla directamente.

                if (cliente.EnFilaNumero > FilaHasta && cliente.EnFilaNumero >= Grilla.RowCount - 1)
                {
                    return;
                }

                // Si el cliente es de tipo especial "Inicializacion" entonces llenar la fila de espacios vacios.

                if (cliente.Estado.Nombre == "Destruido" && cliente.EnFilaNumero == 0)
                {
                    for (int indiceColumna = 30; indiceColumna < Grilla.Columns.Count; ++indiceColumna)
                    {
                        Grilla.Rows[fila].Cells[indiceColumna].Value = null;
                    }
                    continue;
                }


                if (cliente.Estado.Nombre == "EsperandoSurtidor" || cliente.Estado.Nombre == "EsperandoGomeria" || cliente.Estado.Nombre == "EsperandoVentas" || 
                    cliente.Estado.Nombre == "SiendoAtendidosSurt" || cliente.Estado.Nombre == "SiendoAtendidoGome" || cliente.Estado.Nombre == "SiendoAtendidoVent")
                {

                    bool existente = false;
                    int indiceDeHallazgo = 0;

                    // Recorrer todas las columnas de clientes a ver si el cliente ya había sido añadido antes.

                    for (int indiceColumna = 30; indiceColumna < Grilla.Columns.Count; ++indiceColumna)
                    {
                        string valor = Grilla.Rows[fila - 1].Cells[indiceColumna].Value?.ToString() ?? string.Empty;

                        if (valor.Contains("Número: (" + cliente.Numero.ToString() + ")"))
                        {

                            // Si ya había sido añadido antes, entonces agregarlo en la columna encontrada, en la fila donde debería reflejarse la actualización de estado.

                            existente = true;
  
                            if (cliente.Estado.Nombre == "SiendoAtendidoSurt")
                            {
                                Grilla.Rows[fila].Cells[indiceColumna].Value =
                                            "Número: ("
                                            + cliente.Numero.ToString()
                                            + ") (" + cliente.TipoResumido + ") (" + cliente.Estado.Nombre + ") ("
                                            + cliente.SiendoAtendidoPor.Nombre + ") HI: "
                                            + cliente.HoraIngreso.ToString();
                                indiceDeHallazgo = indiceColumna;
                            }
                            else if (cliente.Estado.Nombre == "SiendoAtendidoGome")
                            {
                                Grilla.Rows[fila].Cells[indiceColumna].Value =
                                            "Número: ("
                                            + cliente.Numero.ToString()
                                            + ") (" + cliente.TipoResumido + ") (" + cliente.Estado.Nombre + ") ("
                                            + cliente.SiendoAtendidoPor.Nombre + ") HI: "
                                            + cliente.HoraIngreso.ToString();
                                indiceDeHallazgo = indiceColumna;
                            }
                            else if (cliente.Estado.Nombre == "SiendoAtendidoVent")
                            {
                                Grilla.Rows[fila].Cells[indiceColumna].Value =
                                            "Número: ("
                                            + cliente.Numero.ToString()
                                            + ") (" + cliente.TipoResumido + ") (" + cliente.Estado.Nombre + ") ("
                                            + cliente.SiendoAtendidoPor.Nombre + ") HI: "
                                            + cliente.HoraIngreso.ToString();
                                indiceDeHallazgo = indiceColumna;
                            }
                            
                            else
                            {
                                Grilla.Rows[fila].Cells[indiceColumna].Value =
                                            "Número: ("
                                            + cliente.Numero.ToString()
                                            + ") (" + cliente.TipoResumido + ") (" + cliente.Estado.Nombre + ") ("
                                            + "Nadie" + ") HI: "
                                            + cliente.HoraIngreso.ToString();
                                indiceDeHallazgo = indiceColumna;
                            }
                            break;
                        }
                    }

                    // Si no lo encontró entonces el cliente no había sido añadido anteriormente, significa que es la primera vez y hay que agregarlo.

                    if (!existente)
                    {
                        bool añadido = false;

                        // Buscar el primer lugar con cliente destruido y agregarlo ahí.

                        for (int indiceColumna = 30; indiceColumna < Grilla.Columns.Count; ++indiceColumna)
                        {
                            string valor = Grilla.Rows[fila - 1].Cells[indiceColumna].Value?.ToString() ?? string.Empty;

                            if (valor.Contains("Destruido") && !añadido)
                            {

                                if (cliente.Estado.Nombre == "SiendoAtendidoSurt")
                                {
                                    Grilla.Rows[fila].Cells[indiceColumna].Value =
                                                "Número: ("
                                                + cliente.Numero.ToString()
                                                + ") (" + cliente.TipoResumido + ") (" + cliente.Estado.Nombre + ") ("
                                                + cliente.SiendoAtendidoPor.Nombre + ") HI: "
                                                + cliente.HoraIngreso.ToString();
                                    indiceDeHallazgo = indiceColumna;
                                }
                                else if (cliente.Estado.Nombre == "SiendoAtendidoGome")
                                {
                                    Grilla.Rows[fila].Cells[indiceColumna].Value =
                                                "Número: ("
                                                + cliente.Numero.ToString()
                                                + ") (" + cliente.TipoResumido + ") (" + cliente.Estado.Nombre + ") ("
                                                + cliente.SiendoAtendidoPor.Nombre + ") HI: "
                                                + cliente.HoraIngreso.ToString();
                                    indiceDeHallazgo = indiceColumna;
                                }
                                else if (cliente.Estado.Nombre == "SiendoAtendidoVent")
                                {
                                    Grilla.Rows[fila].Cells[indiceColumna].Value =
                                                "Número: ("
                                                + cliente.Numero.ToString()
                                                + ") (" + cliente.TipoResumido + ") (" + cliente.Estado.Nombre + ") ("
                                                + cliente.SiendoAtendidoPor.Nombre + ") HI: "
                                                + cliente.HoraIngreso.ToString();
                                    indiceDeHallazgo = indiceColumna;
                                }
                              
                                else
                                {
                                    Grilla.Rows[fila].Cells[indiceColumna].Value =
                                                "Número: ("
                                                + cliente.Numero.ToString()
                                                + ") (" + cliente.TipoResumido + ") (" + cliente.Estado.Nombre + ") ("
                                                + "Nadie" + ") HI: "
                                                + cliente.HoraIngreso.ToString();
                                    indiceDeHallazgo = indiceColumna;
                                }
                            }
                            else
                            {
                                // Como no es destruido, significa que ese lugar pertenece a otro cliente, entonces arrastrar el valor de la fila anterior a la fila actual.

                                Grilla.Rows[fila].Cells[indiceColumna].Value = Grilla.Rows[fila - 1].Cells[indiceColumna].Value;
                            }
                        }

                        // Si no fue añadido hasta ahora, es porque no había clientes destruidos, entonces debe crear una nueva columna y añadirse al final.

                        if (añadido != true)
                        {
                            int indiceColumnaCliente = 0;

                            for (int indiceColumna = 30; indiceColumna < Grilla.Columns.Count; ++indiceColumna)
                            {
                                if (Grilla.Rows[fila].Cells[indiceColumna].Value == null)
                                {
                                    indiceColumnaCliente = indiceColumna;
                                    break;
                                }
                            }

                            if (indiceColumnaCliente == 0)
                            {
                                DataGridViewColumn nuevaColumna = new DataGridViewTextBoxColumn();
                                nuevaColumna.Name = "Cliente";
                                Grilla.Columns.Add(nuevaColumna);

                                int indiceColumnaNueva = Grilla.Columns.Count - 1;
           
                                if (cliente.Estado.Nombre == "SiendoAtendidoSurt")
                                {
                                    Grilla.Rows[fila].Cells[indiceColumnaNueva].Value =
                                                "Número: ("
                                                + cliente.Numero.ToString()
                                                + ") (" + cliente.TipoResumido + ") (" + cliente.Estado.Nombre + ") ("
                                                + cliente.SiendoAtendidoPor.Nombre + ") HI: "
                                                + cliente.HoraIngreso.ToString();
                                    indiceDeHallazgo = indiceColumnaNueva;
                                }
                                else if (cliente.Estado.Nombre == "SiendoAtendidoGome")
                                {
                                    Grilla.Rows[fila].Cells[indiceColumnaNueva].Value =
                                                "Número: ("
                                                + cliente.Numero.ToString()
                                                + ") (" + cliente.TipoResumido + ") (" + cliente.Estado.Nombre + ") ("
                                                + cliente.SiendoAtendidoPor.Nombre + ") HI: "
                                                + cliente.HoraIngreso.ToString();
                                    indiceDeHallazgo = indiceColumnaNueva;
                                }
                                else if (cliente.Estado.Nombre == "SiendoAtendidoVent")
                                {
                                    Grilla.Rows[fila].Cells[indiceColumnaNueva].Value =
                                                "Número: ("
                                                + cliente.Numero.ToString()
                                                + ") (" + cliente.TipoResumido + ") (" + cliente.Estado.Nombre + ") ("
                                                + cliente.SiendoAtendidoPor.Nombre + ") HI: "
                                                + cliente.HoraIngreso.ToString();
                                    indiceDeHallazgo = indiceColumnaNueva;
                                }
                               
                                else
                                {
                                    Grilla.Rows[fila].Cells[indiceColumnaNueva].Value =
                                                "Número: ("
                                                + cliente.Numero.ToString()
                                                + ") (" + cliente.TipoResumido + ") (" + cliente.Estado.Nombre + ") ("
                                                + "Nadie" + ") HI: "
                                                + cliente.HoraIngreso.ToString();
                                    indiceDeHallazgo = indiceColumnaNueva;
                                }

                                continue;
                            }
                     
                            if (cliente.Estado.Nombre == "SiendoAtendidoSurt")
                            {
                                Grilla.Rows[fila].Cells[indiceColumnaCliente].Value =
                                            "Número: ("
                                            + cliente.Numero.ToString()
                                            + ") (" + cliente.TipoResumido + ") (" + cliente.Estado.Nombre + ") ("
                                            + cliente.SiendoAtendidoPor.Nombre + ") HI: "
                                            + cliente.HoraIngreso.ToString();
                                indiceDeHallazgo = indiceColumnaCliente;
                            }
                            else if (cliente.Estado.Nombre == "SiendoAtendidoGome")
                            {
                                Grilla.Rows[fila].Cells[indiceColumnaCliente].Value =
                                            "Número: ("
                                            + cliente.Numero.ToString()
                                            + ") (" + cliente.TipoResumido + ") (" + cliente.Estado.Nombre + ") ("
                                            + cliente.SiendoAtendidoPor.Nombre + ") HI: "
                                            + cliente.HoraIngreso.ToString();
                                indiceDeHallazgo = indiceColumnaCliente;
                            }
                            else if (cliente.Estado.Nombre == "SiendoAtendidoVent")
                            {
                                Grilla.Rows[fila].Cells[indiceColumnaCliente].Value =
                                            "Número: ("
                                            + cliente.Numero.ToString()
                                            + ") (" + cliente.TipoResumido + ") (" + cliente.Estado.Nombre + ") ("
                                            + cliente.SiendoAtendidoPor.Nombre + ") HI: "
                                            + cliente.HoraIngreso.ToString();
                                indiceDeHallazgo = indiceColumnaCliente;
                            }
                         
                            else
                            {
                                Grilla.Rows[fila].Cells[indiceColumnaCliente].Value =
                                            "Número: ("
                                            + cliente.Numero.ToString()
                                            + ") (" + cliente.TipoResumido + ") (" + cliente.Estado.Nombre + ") ("
                                            + "Nadie" + ") HI: "
                                            + cliente.HoraIngreso.ToString();
                                indiceDeHallazgo = indiceColumnaCliente;

                            }

                        }

                    }
                }

                // Si el cliente que hay en lista es un cambio de estado a destruido, debe buscar donde estaba.

                if (cliente.Estado.Nombre == "Destruido")
                {
                    for (int indiceColumna = 30; indiceColumna < Grilla.Columns.Count; ++indiceColumna)
                    {
                        if (fila > 0)
                        {
                            string valor = Grilla.Rows[fila - 1].Cells[indiceColumna].Value?.ToString() ?? string.Empty;

                            if (valor.Contains("Número: (" + cliente.Numero.ToString() + ")") && valor.Contains("(" + cliente.TipoResumido.ToString() + ")"))
                            {
                                Console.WriteLine("Estado del cliente: " + cliente.Estado.Nombre);
                                Grilla.Rows[fila].Cells[indiceColumna].Value =
                                                "Número: ("
                                                + cliente.Numero.ToString()
                                                + ") (" + cliente.TipoResumido + ") (" + cliente.Estado.Nombre + ") ("
                                                + cliente.SiendoAtendidoPor.Nombre + ") HI: "
                                                + cliente.HoraIngreso.ToString();

                                // Cambiar el color a rojo.


                            }
                            
                        }

                    }
                }

                // Si sucede que el objeto se destruye pero se muestra con un estado que no es, cambiarlo a destruido.
                
                if (true)
                {
                    for (int indiceColumna = 30; indiceColumna < Grilla.Columns.Count; ++indiceColumna)
                    {
                        if (Grilla.Rows[fila].Cells[indiceColumna].Style.BackColor == System.Drawing.Color.Red)
                        {
                            string textoActual = Grilla.Rows[fila].Cells[indiceColumna].Value.ToString();
                            string textoModificado = "";
                  
                            if (textoActual.Contains("SiendoAtendidoSurt"))
                            {
                                textoModificado = textoActual.Replace("SiendoAtendidoSurt", "Destruido");
                            }

                            else if (textoActual.Contains("SiendoAtendidoGome"))
                            {
                                textoModificado = textoActual.Replace("SiendoAtendidoGome", "Destruido");
                            }

                            else if (textoActual.Contains("SiendoAtendidoVent"))
                            {
                                textoModificado = textoActual.Replace("SiendoAtendidoVent", "Destruido");
                            }
           
                            Grilla.Rows[fila].Cells[indiceColumna].Value = textoModificado;
                        }
                    }
                }  
                
            }

        }
        */

    }
}



