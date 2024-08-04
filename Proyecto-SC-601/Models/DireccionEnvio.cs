using System.ComponentModel.DataAnnotations;

namespace Proyecto_SC_601.Models
{
    public class DireccionEnvio
    {
        public int IDDireccion { get; set; }

        [Required]
        public int IDUsuario { get; set; }

        [Required]
        [StringLength(255)]
        public string Direccion { get; set; }

        [Required]
        [StringLength(100)]
        public string Ciudad { get; set; }

        [Required]
        [StringLength(100)]
        public string Pais { get; set; }

        [Required]
        [StringLength(20)]
        public string CodigoPostal { get; set; }

        [Required]
        [StringLength(20)]
        public string Telefono { get; set; }
    }
}
