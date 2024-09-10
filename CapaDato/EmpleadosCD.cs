using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDato
{
    public class EmpleadoCD
    {
        public static DataTable BuscarEmpleadoPorID(int id)
        {
            using (SqlConnection cnx = ConexionCD.sqlConnection())
            {
                string query = "SELECT e.Nombre, e.Apellido1, e.Apellido2, dl.Cargo, dl.Area " +
                               "FROM Empleados e " +
                               "INNER JOIN DatosLaborales dl ON e.ID = dl.ID_Empleado " +
                               "WHERE e.ID = @ID";

                SqlCommand cmd = new SqlCommand(query, cnx);
                cmd.Parameters.AddWithValue("@ID", id);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
        }
        public DataTable ObtenerInformacionEmpleados()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cnx = ConexionCD.sqlConnection())
            {
                try
                {
                    string query = @"
                SELECT e.Nombre, e.Apellido1, e.Apellido2, e.DNI, e.Telefono, e.CorreoElectronico, e.FechaNacimiento, e.Direccion, e.Distrito,
                       dl.Cargo, dl.Area, dl.EstadoLaboral, dl.Nombre_Supervisor,
                       da.UniversidadInstituto, da.Carrera
                FROM Empleados e
                LEFT JOIN DatosLaborales dl ON e.ID = dl.ID_Empleado
                LEFT JOIN DatosAcademicos da ON e.ID = da.ID_Empleado";

                    SqlCommand cmd = new SqlCommand(query, cnx);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener la información: " + ex.Message);
                }
                finally
                {
                    if (cnx.State == ConnectionState.Open)
                    {
                        cnx.Close();
                    }
                }
            }
            return dt;
        }



    }
}
