using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDato;

namespace CapaNegocio
{
    public class EmpleadoCN
    {
        public static DataTable BuscarEmpleadoPorID(int id)
        {
            return CapaDato.EmpleadoCD.BuscarEmpleadoPorID(id);
        }

        public static DataTable ObtenerInformacionEmpleados()
        {
            EmpleadoCD empleadoCD = new EmpleadoCD();
            return empleadoCD.ObtenerInformacionEmpleados();
        }
    }
}
