using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using CapaModelo_prototipoumg2k26.Contratos;
using CapaModelo_prototipoumg2k26.Entidades;
using CapaModelo_prototipoumg2k26.Repositorios;

namespace CapaControlador_prototipoumg2k26
{
    // Se conserva el nombre del modelo para mantener intacta la estructura/referencias del proyecto.
    // El modelo ahora administra puestos.
    public class ModeloEmpleado
    {
        private int _idPuesto;
        private string _nombre;
        private string _descripcion;
        private decimal _salarioBase;
        private readonly IRepositorioEmpleados RepositorioEmpleados;
        private List<ModeloEmpleado> ListaEmpleados;

        public EstadoEntidad Estado { private get; set; }

        public int IdPuesto { get => _idPuesto; set => _idPuesto = value; }

        [Required(ErrorMessage = "El nombre del puesto es requerido")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres")]
        public string Nombre { get => _nombre; set => _nombre = value; }

        [StringLength(200, ErrorMessage = "La descripción no puede exceder 200 caracteres")]
        public string Descripcion { get => _descripcion; set => _descripcion = value; }

        [Range(typeof(decimal), "0.01", "99999999.99", ErrorMessage = "El salario base debe ser mayor que cero")]
        public decimal SalarioBase { get => _salarioBase; set => _salarioBase = value; }

        public ModeloEmpleado()
        {
            RepositorioEmpleados = new RepositorioEmpleados();
            ListaEmpleados = new List<ModeloEmpleado>();
        }

        public string GrabarCambios()
        {
            try
            {
                var entidad = new Empleados
                {
                    IdPuesto = _idPuesto,
                    Nombre = _nombre,
                    Descripcion = _descripcion,
                    SalarioBase = _salarioBase
                };

                switch (Estado)
                {
                    case EstadoEntidad.Added:
                        RepositorioEmpleados.Agregar(entidad);
                        return "Puesto registrado exitosamente";
                    case EstadoEntidad.Modified:
                        RepositorioEmpleados.Editar(entidad);
                        return "Puesto actualizado exitosamente";
                    case EstadoEntidad.Deleted:
                        RepositorioEmpleados.Remover(entidad);
                        return "Puesto eliminado exitosamente";
                    default:
                        return "No se ha definido una operación";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public List<ModeloEmpleado> GetAll()
        {
            var datos = RepositorioEmpleados.GetAll();
            ListaEmpleados = datos.Select(item => new ModeloEmpleado
            {
                _idPuesto = item.IdPuesto,
                _nombre = item.Nombre,
                _descripcion = item.Descripcion,
                _salarioBase = item.SalarioBase
            }).ToList();

            return ListaEmpleados;
        }

        public IEnumerable<ModeloEmpleado> FindbyId(string filter)
        {
            if (ListaEmpleados == null || ListaEmpleados.Count == 0)
                GetAll();

            if (string.IsNullOrWhiteSpace(filter))
                return ListaEmpleados;

            filter = filter.Trim();
            return ListaEmpleados.Where(p =>
                p.IdPuesto.ToString(CultureInfo.InvariantCulture).Contains(filter) ||
                (p.Nombre ?? string.Empty).IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0 ||
                (p.Descripcion ?? string.Empty).IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0);
        }
    }
}
