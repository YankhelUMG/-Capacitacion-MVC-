using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo_MVC1
{
    public class Sentencias
    {
        Conexion conn = new Conexion();
        public OdbcDataAdapter llenarTbl(string nombreTabla)
        {
            string sSql = "SELECT * FROM " + nombreTabla + ";";
            OdbcDataAdapter daSentencias = new OdbcDataAdapter(sSql, conn.conexion());
            return daSentencias;
        }
    }
}
