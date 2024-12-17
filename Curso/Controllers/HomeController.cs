using System.Diagnostics;
using Curso.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Curso.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbcarritoContext _context;
        private readonly IMyService _myService;

        public HomeController(ILogger<HomeController> logger, DbcarritoContext context, MyService  myService)
        {
            _logger = logger;
            _context = context;
            _myService = myService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Usuarios()
        {
            return View(await _context.Usuarios.ToListAsync());
        }

        [HttpPost]
        public IActionResult CrearUsuario([FromBody]Usuario obj)
        {
            string mensaje = string.Empty;
            int resultado = _myService.CrearUsuario(obj, out mensaje);

            if (resultado > 0)
            {
                return Ok(new {message = mensaje});
            }
            else
            {
                return BadRequest(new {message = mensaje});
            }

        }

        [HttpPost]
        public IActionResult ActualizarUsuario([FromBody] Usuario obj)
        {
            string mensaje = string.Empty;
            bool resultado = _myService.ActualizarUsuario(obj, out mensaje);

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
        public IActionResult EliminarUsuario([FromBody]int id)
        {
            string mensaje = string.Empty;
            bool resultado = _myService.EliminarUsuario(id, out mensaje);
            if(resultado)
            {
                return Ok(new { message = mensaje });
            }
            else
            {
                return BadRequest(new { message = mensaje });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
