using System;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using CapaControlador_prototipoumg2k26;

namespace CapaVista_prototipoumg2k26.Reportes
{
    public partial class FrmReporteEmpleados : Form
    {
        private readonly ModeloEmpleado empleado = new ModeloEmpleado();
        public FrmReporteEmpleados()
        {
            InitializeComponent();
        }

        private void FrmReporteEmpleados_Load(object sender, EventArgs e)
        {
            ReportDataSource reportDataSource = new ReportDataSource("DataSet1", empleado.GetAll());
            reportViewer1.LocalReport.ReportEmbeddedResource = "CapaVista_prototipoumg2k26.Reportes.ReporteEmpleados.rdlc";
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(reportDataSource);

            reportViewer1.RefreshReport();
        }
    }
}
