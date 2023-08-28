using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Empleado
    {
        public int IdEmpleado { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Direccion { get; set; }
        public int Edad { get; set; }
        public string Telefono { get; set; }
        public string Sexo { get; set; }
        public string? FechaIngreso { get; set; }
        public string Salario { get; set; }
        public ML.Sucursal Sucursal { get; set; }
        public List<object>? Empleados { get; set; }

    }
}
