using CapaModelo_prototipoumg2k26.Contratos;
using CapaModelo_prototipoumg2k26.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo_prototipoumg2k26.Repositorios
{
    public class RepositorioEmpleados : RepositorioMaestro, IRepositorioEmpleados
    {
        private string selectAll;
        private string insert;
        private string update;
        private string delete;

        public RepositorioEmpleados()
        {
            selectAll = "SELECT * FROM puestos";

            insert = "INSERT INTO puestos (cmp_nombre, cmp_descripcion, cmp_salario_base) VALUES (?, ?, ?)";

            update = "UPDATE puestos SET cmp_nombre=?, cmp_descripcion=?, cmp_salario_base=? WHERE cmp_id_puesto=?";

            delete = "DELETE FROM puestos WHERE cmp_id_puesto=?";
        }

        public int Agregar(Empleados entidad)
        {
            var _parametros = new List<OdbcParameter>();

            _parametros.Add(new OdbcParameter("p_Nombre", entidad.cmp_nombre));
            _parametros.Add(new OdbcParameter("p_Descripcion", entidad.cmp_descripcion));
            _parametros.Add(new OdbcParameter("p_SalarioBase", entidad.cmp_salario_base));

            return EjecucionNonQuery(insert, _parametros, CommandType.Text);
        }

        public int Editar(Empleados entidad)
        {
            var _parametros = new List<OdbcParameter>();

            _parametros.Add(new OdbcParameter("p_Nombre", entidad.cmp_nombre));
            _parametros.Add(new OdbcParameter("p_Descripcion", entidad.cmp_descripcion));
            _parametros.Add(new OdbcParameter("p_SalarioBase", entidad.cmp_salario_base));
            _parametros.Add(new OdbcParameter("p_IdPuesto", entidad.cmp_id_puesto));

            return EjecucionNonQuery(update, _parametros, CommandType.Text);
        }

        public int Remover(Empleados entidad)
        {
            var _parametros = new List<OdbcParameter>();

            _parametros.Add(new OdbcParameter("p_IdPuesto", entidad.cmp_id_puesto));

            return EjecucionNonQuery(delete, _parametros, CommandType.Text);
        }

        public IEnumerable<Empleados> GetAll()
        {
            var lstEmpleado = new List<Empleados>();

            var tblTabla = EjecucionConsulta(selectAll, CommandType.Text);

            foreach (DataRow row in tblTabla.Rows)
            {
                var empleado = new Empleados();

                empleado.cmp_id_puesto = Convert.ToInt32(row["cmp_id_puesto"]);
                empleado.cmp_nombre = row["cmp_nombre"].ToString();
                empleado.cmp_descripcion = row["cmp_descripcion"].ToString();
                empleado.cmp_salario_base = Convert.ToDecimal(row["cmp_salario_base"]);

                lstEmpleado.Add(empleado);
            }

            tblTabla.Clear();
            tblTabla = null;

            return lstEmpleado;
        }
    }
}