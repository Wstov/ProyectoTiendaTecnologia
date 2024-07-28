using System;
using System.Collections.Generic;

namespace TiendaTecnologia.Models;

public partial class Pedido
{
    public int Idpedido { get; set; }

    public int Idusuario { get; set; }

    public DateTime FechaPedido { get; set; }

    public decimal Total { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<DetallesPedido> DetallesPedidos { get; set; } = new List<DetallesPedido>();

    public virtual Usuario IdusuarioNavigation { get; set; } = null!;
}
