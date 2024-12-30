using Curso.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Curso.Controllers
{
    public class MantenedorController : Controller
    {
        private readonly DbcarritoContext _context;
        private readonly IMyService _myService;

        public MantenedorController(DbcarritoContext context, MyService myService)
        {
            _context = context;
            _myService = myService;
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

        public IActionResult Producto()
        {
            return View();
        }
    }
}
