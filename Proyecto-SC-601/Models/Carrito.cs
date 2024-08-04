using System.ComponentModel.DataAnnotations;

namespace Proyecto_SC_601.Models
{
    public class Carrito
    {
        public int IDDetalle { get; set; }

        [Required]
        public int IDProducto { get; set; }

        [Required]
        public int IDUsuario { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public decimal PrecioUnitario { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }
    }
}
