using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TiendaTecnologia.AccesoDatos.Models;

public partial class Producto
{
    [Key]
    public int Idproducto { get; set; }

    public string NombreProducto { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public int Stock { get; set; }

    public int Idcategoria { get; set; }

    public int Idproveedor { get; set; }

    public string? ImagenUrl { get; set; }

    //public virtual ICollection<DetalleCarrito> DetalleCarritos { get; set; } = new List<DetalleCarrito>();

    //public virtual ICollection<DetallesPedido> DetallesPedidos { get; set; } = new List<DetallesPedido>();

    public virtual Categoria IdcategoriaNavigation { get; set; } = null!;

    public virtual Proveedor IdproveedorNavigation { get; set; } = null!;
}
