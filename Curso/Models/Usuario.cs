using System;
using System.Collections.Generic;

namespace Curso.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? Nombres { get; set; }

    public string? Apellidos { get; set; }

    public string? Correro { get; set; }

    public string? Clave { get; set; }

    public bool? Restablecer { get; set; }

    public bool? Activo { get; set; }

    public DateTime? FechaRegistro { get; set; }
}
