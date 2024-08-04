using System.ComponentModel.DataAnnotations;

namespace Proyecto_SC_601.Models
{
    public class Proveedor
    {
        public int IDProveedor { get; set; }

        [Required]
        [StringLength(255)]
        public string NombreProveedor { get; set; }

        [Required]
        [StringLength(255)]
        public string Contacto { get; set; }

        [Required]
        [StringLength(255)]
        public string Direccion { get; set; }


        [Required]
        [StringLength(100)]
        public string Pais { get; set; }
        [Required]
        [StringLength(100)]
        public string Ciudad { get; set; }

        [Required]
        [StringLength(20)]
        public string Telefono { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }
    }
}
