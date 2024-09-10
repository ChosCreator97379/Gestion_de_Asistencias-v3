using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using CapaEntidad;
using System.Data;

namespace CapaDato
{
    public class AsistenciaCD
    {
        public void GuardarAsistencia(AsistenciaCE asistencia)
        {
            using (SqlConnection cnx = ConexionCD.sqlConnection())
            {
                if (cnx == null)
                {
                    throw new Exception("No se pudo establecer la conexión a la base de datos.");
                }

                try
                {
                    cnx.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Asistencias (ID_Empleado, Fecha, HoraEntrada, HoraSalida) VALUES (@ID_Empleado, @Fecha, @HoraEntrada, @HoraSalida)", cnx);

                    cmd.Parameters.AddWithValue("@ID_Empleado", asistencia.ID_Empleado);
                    cmd.Parameters.AddWithValue("@Fecha", asistencia.Fecha);

                    cmd.Parameters.AddWithValue("@HoraEntrada", asistencia.HoraEntrada.HasValue ? (object)asistencia.HoraEntrada.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@HoraSalida", asistencia.HoraSalida.HasValue ? (object)asistencia.HoraSalida.Value : DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al guardar la asistencia: " + ex.Message);
                }
            }
        }

        public DataTable ObtenerAsistencias()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cnx = ConexionCD.sqlConnection())
            {
                if (cnx == null)
                {
                    throw new Exception("No se pudo establecer la conexión a la base de datos.");
                }

                try
                {
                    cnx.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Asistencias", cnx);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener los datos: " + ex.Message);
                }
            }
            return dt;
        }
    }
}
