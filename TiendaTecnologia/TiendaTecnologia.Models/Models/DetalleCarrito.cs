using System;
using System.Collections.Generic;

namespace TiendaTecnologia.AccesoDatos.Models;

public partial class DetalleCarrito
{
    public int IddetalleCarrito { get; set; }

    public int Idcarrito { get; set; }

    public int Idproducto { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public virtual Carrito IdcarritoNavigation { get; set; } = null!;

    public virtual Producto IdproductoNavigation { get; set; } = null!;
}
