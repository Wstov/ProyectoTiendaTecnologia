using System;
using System.Collections.Generic;

namespace TiendaTecnologia.Models;

public partial class DireccionesEnvio
{
    public int Iddireccion { get; set; }

    public int Idusuario { get; set; }

    public string Direccion { get; set; } = null!;

    public string? Ciudad { get; set; }

    public string? Pais { get; set; }

    public string? CodigoPostal { get; set; }

    public string? Telefono { get; set; }

    public virtual Usuario IdusuarioNavigation { get; set; } = null!;
}
