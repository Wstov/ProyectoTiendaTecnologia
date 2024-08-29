using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_SC_601.Models
{
    public class DetallePedido
    {
        public int IDDetalle { get; set; }

        [Required]
        public int IDPedido { get; set; }

        [Required]
        public int IDProducto { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        [Column(TypeName = "decimal(25,2)")]
        public decimal PrecioUnitario { get; set; }
    }
}
