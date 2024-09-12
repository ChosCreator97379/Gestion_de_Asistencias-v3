﻿using System;
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

        private EmpleadoCD empleadoCD = new EmpleadoCD();

        public void AgregarEmpleadoConDatos(string nombre, string apellido1, string apellido2, string dni, string telefono,
            string correo, DateTime fechaNacimiento, string direccion, string distrito, string cargo, string area,
            string estadoLaboral, string nombreSupervisor, string universidadInstituto, string carrera)
        {
            int empleadoId = empleadoCD.InsertarEmpleado(nombre, apellido1, apellido2, dni, telefono, correo, fechaNacimiento, direccion, distrito);

            empleadoCD.InsertarDatosLaborales(empleadoId, cargo, area, estadoLaboral, nombreSupervisor);
            empleadoCD.InsertarDatosAcademicos(empleadoId, universidadInstituto, carrera);
        }

        public static void ActualizarEmpleado(int idEmpleado, string nombre, string apellido1, string apellido2, string dni, 
            string telefono, string correo, string direccion, string distrito, string cargo, string area, string estadoLaboral, 
            string nombreSupervisor)
        {
            EmpleadoCD.ActualizarEmpleado(idEmpleado, nombre, apellido1, apellido2, dni, telefono, correo, direccion, distrito, cargo, area, estadoLaboral, nombreSupervisor);
        }
    }
}

