﻿using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaDato;
using static System.Windows.Forms.MonthCalendar;

namespace CapaPresentacion
{
    public partial class Editar : Form
    {
        private int _idEmpleado;
        public Editar(int idEmpleado)
        {
            InitializeComponent();
            _idEmpleado = idEmpleado;
        }

        private void EditarEmpleado_Load(object sender, EventArgs e)
        {
            CargarDatosEmpleado(_idEmpleado);
        }

        private void CargarDatosEmpleado(int idEmpleado)
        {
            DataTable dt = EmpleadoCN.BuscarEmpleadoPorID(idEmpleado);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                // Cargar datos en los TextBoxes (Asegúrate de tener estos controles en el formulario)
                txtNombre.Text = row["Nombre"].ToString();
                txtApellido1.Text = row["Apellido1"].ToString();
                txtApellido2.Text = row["Apellido2"].ToString();
                txtDni.Text = row["DNI"].ToString();
                txtTelefono.Text = row["Telefono"].ToString();
                txtCorreo.Text = row["CorreoElectronico"].ToString();
                txtDireccion.Text = row["Direccion"].ToString();
                txtDistrito.Text = row["Distrito"].ToString();
                txtCargo.Text = row["Cargo"].ToString();
                txtArea.Text = row["Area"].ToString();
                txtEstadoLaboral.Text = row["EstadoLaboral"].ToString();
                txtNombreSupervisor.Text = row["Nombre_Supervisor"].ToString();
            }
            else
            {
                MessageBox.Show("No se encontraron datos del empleado.");
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Recoger los valores del formulario para actualizar
                string nombre = txtNombre.Text;
                string apellido1 = txtApellido1.Text;
                string apellido2 = txtApellido2.Text;
                string dni = txtDni.Text;
                string telefono = txtTelefono.Text;
                string correo = txtCorreo.Text;
                string direccion = txtDireccion.Text;
                string distrito = txtDistrito.Text;
                string cargo = txtCargo.Text;
                string area = txtArea.Text;
                string estadoLaboral = txtEstadoLaboral.Text;
                string nombreSupervisor = txtNombreSupervisor.Text;

                // Llamar a la capa de negocios para actualizar los datos del empleado
                EmpleadoCN.ActualizarEmpleado(_idEmpleado, nombre, apellido1, apellido2, dni, telefono, correo, direccion, distrito, cargo, area, estadoLaboral, nombreSupervisor);

                MessageBox.Show("Datos actualizados correctamente.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar los datos: {ex.Message}");
            }
        }

        private void Editar_Load(object sender, EventArgs e)
        {

        }
    }
}

