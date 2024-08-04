using System;
using System.Collections.Generic;

namespace TiendaTecnologia.AccesoDatos.Models
{
    public partial class Pedido
    {
        public int Idpedido { get; set; }

        public int Idusuario { get; set; }

        public DateTime FechaPedido { get; set; }

        public decimal Total { get; set; }

        public string? Estado { get; set; }

        // Propiedad de navegación a AspNetUsers
        public virtual AspNetUsers IdusuarioNavigation { get; set; } = null!;

        //public virtual ICollection<DetallesPedido> DetallesPedidos { get; set; } = new List<DetallesPedido>();
    }

    public class AspNetUsers
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        // Otras propiedades que AspNetUsers debería tener
    }
}
