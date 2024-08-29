using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_SC_601.Models
{
    public class Producto
    {
        public int IDProducto { get; set; }

        [Required]
        [StringLength(255)]
        public string NombreProducto { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        [Column(TypeName = "decimal(25,2)")]
        public decimal Precio { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public int IDCategoria { get; set; }

        [Required]
        public int IDProveedor { get; set; }

        public bool Activo { get; set; }

        [Required]
        public string ImagenURL1 { get; set; }

        [Required]
        public string ImagenURL2 { get; set; }

        [Required]
        public string ImagenURL3 { get; set; }
    }
}
