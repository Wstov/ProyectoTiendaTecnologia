using System.ComponentModel.DataAnnotations;

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
        public decimal PrecioUnitario { get; set; }
    }
}
