﻿using System;
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

        public int InsertarEmpleado(string nombre, string apellido1, string apellido2, string dni, string telefono,
            string correo, DateTime fechaNacimiento, string direccion, string distrito)
        {
            using (SqlConnection conn = ConexionCD.sqlConnection())
            {
                if (conn == null)
                {
                    throw new Exception("No se pudo establecer una conexión con la base de datos.");
                }

                conn.Open();
                string query = "INSERT INTO Empleados (Nombre, Apellido1, Apellido2, DNI, Telefono, CorreoElectronico, FechaNacimiento, Direccion, Distrito) " +
                               "VALUES (@Nombre, @Apellido1, @Apellido2, @DNI, @Telefono, @Correo, @FechaNacimiento, @Direccion, @Distrito); " +
                               "SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Apellido1", apellido1);
                    cmd.Parameters.AddWithValue("@Apellido2", apellido2);
                    cmd.Parameters.AddWithValue("@DNI", dni);
                    cmd.Parameters.AddWithValue("@Telefono", telefono);
                    cmd.Parameters.AddWithValue("@Correo", correo);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", fechaNacimiento);
                    cmd.Parameters.AddWithValue("@Direccion", direccion);
                    cmd.Parameters.AddWithValue("@Distrito", distrito);

                    int empleadoId = Convert.ToInt32(cmd.ExecuteScalar());
                    return empleadoId;
                }
            }
        }

        public void InsertarDatosLaborales(int empleadoId, string cargo, string area, string estadoLaboral, string nombreSupervisor)
        {
            using (SqlConnection conn = ConexionCD.sqlConnection())
            {
                if (conn == null)
                {
                    throw new Exception("No se pudo establecer una conexión con la base de datos.");
                }

                conn.Open();
                string query = "INSERT INTO DatosLaborales (ID_Empleado, Cargo, Area, EstadoLaboral, Nombre_Supervisor) " +
                               "VALUES (@ID_Empleado, @Cargo, @Area, @EstadoLaboral, @NombreSupervisor);";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID_Empleado", empleadoId);
                    cmd.Parameters.AddWithValue("@Cargo", cargo);
                    cmd.Parameters.AddWithValue("@Area", area);
                    cmd.Parameters.AddWithValue("@EstadoLaboral", estadoLaboral);
                    cmd.Parameters.AddWithValue("@NombreSupervisor", nombreSupervisor);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertarDatosAcademicos(int empleadoId, string universidadInstituto, string carrera)
        {
            using (SqlConnection conn = ConexionCD.sqlConnection())
            {
                if (conn == null)
                {
                    throw new Exception("No se pudo establecer una conexión con la base de datos.");
                }

                conn.Open();
                string query = "INSERT INTO DatosAcademicos (ID_Empleado, UniversidadInstituto, Carrera) " +
                               "VALUES (@ID_Empleado, @Universidad, @Carrera);";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID_Empleado", empleadoId);
                    cmd.Parameters.AddWithValue("@Universidad", universidadInstituto);
                    cmd.Parameters.AddWithValue("@Carrera", carrera);

                    cmd.ExecuteNonQuery();
                }
            }
        }


    }
}
