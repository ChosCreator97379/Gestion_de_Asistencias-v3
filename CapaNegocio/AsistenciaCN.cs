using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using CapaDato;
using CapaEntidad;

namespace CapaNegocio
{
    public class AsistenciaCN
    {
        public void GuardarAsistencia(int idEmpleado, DateTime fecha, TimeSpan? hora, string tipoRegistro)
        {
            AsistenciaCD asistenciaCD = new AsistenciaCD();
            AsistenciaCE asistencia = new AsistenciaCE
            {
                ID_Empleado = idEmpleado,
                Fecha = fecha,
                HoraEntrada = tipoRegistro == "Entrada" ? hora : (TimeSpan?)null,
                HoraSalida = tipoRegistro == "Salida" ? hora : (TimeSpan?)null
            };
            asistenciaCD.GuardarAsistencia(asistencia);
        }
        public DataTable ObtenerAsistencias()
        {
            AsistenciaCD asistenciaCD = new AsistenciaCD();
            return asistenciaCD.ObtenerAsistencias();
        }
        public void ActualizarAsistencia(int id, DateTime fecha, TimeSpan? horaEntrada, TimeSpan? horaSalida)
        {
            AsistenciaCD asistenciaCD = new AsistenciaCD();
            asistenciaCD.ActualizarAsistencia(id, fecha, horaEntrada, horaSalida);
        }
        public DataTable ObtenerAsistenciaPorId(int id)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cnx = ConexionCD.sqlConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Asistencias WHERE ID = @ID", cnx);
                cmd.Parameters.AddWithValue("@ID", id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }
        public bool EliminarAsistencia(int id)
        {
            using (SqlConnection cnx = ConexionCD.sqlConnection())
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Asistencias WHERE ID = @ID", cnx);
                cmd.Parameters.AddWithValue("@ID", id);

                cnx.Open();
                int filasAfectadas = cmd.ExecuteNonQuery();
                cnx.Close();

                return filasAfectadas > 0;
            }
        }
        public DataTable BuscarAsistencias(string campo, string valor)
        {
            AsistenciaCD asistenciaCD = new AsistenciaCD();
            return asistenciaCD.BuscarAsistencias(campo, valor);
        }
    }
}
