using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class Administrador : Form
    {
        public Administrador()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Añadirempleado añadirempleado = new Añadirempleado();
            añadirempleado.Show();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            // Verificar que se haya seleccionado una fila
            if (dataGridView.SelectedRows.Count > 0)
            {
                // Obtener el ID del empleado desde la fila seleccionada
                int idEmpleado = Convert.ToInt32(dataGridView.SelectedRows[0].Cells["ID"].Value);

                // Abrir el formulario de edición y pasarle el ID del empleado
                Editar editarEmpleado = new Editar(idEmpleado);
                editarEmpleado.ShowDialog();
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un empleado para editar.");
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    DataTable dt = EmpleadoCN.ObtenerInformacionEmpleados();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dataGridView.DataSource = dt;
                    }
                    else
                    {
                        dataGridView.DataSource = null;
                        MessageBox.Show("No se encontraron datos para mostrar.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al obtener los datos: {ex.Message}");
                }
            }

        }

        private void Administrador_Load(object sender, EventArgs e)
        {

        }
    }
}
