using System.ComponentModel.DataAnnotations;

namespace Proyecto_SC_601.Models
{
    public class Categoria
    {
        public int IDCategoria { get; set; }

        [Required]
        [StringLength(255)]
        public string NombreCategoria { get; set; }

        [Required]
        public string Descripcion { get; set; }
    }
}
