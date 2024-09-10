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
            string nombre = txtNombre.Text;
            string apellido1 = txtApellido1.Text;
            string apellido2 = txtApellido2.Text;
            string dni = txtDni.Text;
            string telefono = txtTelefono.Text;
            string correo = txtCorreo.Text;
            string area = txtArea.Text;
            string direccion = txtDireccion.Text;
            string distrito = txtDistrito.Text;
            string cargo = txtCargo.Text;
            string estadoLaboral = txtEstadoLAboral.Text;
            string nombreSupervisor = txtNombreSupervisor.Text;


            string connectionString = "";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Insertar en la tabla Empleados
                string insertEmpleadoQuery = "INSERT INTO Empleados (Nombre, Apellido1, Apellido2, DNI, Telefono, CorreoElectronico, Direccion, Distrito) " +
                                             "VALUES (@Nombre, @Apellido1, @Apellido2, @DNI, @Telefono, @Correo, @Direccion, @Distrito)";
                using (SqlCommand cmd = new SqlCommand(insertEmpleadoQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Apellido1", apellido1);
                    cmd.Parameters.AddWithValue("@Apellido2", apellido2);
                    cmd.Parameters.AddWithValue("@DNI", dni);
                    cmd.Parameters.AddWithValue("@Telefono", telefono);
                    cmd.Parameters.AddWithValue("@Correo", correo);
                    cmd.Parameters.AddWithValue("@Direccion", direccion);
                    cmd.Parameters.AddWithValue("@Distrito", distrito);

                    cmd.ExecuteNonQuery();
                }

                // Obtener el ID del empleado recién insertado
                string getEmpleadoIDQuery = "SELECT SCOPE_IDENTITY()";
                SqlCommand getEmpleadoIDCmd = new SqlCommand(getEmpleadoIDQuery, conn);
                int empleadoID = Convert.ToInt32(getEmpleadoIDCmd.ExecuteScalar());

                // Insertar en la tabla DatosLaborales
                string insertDatosLaboralesQuery = "INSERT INTO DatosLaborales (ID_Empleado, Cargo, Area, EstadoLaboral, Nombre_Supervisor) " +
                                                   "VALUES (@ID_Empleado, @Cargo, @Area, @EstadoLaboral, @NombreSupervisor)";
                using (SqlCommand cmd = new SqlCommand(insertDatosLaboralesQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@ID_Empleado", empleadoID);
                    cmd.Parameters.AddWithValue("@Cargo", cargo);
                    cmd.Parameters.AddWithValue("@Area", area);
                    cmd.Parameters.AddWithValue("@EstadoLaboral", estadoLaboral);
                    cmd.Parameters.AddWithValue("@NombreSupervisor", nombreSupervisor);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Datos guardados exitosamente.");
            }
        }
    }
}
