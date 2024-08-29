using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Column(TypeName = "decimal(25,2)")]
        public decimal Total { get; set; }

        [Required]
        [StringLength(50)]
        public string Estado { get; set; }
    }
}
