using System;
using System.Collections.Generic;

namespace TiendaTecnologia.AccesoDatos.Models;

public partial class MetodosPago
{
    public int IdmetodoPago { get; set; }

    public int Idusuario { get; set; }

    public string? Tipo { get; set; }

    public string? Proveedor { get; set; }

    public string? NumeroCuenta { get; set; }

    public DateTime? FechaExpiracion { get; set; }

    public virtual Usuario IdusuarioNavigation { get; set; } = null!;
}
