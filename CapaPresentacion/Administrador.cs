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
            int idEmpleado = Convert.ToInt32(dataGridView.SelectedRows[0].Cells["ID"].Value);
            Editar editar = new Editar(idEmpleado);

            editar.ShowDialog();
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
