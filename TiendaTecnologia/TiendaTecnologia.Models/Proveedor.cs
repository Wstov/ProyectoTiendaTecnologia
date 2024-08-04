using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TiendaTecnologia.AccesoDatos.Models;

public partial class Proveedor
{
    [Key]
    public int Idproveedor { get; set; }

    public string NombreProveedor { get; set; } = null!;

    public string? Contacto { get; set; }

    public string? Direccion { get; set; }

    public string? Ciudad { get; set; }

    public string? Pais { get; set; }

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
