using System;
using System.Collections.Generic;

namespace Curso.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public int? IdMarca { get; set; }

    public int? IdCategoria { get; set; }

    public decimal? Precio { get; set; }

    public int? Stock { get; set; }

    public string? RutaImagen { get; set; }

    public string? NombreImagen { get; set; }

    public bool? Activo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Carrito> Carritos { get; set; } = new List<Carrito>();

    public virtual ICollection<DetalleVentum> DetalleVenta { get; set; } = new List<DetalleVentum>();

    public virtual Categorium? IdCategoriaNavigation { get; set; }

    public virtual Marca? IdMarcaNavigation { get; set; }

}
