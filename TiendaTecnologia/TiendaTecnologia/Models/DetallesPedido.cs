using System;
using System.Collections.Generic;

namespace TiendaTecnologia.Models;

public partial class DetallesPedido
{
    public int Iddetalle { get; set; }

    public int Idpedido { get; set; }

    public int Idproducto { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public virtual Pedido IdpedidoNavigation { get; set; } = null!;

    public virtual Producto IdproductoNavigation { get; set; } = null!;
}
