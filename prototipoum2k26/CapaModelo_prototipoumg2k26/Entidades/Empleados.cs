using System;

namespace CapaModelo_prototipoumg2k26.Entidades
{
    // Se conserva el nombre de la clase/archivo para no alterar la estructura del proyecto.
    // La entidad ahora representa un registro de tbl_Puestos.
    public class Empleados
    {
        public int IdPuesto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal SalarioBase { get; set; }
    }
}
