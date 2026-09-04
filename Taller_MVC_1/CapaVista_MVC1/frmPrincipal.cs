using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaControlador_MVC1;

namespace CapaVista_MVC1
{
    public partial class frmPrincipal : Form
    {
        string nombreTabla = "tbl_Puestos";
        Controlador controlador = new Controlador();
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            actualizarDataGriedView();
        }
        public void actualizarDataGriedView()
        {

            DataTable dtVista = controlador.llenarDgv(nombreTabla);
            dgvConsultarTabla.DataSource=dtVista;
        }
    }
}
