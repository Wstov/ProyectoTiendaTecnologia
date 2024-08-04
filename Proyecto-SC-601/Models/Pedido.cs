using System.ComponentModel.DataAnnotations;

namespace Proyecto_SC_601.Models
{
    public class Pedido
    {
        public int IDPedido { get; set; }

        [Required]
        public int IDUsuario { get; set; }

        [Required]
        public int IDDireccion { get; set; }

        [Required]
        public DateTime FechaPedido { get; set; }

        [Required]
        public decimal Total { get; set; }

        [Required]
        [StringLength(50)]
        public string Estado { get; set; }
    }
}
