using Curso.Models;
using Curso.Models.NewFolder1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Curso.Controllers
{
    public class MantenedorController : Controller
    {
        private readonly DbcarritoContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMyService _myService;

        public MantenedorController(DbcarritoContext context, MyService myService, IConfiguration configuration)
        {
            _context = context;
            _myService = myService;
            _configuration = configuration;
        }

        //CRUD Categorias
        #region Categorias
        [HttpGet]
        public async Task<IActionResult> Categoria()
        {
            return View(await _context.Categoria.ToListAsync());
        }

        [HttpPost]
        public IActionResult CrearCategoria([FromBody] Categorium obj)
        {
            string mensaje = string.Empty;
            int resultado = _myService.CrearCategoria(obj, out mensaje);

            if (resultado > 0)
            {
                return Ok(new { message = mensaje });
            }
            else
            {
                return BadRequest(new { message = mensaje });
            }

        }

        [HttpPost]
        public IActionResult ActualizarCategoria([FromBody] Categorium obj)
        {
            string mensaje = string.Empty;
            bool resultado = _myService.ActulizarCategoria(obj, out mensaje);

            if (resultado)
            {
                return Ok(new { message = mensaje });
            }
            else
            {
                return BadRequest(new { message = mensaje });
            }

        }

        [HttpPost]
        public IActionResult EliminarCategoria([FromBody] int id)
        {
            string mensaje = string.Empty;
            bool resultado = _myService.EliminarCategoria(id, out mensaje);
            if (resultado)
            {
                return Ok(new { message = mensaje });
            }
            else
            {
                return BadRequest(new { message = mensaje });
            }
        }
        #endregion

        //CRUD Marcas
        #region Marcas
        [HttpGet]
        public async Task<IActionResult> Marca()
        {
            return View(await _context.Marcas.ToListAsync());
        }

        [HttpPost]
        public IActionResult CrearMarca([FromBody] Marca obj)
        {
            string mensaje = string.Empty;
            int resultado = _myService.CrearMarca(obj, out mensaje);

            if (resultado > 0)
            {
                return Ok(new { message = mensaje });
            }
            else
            {
                return BadRequest(new { message = mensaje });
            }

        }

        [HttpPost]
        public IActionResult ActualizarMarca([FromBody] Marca obj)
        {
            string mensaje = string.Empty;
            bool resultado = _myService.ActulizarMarca(obj, out mensaje);

            if (resultado)
            {
                return Ok(new { message = mensaje });
            }
            else
            {
                return BadRequest(new { message = mensaje });
            }

        }

        [HttpPost]
        public IActionResult EliminarMarca([FromBody] int id)
        {
            string mensaje = string.Empty;
            bool resultado = _myService.EliminarMarca(id, out mensaje);
            if (resultado)
            {
                return Ok(new { message = mensaje });
            }
            else
            {
                return BadRequest(new { message = mensaje });
            }
        }
        #endregion

        //CRUD Productos
        #region Productos
        public async Task<IActionResult> Producto()
        {
            var productos = _context.Productos.Include(p => p.IdMarcaNavigation).
                Include(p => p.IdCategoriaNavigation).
                Select(p => new ProductoDTS
            {
                IdProducto = p.IdProducto,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                IdMarca = p.IdMarca,
                IdCategoria = p.IdCategoria,
                Precio = p.Precio,
                Stock = p.Stock,
                RutaImagen = p.RutaImagen,
                NombreImagen = p.NombreImagen,
                Activo = p.Activo,
                FechaRegistro = p.FechaRegistro,
                CategoriaDescripcion = p.IdCategoriaNavigation.Descripcion,
                MarcaDescripcion = p.IdMarcaNavigation.Descripcion
            });
            ViewData["Categorias"] = new SelectList(_context.Categoria, "IdCategoria", "Descripcion");
            ViewData["Marcas"] = new SelectList(_context.Marcas, "IdMarca", "Descripcion");

            return View(await productos.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CrearProducto([FromForm] Producto obj, IFormFile imagen, string precio)
        {
            string mensaje = string.Empty;
            
            obj.Precio = decimal.Parse(precio.Replace(".",","));
            int resultado_producto = _myService.CrearProducto(obj, out mensaje);
            obj.IdProducto = resultado_producto;

            if (imagen != null && string.IsNullOrEmpty(mensaje))
            {
                string ruta_guardar = _configuration["RutaImagenes"];
                string extension = Path.GetExtension(imagen.FileName);
                string nombre_imagen = string.Concat(obj.IdProducto, extension);
                string ruta_completa = Path.Combine(ruta_guardar, nombre_imagen);

                using (var stream = new FileStream(ruta_completa, FileMode.Create))
                {
                    await imagen.CopyToAsync(stream);
                }

                obj.NombreImagen = nombre_imagen;
                obj.RutaImagen = ruta_guardar;
            }

            bool resultado_imagen = _myService.GuardarDatosImagen(obj, out mensaje);

            if (resultado_producto > 0 && resultado_imagen)
            {
                return Ok(new { message = mensaje });
            }
            else
            {
                return BadRequest(new { message = mensaje });
            }

        }

        [HttpPost]
        public async Task<IActionResult> ActualizarProducto([FromForm] Producto obj, IFormFile imagen)
        {
            string mensaje = string.Empty;
            bool resultado_producto = _myService.ActulizarProducto(obj, out mensaje);

            if (imagen != null)
            {
                string ruta_guardar = _configuration["RutaImagenes"];
                string extension = Path.GetExtension(imagen.FileName);
                string nombre_imagen = string.Concat(obj.IdProducto, extension);
                string ruta_completa = Path.Combine(ruta_guardar, nombre_imagen);

                using (var stream = new FileStream(ruta_completa, FileMode.Create))
                {
                    await imagen.CopyToAsync(stream);
                }

                obj.NombreImagen = nombre_imagen;
                obj.RutaImagen = ruta_guardar;
            }

            bool resultado_imagen = _myService.GuardarDatosImagen(obj, out mensaje);

            if (resultado_producto && resultado_imagen)
            {
                return Ok(new { message = mensaje });
            }
            else
            {
                return BadRequest(new { message = mensaje });
            }

        }

        [HttpPost]
        public async Task<IActionResult> ImagenProducto([FromBody] int id)
        {
            Producto producto = _context.Productos.Find(id);
            string txtBase64 = Recursos.ConvertToBase64(Path.Combine(producto.RutaImagen, producto.NombreImagen));

            if (producto != null)
            {
                return Ok(new { txtbase64 = txtBase64, extension = Path.GetExtension(producto.NombreImagen) });
            }
            else
            {
                return BadRequest();
            }

            
        }

        [HttpPost]
        public IActionResult EliminarProducto([FromBody] int id)
        {
            string mensaje = string.Empty;
            bool resultado = _myService.EliminarProducto(id, out mensaje);


            if (resultado)
            {
                return Ok(new { message = mensaje });
            }
            else
            {
                return BadRequest(new { message = mensaje });
            }
        }

        #endregion
    }
}
