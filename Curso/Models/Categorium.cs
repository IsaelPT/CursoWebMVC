using System;
using System.Collections.Generic;

namespace Curso.Models;

public partial class Categorium
{
    public int IdCategoria { get; set; }

    public string? Descripcion { get; set; }

    public bool? Activo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
