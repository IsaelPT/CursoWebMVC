using System.ComponentModel.DataAnnotations;

namespace Curso.Models.NewFolder1
{
    public class ProductoViewModels
    {
        public int IdProducto { get; set; }
        [Required]
        public string? Nombre { get; set; }
        [Required]
        public string? Descripcion { get; set; }
        public int? IdMarca { get; set; }
        public int? IdCategoria { get; set; }
        [Required]
        public decimal? Precio { get; set; }
        [Required]
        public int? Stock { get; set; }
        public string? RutaImagen { get; set; }
        public string? NombreImagen { get; set; }
        public bool? Activo { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public string? CategoriaDescripcion { get; set; } // Solo para mostrar en la vista  
        public string? MarcaDescripcion { get; set; } // Solo para mostrar en la vista  
    }
}
