using System;
using System.Collections.Generic;

namespace TiendaTecnologia.Models;

public partial class Carrito
{
    public int Idcarrito { get; set; }

    public int Idusuario { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual ICollection<DetalleCarrito> DetalleCarritos { get; set; } = new List<DetalleCarrito>();

    public virtual Usuario IdusuarioNavigation { get; set; } = null!;
}
