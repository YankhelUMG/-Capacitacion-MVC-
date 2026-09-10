using System;
using System.Globalization;
using System.Windows.Forms;
using CapaControlador_prototipoumg2k26;

namespace CapaVista_prototipoumg2k26.Formas
{
    public partial class FrmEmpleados : Form
    {
        private ModeloEmpleado empleado = new ModeloEmpleado();

        public FrmEmpleados()
        {
            InitializeComponent();
            ConfigurarFormularioPuestos();
            panIngresoDatos.Enabled = false;
        }

        private void FrmEmpleados_Load(object sender, EventArgs e)
        {
            listaEmpleados();
        }

        private void ConfigurarFormularioPuestos()
        {
            Text = "Mantenimiento de Puestos - UMG Didáctica";

            lblNumeroID.Text = "Nombre del puesto";
            lblNombre.Text = "Descripción";
            lblCorreo.Text = "Salario base";

            // tbl_Puestos solo requiere nombre, descripción y salario base.
            lblCumpleaños.Visible = false;
            txtCumpleaños.Visible = false;

            // El combo del prototipo original correspondía a empleados/puestos y ya no aplica.
            comboI1.Visible = false;
            txtSearch.Width = 736;

            dgvEmpleados.AutoGenerateColumns = true;
            dgvEmpleados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEmpleados.MultiSelect = false;
            dgvEmpleados.ReadOnly = true;
        }

        private void listaEmpleados()
        {
            try
            {
                dgvEmpleados.DataSource = empleado.GetAll();
                AjustarColumnas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AjustarColumnas()
        {
            if (dgvEmpleados.Columns["IdPuesto"] != null)
                dgvEmpleados.Columns["IdPuesto"].HeaderText = "ID Puesto";
            if (dgvEmpleados.Columns["Nombre"] != null)
                dgvEmpleados.Columns["Nombre"].HeaderText = "Nombre";
            if (dgvEmpleados.Columns["Descripcion"] != null)
                dgvEmpleados.Columns["Descripcion"].HeaderText = "Descripción";
            if (dgvEmpleados.Columns["SalarioBase"] != null)
            {
                dgvEmpleados.Columns["SalarioBase"].HeaderText = "Salario base";
                dgvEmpleados.Columns["SalarioBase"].DefaultCellStyle.Format = "N2";
            }
            dgvEmpleados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dgvEmpleados.DataSource = empleado.FindbyId(txtSearch.Text);
            AjustarColumnas();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dgvEmpleados.DataSource = empleado.FindbyId(txtSearch.Text);
            AjustarColumnas();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            decimal salario;
            if (!decimal.TryParse(txtCorreo.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out salario) &&
                !decimal.TryParse(txtCorreo.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out salario))
            {
                MessageBox.Show("Ingrese un salario base válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCorreo.Focus();
                return;
            }

            empleado.Nombre = txtNumeroID.Text.Trim();
            empleado.Descripcion = txtNombre.Text.Trim();
            empleado.SalarioBase = salario;

            bool valido = new Ayudas.ValidacionDatos(empleado).Validar();
            if (valido)
            {
                string resultado = empleado.GrabarCambios();
                MessageBox.Show(resultado);
                listaEmpleados();
                Reinicio();
            }
        }

        private void Reinicio()
        {
            panIngresoDatos.Enabled = false;
            txtNumeroID.Clear();
            txtNombre.Clear();
            txtCorreo.Clear();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Reinicio();
            panIngresoDatos.Enabled = true;
            empleado.Estado = EstadoEntidad.Added;
            empleado.IdPuesto = 0;
            txtNumeroID.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvEmpleados.SelectedRows.Count > 0)
            {
                panIngresoDatos.Enabled = true;
                empleado.Estado = EstadoEntidad.Modified;
                empleado.IdPuesto = Convert.ToInt32(dgvEmpleados.CurrentRow.Cells["IdPuesto"].Value);
                txtNumeroID.Text = dgvEmpleados.CurrentRow.Cells["Nombre"].Value?.ToString();
                txtNombre.Text = dgvEmpleados.CurrentRow.Cells["Descripcion"].Value?.ToString();
                txtCorreo.Text = Convert.ToDecimal(dgvEmpleados.CurrentRow.Cells["SalarioBase"].Value).ToString("0.00");
            }
            else
            {
                MessageBox.Show("Seleccione una fila");
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (dgvEmpleados.SelectedRows.Count > 0)
            {
                empleado.Estado = EstadoEntidad.Deleted;
                empleado.IdPuesto = Convert.ToInt32(dgvEmpleados.CurrentRow.Cells["IdPuesto"].Value);

                var respuesta = MessageBox.Show(
                    "¿Desea eliminar el puesto seleccionado?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    string resultado = empleado.GrabarCambios();
                    MessageBox.Show(resultado);
                    listaEmpleados();
                }
            }
            else
            {
                MessageBox.Show("Seleccione una fila");
            }
        }

        // Se mantiene el método porque pertenece al archivo original, pero el combo ya no es necesario
        // para tbl_Puestos.
        void CargarDatos()
        {
        }
    }
}
