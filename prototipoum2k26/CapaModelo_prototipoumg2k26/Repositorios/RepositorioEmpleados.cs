using CapaModelo_prototipoumg2k26.Contratos;
using CapaModelo_prototipoumg2k26.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;

namespace CapaModelo_prototipoumg2k26.Repositorios
{
    // Se conserva el nombre de la clase/archivo para no mover ni renombrar elementos del proyecto.
    // Este repositorio trabaja con tbl_Puestos de la base umg_didactica.
    public class RepositorioEmpleados : RepositorioMaestro, IRepositorioEmpleados
    {
        private readonly string selectAll;
        private readonly string insert;
        private readonly string update;
        private readonly string delete;

        public RepositorioEmpleados()
        {
            selectAll = "SELECT cmp_id_puesto, cmp_nombre, cmp_descripcion, cmp_salario_base FROM tbl_Puestos ORDER BY cmp_id_puesto";
            insert = "INSERT INTO tbl_Puestos (cmp_nombre, cmp_descripcion, cmp_salario_base) VALUES (?, ?, ?)";
            update = "UPDATE tbl_Puestos SET cmp_nombre=?, cmp_descripcion=?, cmp_salario_base=? WHERE cmp_id_puesto=?";
            delete = "DELETE FROM tbl_Puestos WHERE cmp_id_puesto=?";
        }

        public int Agregar(Empleados entidad)
        {
            var parametros = new List<OdbcParameter>
            {
                new OdbcParameter("p_nombre", OdbcType.VarChar) { Value = entidad.Nombre },
                new OdbcParameter("p_descripcion", OdbcType.VarChar) { Value = (object)entidad.Descripcion ?? DBNull.Value },
                new OdbcParameter("p_salario", OdbcType.Decimal) { Value = entidad.SalarioBase, Precision = 10, Scale = 2 }
            };

            return EjecucionNonQuery(insert, parametros, CommandType.Text);
        }

        public int Editar(Empleados entidad)
        {
            var parametros = new List<OdbcParameter>
            {
                new OdbcParameter("p_nombre", OdbcType.VarChar) { Value = entidad.Nombre },
                new OdbcParameter("p_descripcion", OdbcType.VarChar) { Value = (object)entidad.Descripcion ?? DBNull.Value },
                new OdbcParameter("p_salario", OdbcType.Decimal) { Value = entidad.SalarioBase, Precision = 10, Scale = 2 },
                new OdbcParameter("p_id_puesto", OdbcType.Int) { Value = entidad.IdPuesto }
            };

            return EjecucionNonQuery(update, parametros, CommandType.Text);
        }

        public int Remover(Empleados entidad)
        {
            var parametros = new List<OdbcParameter>
            {
                new OdbcParameter("p_id_puesto", OdbcType.Int) { Value = entidad.IdPuesto }
            };

            return EjecucionNonQuery(delete, parametros, CommandType.Text);
        }

        public IEnumerable<Empleados> GetAll()
        {
            var listaPuestos = new List<Empleados>();
            var tabla = EjecucionConsulta(selectAll, CommandType.Text);

            foreach (DataRow row in tabla.Rows)
            {
                listaPuestos.Add(new Empleados
                {
                    IdPuesto = Convert.ToInt32(row["cmp_id_puesto"]),
                    Nombre = row["cmp_nombre"].ToString(),
                    Descripcion = row["cmp_descripcion"] == DBNull.Value ? string.Empty : row["cmp_descripcion"].ToString(),
                    SalarioBase = Convert.ToDecimal(row["cmp_salario_base"])
                });
            }

            return listaPuestos;
        }
    }
}
