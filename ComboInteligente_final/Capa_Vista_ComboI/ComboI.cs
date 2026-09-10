using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa_Controlador_ComboI;

namespace Capa_Vista_ComboI
{
    public partial class cboPrueba : UserControl
    {
        public cboPrueba()
        {
            InitializeComponent();
        }

        ModeloComboI controlador = new ModeloComboI();

        public void llenarCombo(string _tabla, string _campo1, string _campo2)
        {
            var dtTabla = controlador.enviarDatos(_tabla, _campo1, _campo2);

            comboBox1.DataSource = dtTabla;
            comboBox1.ValueMember = _campo1; 
            comboBox1.DisplayMember = _campo2; 

            AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();
            foreach (DataRow row in dtTabla.Rows)
            {
                coleccion.Add(Convert.ToString(row[_campo1]) + "-" + Convert.ToString(row[_campo2]));
                coleccion.Add(Convert.ToString(row[_campo2]) + "-" + Convert.ToString(row[_campo1]));
            }

            comboBox1.AutoCompleteCustomSource = coleccion;
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }
    }
}