using System;
using System.Windows.Forms;
using CapaVista_prototipoumg2k26;
using CapaVista_prototipoumg2k26.Formas;

namespace Ejecucion_Taller_MVC_1
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new FrmEmpleados());
        }
    }
}