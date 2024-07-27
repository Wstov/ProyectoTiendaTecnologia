using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TiendaTecnologia.AccesoDatos.Models;

public class Categoria
{
    [Key]
    public int Idcategoria { get; set; }

    [Required(ErrorMessage ="Ingrese un nombre para la categoría")]
    [Display(Name ="Nombre de Categoría")]
    public string NombreCategoria { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
