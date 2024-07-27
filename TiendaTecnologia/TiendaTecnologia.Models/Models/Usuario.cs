using System;
using System.Collections.Generic;

namespace TiendaTecnologia.AccesoDatos.Models;

public partial class Usuario
{
    public int Idusuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public string? Direccion { get; set; }

    public string? Ciudad { get; set; }

    public string? Pais { get; set; }

    public string? CodigoPostal { get; set; }

    public string? Telefono { get; set; }
    public string Rol { get; set; } = null!;

    public virtual ICollection<Carrito> Carritos { get; set; } = new List<Carrito>();

    public virtual ICollection<DireccionesEnvio> DireccionesEnvios { get; set; } = new List<DireccionesEnvio>();

    public virtual ICollection<MetodosPago> MetodosPagos { get; set; } = new List<MetodosPago>();

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
