namespace Final_SIM.Forms
{
    partial class FormSimulacion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvGrillaSimulacion = new System.Windows.Forms.DataGridView();
            this.lblPorcentaje = new System.Windows.Forms.Label();
            this.Metricas = new System.Windows.Forms.Button();
            this.Evento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Reloj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tiempoEntreLlegadas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.proxLlegada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RND_tipo1Servicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tipo1Servicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RND_tipo2Servicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tipo2Servicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RND_finAtencionSurt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tiempoAtencionSurt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Emple1_finAt_Surt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Emple1_Estado_Surt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Emple2_finAt_Surt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Emple2_Estado_Surt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Emple3_finAt_Surt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Emple3_Estado_Surt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RND_finAtencionGome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tiempoAtencionGome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Emple1_finAt_Gome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Emple1_Estado_Gome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Emple2_finAt_Gome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Emple2_Estado_Gome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RND_finAtencionVent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tiempoAtencionVent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Emple_finAt_Vent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Emple_Estado_Vent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cola_Surtidor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cola_Gomeria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cola_VentasAccesorios = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColaMax_Surt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColaMax_Gome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColaMax_Vent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CantClientesVanSinComprar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CantidadTotalClientes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrillaSimulacion)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvGrillaSimulacion
            // 
            this.dgvGrillaSimulacion.AllowUserToAddRows = false;
            this.dgvGrillaSimulacion.AllowUserToDeleteRows = false;
            this.dgvGrillaSimulacion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGrillaSimulacion.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Evento,
            this.Reloj,
            this.tiempoEntreLlegadas,
            this.proxLlegada,
            this.RND_tipo1Servicio,
            this.tipo1Servicio,
            this.RND_tipo2Servicio,
            this.tipo2Servicio,
            this.RND_finAtencionSurt,
            this.tiempoAtencionSurt,
            this.Emple1_finAt_Surt,
            this.Emple1_Estado_Surt,
            this.Emple2_finAt_Surt,
            this.Emple2_Estado_Surt,
            this.Emple3_finAt_Surt,
            this.Emple3_Estado_Surt,
            this.RND_finAtencionGome,
            this.tiempoAtencionGome,
            this.Emple1_finAt_Gome,
            this.Emple1_Estado_Gome,
            this.Emple2_finAt_Gome,
            this.Emple2_Estado_Gome,
            this.RND_finAtencionVent,
            this.tiempoAtencionVent,
            this.Emple_finAt_Vent,
            this.Emple_Estado_Vent,
            this.Cola_Surtidor,
            this.Cola_Gomeria,
            this.Cola_VentasAccesorios,
            this.ColaMax_Surt,
            this.ColaMax_Gome,
            this.ColaMax_Vent,
            this.CantClientesVanSinComprar,
            this.CantidadTotalClientes});
            this.dgvGrillaSimulacion.Location = new System.Drawing.Point(12, 12);
            this.dgvGrillaSimulacion.Name = "dgvGrillaSimulacion";
            this.dgvGrillaSimulacion.ReadOnly = true;
            this.dgvGrillaSimulacion.RowHeadersWidth = 51;
            this.dgvGrillaSimulacion.RowTemplate.Height = 24;
            this.dgvGrillaSimulacion.Size = new System.Drawing.Size(1900, 643);
            this.dgvGrillaSimulacion.TabIndex = 0;
            // 
            // lblPorcentaje
            // 
            this.lblPorcentaje.AutoSize = true;
            this.lblPorcentaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPorcentaje.Location = new System.Drawing.Point(42, 671);
            this.lblPorcentaje.Name = "lblPorcentaje";
            this.lblPorcentaje.Size = new System.Drawing.Size(151, 32);
            this.lblPorcentaje.TabIndex = 1;
            this.lblPorcentaje.Text = "Porcentaje";
            // 
            // Metricas
            // 
            this.Metricas.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Metricas.Location = new System.Drawing.Point(222, 671);
            this.Metricas.Name = "Metricas";
            this.Metricas.Size = new System.Drawing.Size(133, 57);
            this.Metricas.TabIndex = 2;
            this.Metricas.Text = "Metricas";
            this.Metricas.UseVisualStyleBackColor = true;
            this.Metricas.Click += new System.EventHandler(this.Metricas_Click);
            // 
            // Evento
            // 
            this.Evento.HeaderText = "Evento";
            this.Evento.MinimumWidth = 6;
            this.Evento.Name = "Evento";
            this.Evento.ReadOnly = true;
            this.Evento.Width = 125;
            // 
            // Reloj
            // 
            this.Reloj.HeaderText = "Reloj";
            this.Reloj.MinimumWidth = 6;
            this.Reloj.Name = "Reloj";
            this.Reloj.ReadOnly = true;
            this.Reloj.Width = 125;
            // 
            // tiempoEntreLlegadas
            // 
            this.tiempoEntreLlegadas.HeaderText = "tiempoEntreLlegadas";
            this.tiempoEntreLlegadas.MinimumWidth = 6;
            this.tiempoEntreLlegadas.Name = "tiempoEntreLlegadas";
            this.tiempoEntreLlegadas.ReadOnly = true;
            this.tiempoEntreLlegadas.Width = 125;
            // 
            // proxLlegada
            // 
            this.proxLlegada.HeaderText = "proxLlegada";
            this.proxLlegada.MinimumWidth = 6;
            this.proxLlegada.Name = "proxLlegada";
            this.proxLlegada.ReadOnly = true;
            this.proxLlegada.Width = 125;
            // 
            // RND_tipo1Servicio
            // 
            this.RND_tipo1Servicio.HeaderText = "RND_tipo1Servicio";
            this.RND_tipo1Servicio.MinimumWidth = 6;
            this.RND_tipo1Servicio.Name = "RND_tipo1Servicio";
            this.RND_tipo1Servicio.ReadOnly = true;
            this.RND_tipo1Servicio.Width = 125;
            // 
            // tipo1Servicio
            // 
            this.tipo1Servicio.HeaderText = "tipo1Servicio";
            this.tipo1Servicio.MinimumWidth = 6;
            this.tipo1Servicio.Name = "tipo1Servicio";
            this.tipo1Servicio.ReadOnly = true;
            this.tipo1Servicio.Width = 125;
            // 
            // RND_tipo2Servicio
            // 
            this.RND_tipo2Servicio.HeaderText = "RND_tipo2Servicio";
            this.RND_tipo2Servicio.MinimumWidth = 6;
            this.RND_tipo2Servicio.Name = "RND_tipo2Servicio";
            this.RND_tipo2Servicio.ReadOnly = true;
            this.RND_tipo2Servicio.Width = 125;
            // 
            // tipo2Servicio
            // 
            this.tipo2Servicio.HeaderText = "tipo2Servicio";
            this.tipo2Servicio.MinimumWidth = 6;
            this.tipo2Servicio.Name = "tipo2Servicio";
            this.tipo2Servicio.ReadOnly = true;
            this.tipo2Servicio.Width = 125;
            // 
            // RND_finAtencionSurt
            // 
            this.RND_finAtencionSurt.HeaderText = "RND_finAtencionSurt";
            this.RND_finAtencionSurt.MinimumWidth = 6;
            this.RND_finAtencionSurt.Name = "RND_finAtencionSurt";
            this.RND_finAtencionSurt.ReadOnly = true;
            this.RND_finAtencionSurt.Width = 125;
            // 
            // tiempoAtencionSurt
            // 
            this.tiempoAtencionSurt.HeaderText = "tiempoAtencionSurt";
            this.tiempoAtencionSurt.MinimumWidth = 6;
            this.tiempoAtencionSurt.Name = "tiempoAtencionSurt";
            this.tiempoAtencionSurt.ReadOnly = true;
            this.tiempoAtencionSurt.Width = 125;
            // 
            // Emple1_finAt_Surt
            // 
            this.Emple1_finAt_Surt.HeaderText = "Emple1_finAt_Surt";
            this.Emple1_finAt_Surt.MinimumWidth = 6;
            this.Emple1_finAt_Surt.Name = "Emple1_finAt_Surt";
            this.Emple1_finAt_Surt.ReadOnly = true;
            this.Emple1_finAt_Surt.Width = 125;
            // 
            // Emple1_Estado_Surt
            // 
            this.Emple1_Estado_Surt.HeaderText = "Emple1_Estado_Surt";
            this.Emple1_Estado_Surt.MinimumWidth = 6;
            this.Emple1_Estado_Surt.Name = "Emple1_Estado_Surt";
            this.Emple1_Estado_Surt.ReadOnly = true;
            this.Emple1_Estado_Surt.Width = 125;
            // 
            // Emple2_finAt_Surt
            // 
            this.Emple2_finAt_Surt.HeaderText = "Emple2_finAt_Surt";
            this.Emple2_finAt_Surt.MinimumWidth = 6;
            this.Emple2_finAt_Surt.Name = "Emple2_finAt_Surt";
            this.Emple2_finAt_Surt.ReadOnly = true;
            this.Emple2_finAt_Surt.Width = 125;
            // 
            // Emple2_Estado_Surt
            // 
            this.Emple2_Estado_Surt.HeaderText = "Emple2_Estado_Surt";
            this.Emple2_Estado_Surt.MinimumWidth = 6;
            this.Emple2_Estado_Surt.Name = "Emple2_Estado_Surt";
            this.Emple2_Estado_Surt.ReadOnly = true;
            this.Emple2_Estado_Surt.Width = 125;
            // 
            // Emple3_finAt_Surt
            // 
            this.Emple3_finAt_Surt.HeaderText = "Emple3_finAt_Surt";
            this.Emple3_finAt_Surt.MinimumWidth = 6;
            this.Emple3_finAt_Surt.Name = "Emple3_finAt_Surt";
            this.Emple3_finAt_Surt.ReadOnly = true;
            this.Emple3_finAt_Surt.Width = 125;
            // 
            // Emple3_Estado_Surt
            // 
            this.Emple3_Estado_Surt.HeaderText = "Emple3_Estado_Surt";
            this.Emple3_Estado_Surt.MinimumWidth = 6;
            this.Emple3_Estado_Surt.Name = "Emple3_Estado_Surt";
            this.Emple3_Estado_Surt.ReadOnly = true;
            this.Emple3_Estado_Surt.Width = 125;
            // 
            // RND_finAtencionGome
            // 
            this.RND_finAtencionGome.HeaderText = "RND_finAtencionGome";
            this.RND_finAtencionGome.MinimumWidth = 6;
            this.RND_finAtencionGome.Name = "RND_finAtencionGome";
            this.RND_finAtencionGome.ReadOnly = true;
            this.RND_finAtencionGome.Width = 125;
            // 
            // tiempoAtencionGome
            // 
            this.tiempoAtencionGome.HeaderText = "tiempoAtencionGome";
            this.tiempoAtencionGome.MinimumWidth = 6;
            this.tiempoAtencionGome.Name = "tiempoAtencionGome";
            this.tiempoAtencionGome.ReadOnly = true;
            this.tiempoAtencionGome.Width = 125;
            // 
            // Emple1_finAt_Gome
            // 
            this.Emple1_finAt_Gome.HeaderText = "Emple1_finAt_Gome";
            this.Emple1_finAt_Gome.MinimumWidth = 6;
            this.Emple1_finAt_Gome.Name = "Emple1_finAt_Gome";
            this.Emple1_finAt_Gome.ReadOnly = true;
            this.Emple1_finAt_Gome.Width = 125;
            // 
            // Emple1_Estado_Gome
            // 
            this.Emple1_Estado_Gome.HeaderText = "Emple1_Estado_Gome";
            this.Emple1_Estado_Gome.MinimumWidth = 6;
            this.Emple1_Estado_Gome.Name = "Emple1_Estado_Gome";
            this.Emple1_Estado_Gome.ReadOnly = true;
            this.Emple1_Estado_Gome.Width = 125;
            // 
            // Emple2_finAt_Gome
            // 
            this.Emple2_finAt_Gome.HeaderText = "Emple2_finAt_Gome";
            this.Emple2_finAt_Gome.MinimumWidth = 6;
            this.Emple2_finAt_Gome.Name = "Emple2_finAt_Gome";
            this.Emple2_finAt_Gome.ReadOnly = true;
            this.Emple2_finAt_Gome.Width = 125;
            // 
            // Emple2_Estado_Gome
            // 
            this.Emple2_Estado_Gome.HeaderText = "Emple2_Estado_Gome";
            this.Emple2_Estado_Gome.MinimumWidth = 6;
            this.Emple2_Estado_Gome.Name = "Emple2_Estado_Gome";
            this.Emple2_Estado_Gome.ReadOnly = true;
            this.Emple2_Estado_Gome.Width = 125;
            // 
            // RND_finAtencionVent
            // 
            this.RND_finAtencionVent.HeaderText = "RND_finAtencionVent";
            this.RND_finAtencionVent.MinimumWidth = 6;
            this.RND_finAtencionVent.Name = "RND_finAtencionVent";
            this.RND_finAtencionVent.ReadOnly = true;
            this.RND_finAtencionVent.Width = 125;
            // 
            // tiempoAtencionVent
            // 
            this.tiempoAtencionVent.HeaderText = "tiempoAtencionVent";
            this.tiempoAtencionVent.MinimumWidth = 6;
            this.tiempoAtencionVent.Name = "tiempoAtencionVent";
            this.tiempoAtencionVent.ReadOnly = true;
            this.tiempoAtencionVent.Width = 125;
            // 
            // Emple_finAt_Vent
            // 
            this.Emple_finAt_Vent.HeaderText = "Emple_finAt_Vent";
            this.Emple_finAt_Vent.MinimumWidth = 6;
            this.Emple_finAt_Vent.Name = "Emple_finAt_Vent";
            this.Emple_finAt_Vent.ReadOnly = true;
            this.Emple_finAt_Vent.Width = 125;
            // 
            // Emple_Estado_Vent
            // 
            this.Emple_Estado_Vent.HeaderText = "Emple_Estado_Vent";
            this.Emple_Estado_Vent.MinimumWidth = 6;
            this.Emple_Estado_Vent.Name = "Emple_Estado_Vent";
            this.Emple_Estado_Vent.ReadOnly = true;
            this.Emple_Estado_Vent.Width = 125;
            // 
            // Cola_Surtidor
            // 
            this.Cola_Surtidor.HeaderText = "Cola_Surtidor";
            this.Cola_Surtidor.MinimumWidth = 6;
            this.Cola_Surtidor.Name = "Cola_Surtidor";
            this.Cola_Surtidor.ReadOnly = true;
            this.Cola_Surtidor.Width = 125;
            // 
            // Cola_Gomeria
            // 
            this.Cola_Gomeria.HeaderText = "Cola_Gomeria";
            this.Cola_Gomeria.MinimumWidth = 6;
            this.Cola_Gomeria.Name = "Cola_Gomeria";
            this.Cola_Gomeria.ReadOnly = true;
            this.Cola_Gomeria.Width = 125;
            // 
            // Cola_VentasAccesorios
            // 
            this.Cola_VentasAccesorios.HeaderText = "Cola_VentasAccesorios";
            this.Cola_VentasAccesorios.MinimumWidth = 6;
            this.Cola_VentasAccesorios.Name = "Cola_VentasAccesorios";
            this.Cola_VentasAccesorios.ReadOnly = true;
            this.Cola_VentasAccesorios.Width = 125;
            // 
            // ColaMax_Surt
            // 
            this.ColaMax_Surt.HeaderText = "ColaMax_Surt";
            this.ColaMax_Surt.MinimumWidth = 6;
            this.ColaMax_Surt.Name = "ColaMax_Surt";
            this.ColaMax_Surt.ReadOnly = true;
            this.ColaMax_Surt.Width = 125;
            // 
            // ColaMax_Gome
            // 
            this.ColaMax_Gome.HeaderText = "ColaMax_Gome";
            this.ColaMax_Gome.MinimumWidth = 6;
            this.ColaMax_Gome.Name = "ColaMax_Gome";
            this.ColaMax_Gome.ReadOnly = true;
            this.ColaMax_Gome.Width = 125;
            // 
            // ColaMax_Vent
            // 
            this.ColaMax_Vent.HeaderText = "ColaMax_Vent";
            this.ColaMax_Vent.MinimumWidth = 6;
            this.ColaMax_Vent.Name = "ColaMax_Vent";
            this.ColaMax_Vent.ReadOnly = true;
            this.ColaMax_Vent.Width = 125;
            // 
            // CantClientesVanSinComprar
            // 
            this.CantClientesVanSinComprar.HeaderText = "CantClientesVanSinComprar";
            this.CantClientesVanSinComprar.MinimumWidth = 6;
            this.CantClientesVanSinComprar.Name = "CantClientesVanSinComprar";
            this.CantClientesVanSinComprar.ReadOnly = true;
            this.CantClientesVanSinComprar.Width = 125;
            // 
            // CantidadTotalClientes
            // 
            this.CantidadTotalClientes.HeaderText = "CantidadTotalClientes";
            this.CantidadTotalClientes.MinimumWidth = 6;
            this.CantidadTotalClientes.Name = "CantidadTotalClientes";
            this.CantidadTotalClientes.ReadOnly = true;
            this.CantidadTotalClientes.Width = 125;
            // 
            // FormSimulacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 809);
            this.Controls.Add(this.Metricas);
            this.Controls.Add(this.lblPorcentaje);
            this.Controls.Add(this.dgvGrillaSimulacion);
            this.Name = "FormSimulacion";
            this.Text = "Simulacion";
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrillaSimulacion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvGrillaSimulacion;
        private System.Windows.Forms.Label lblPorcentaje;
        private System.Windows.Forms.Button Metricas;
        private System.Windows.Forms.DataGridViewTextBoxColumn Evento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Reloj;
        private System.Windows.Forms.DataGridViewTextBoxColumn tiempoEntreLlegadas;
        private System.Windows.Forms.DataGridViewTextBoxColumn proxLlegada;
        private System.Windows.Forms.DataGridViewTextBoxColumn RND_tipo1Servicio;
        private System.Windows.Forms.DataGridViewTextBoxColumn tipo1Servicio;
        private System.Windows.Forms.DataGridViewTextBoxColumn RND_tipo2Servicio;
        private System.Windows.Forms.DataGridViewTextBoxColumn tipo2Servicio;
        private System.Windows.Forms.DataGridViewTextBoxColumn RND_finAtencionSurt;
        private System.Windows.Forms.DataGridViewTextBoxColumn tiempoAtencionSurt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Emple1_finAt_Surt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Emple1_Estado_Surt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Emple2_finAt_Surt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Emple2_Estado_Surt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Emple3_finAt_Surt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Emple3_Estado_Surt;
        private System.Windows.Forms.DataGridViewTextBoxColumn RND_finAtencionGome;
        private System.Windows.Forms.DataGridViewTextBoxColumn tiempoAtencionGome;
        private System.Windows.Forms.DataGridViewTextBoxColumn Emple1_finAt_Gome;
        private System.Windows.Forms.DataGridViewTextBoxColumn Emple1_Estado_Gome;
        private System.Windows.Forms.DataGridViewTextBoxColumn Emple2_finAt_Gome;
        private System.Windows.Forms.DataGridViewTextBoxColumn Emple2_Estado_Gome;
        private System.Windows.Forms.DataGridViewTextBoxColumn RND_finAtencionVent;
        private System.Windows.Forms.DataGridViewTextBoxColumn tiempoAtencionVent;
        private System.Windows.Forms.DataGridViewTextBoxColumn Emple_finAt_Vent;
        private System.Windows.Forms.DataGridViewTextBoxColumn Emple_Estado_Vent;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cola_Surtidor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cola_Gomeria;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cola_VentasAccesorios;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColaMax_Surt;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColaMax_Gome;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColaMax_Vent;
        private System.Windows.Forms.DataGridViewTextBoxColumn CantClientesVanSinComprar;
        private System.Windows.Forms.DataGridViewTextBoxColumn CantidadTotalClientes;
    }
}