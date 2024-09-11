using CapaNegocio;
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

namespace CapaPresentacion
{
    public partial class Editar : Form
    {
        public Editar()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                
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

                // Insertar datos del empleado en la base de datos usando la clase EmpleadoCD
                EmpleadoCD empleadosCD = new EmpleadoCD();

                
                using (SqlConnection cnx = ConexionCD.sqlConnection())
                {
                    cnx.Open();
                    string queryEmpleado = "INSERT INTO Empleados (Nombre, Apellido1, Apellido2, DNI, Telefono, CorreoElectronico, Direccion, Distrito) " +
                                           "VALUES (@Nombre, @Apellido1, @Apellido2, @DNI, @Telefono, @Correo, @Direccion, @Distrito); SELECT SCOPE_IDENTITY();";

                    SqlCommand cmdEmpleado = new SqlCommand(queryEmpleado, cnx);
                    cmdEmpleado.Parameters.AddWithValue("@Nombre", nombre);
                    cmdEmpleado.Parameters.AddWithValue("@Apellido1", apellido1);
                    cmdEmpleado.Parameters.AddWithValue("@Apellido2", apellido2);
                    cmdEmpleado.Parameters.AddWithValue("@DNI", dni);
                    cmdEmpleado.Parameters.AddWithValue("@Telefono", telefono);
                    cmdEmpleado.Parameters.AddWithValue("@Correo", correo);
                    cmdEmpleado.Parameters.AddWithValue("@Direccion", direccion);
                    cmdEmpleado.Parameters.AddWithValue("@Distrito", distrito);

                    // Obtener el ID del empleado insertado
                    int idEmpleado = Convert.ToInt32(cmdEmpleado.ExecuteScalar());

                    
                    string queryLaboral = "INSERT INTO DatosLaborales (ID_Empleado, Cargo, Area, EstadoLaboral, Nombre_Supervisor) " +
                                          "VALUES (@ID_Empleado, @Cargo, @Area, @EstadoLaboral, @NombreSupervisor)";

                    SqlCommand cmdLaboral = new SqlCommand(queryLaboral, cnx);
                    cmdLaboral.Parameters.AddWithValue("@ID_Empleado", idEmpleado);
                    cmdLaboral.Parameters.AddWithValue("@Cargo", cargo);
                    cmdLaboral.Parameters.AddWithValue("@Area", area);
                    cmdLaboral.Parameters.AddWithValue("@EstadoLaboral", estadoLaboral);
                    cmdLaboral.Parameters.AddWithValue("@NombreSupervisor", nombreSupervisor);
                    cmdLaboral.ExecuteNonQuery();
                }

                MessageBox.Show("Datos guardados correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar los datos: " + ex.Message);
            }
        }

        private void Editar_Load(object sender, EventArgs e)
        {

        }
    }
}

