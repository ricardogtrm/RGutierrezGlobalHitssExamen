using System;
using System.Collections.Generic;

namespace DL;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string ApellidoMaterno { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public int? Edad { get; set; }

    public string? Telefono { get; set; }

    public string? Sexo { get; set; }

    public DateTime? FechaIngreso { get; set; }

    public decimal? Salario { get; set; }

    public int? IdSucursal { get; set; }

    public virtual Sucursal? IdSucursalNavigation { get; set; }

    public string Sucursal { get; set; }
}
