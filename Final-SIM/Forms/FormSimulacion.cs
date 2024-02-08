using Final_SIM.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_SIM.Forms
{
    public partial class FormSimulacion : Form
    {
        public FormSimulacion()
        {
            InitializeComponent();
            
        }

        public DataGridView DevolverGrilla()
        {
            return this.dgvGrillaSimulacion;
        }

        

        public void ActualizarPorcentajeClientesVanSinComprar()
        {
            Simulacion simulacion = new Simulacion();
            int porcentaje = simulacion.CantClientesVanSinComprar / simulacion.CantidadTotalClientes;
            lblPorcentaje.Text = porcentaje.ToString();
            //lblPorcentaje.Text = "Hola";
           
        }

        private void Metricas_Click(object sender, EventArgs e)
        {
            ActualizarPorcentajeClientesVanSinComprar();
        }
    }
}
