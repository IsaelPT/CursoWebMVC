using System;
using System.Collections.Generic;

namespace Curso.Models;

public partial class Ventum
{
    public int IdVenta { get; set; }

    public int? IdCliente { get; set; }

    public int? TotalProducto { get; set; }

    public decimal? MontoTotal { get; set; }

    public string? Contacto { get; set; }

    public string? IdDistrito { get; set; }

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public string? IdTransaccion { get; set; }

    public DateTime? FechaVenta { get; set; }

    public virtual ICollection<DetalleVentum> DetalleVenta { get; set; } = new List<DetalleVentum>();

    public virtual Cliente? IdClienteNavigation { get; set; }
}
