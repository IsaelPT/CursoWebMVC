using System.Data;
using System.Diagnostics;
using ClosedXML.Excel;
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
        #region Dashboard
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Reporte(DateTime fechaInicio, DateTime fechaFin, string idTransaccion)
        {
            var info = await _context.DetalleVenta.
                Include(d => d.IdProductoNavigation).
                Include(d => d.IdVentaNavigation).
                ThenInclude(v => v.IdClienteNavigation).
                Where(dv => dv.IdVentaNavigation.FechaVenta >= fechaInicio &&
                            dv.IdVentaNavigation.FechaVenta <= fechaFin &&
                            (string.IsNullOrEmpty(idTransaccion) || dv.IdVentaNavigation.IdTransaccion == idTransaccion)).
                ToListAsync();

            if (info != null)
            {
                return Json(new { data = info });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Exportar(DateTime fechaInicio, DateTime fechaFin, string idTransaccion)
        {
            var info = await _context.DetalleVenta.
                Include(d => d.IdProductoNavigation).
                Include(d => d.IdVentaNavigation).
                ThenInclude(v => v.IdClienteNavigation).
                Where(dv => dv.IdVentaNavigation.FechaVenta >= fechaInicio &&
                            dv.IdVentaNavigation.FechaVenta <= fechaFin &&
                            (string.IsNullOrEmpty(idTransaccion) || dv.IdVentaNavigation.IdTransaccion == idTransaccion)).
                ToListAsync();

            DataTable dt = new DataTable();
            dt.Columns.Add("Fecha Venta", typeof(string));
            dt.Columns.Add("Cliente", typeof(string));
            dt.Columns.Add("Producto", typeof(string));
            dt.Columns.Add("Precio", typeof(decimal));
            dt.Columns.Add("Cantidad", typeof(int));
            dt.Columns.Add("Total", typeof(decimal));
            dt.Columns.Add("Id Transaccion", typeof(string));

            foreach(var item in info)
            {
                dt.Rows.Add(new Object[]
                {
                    item.IdVentaNavigation.FechaVenta,
                    item.IdVentaNavigation.IdClienteNavigation.Nombres,
                    item.IdProductoNavigation.Nombre,
                    item.Cantidad,
                    item.Total,
                    item.IdVentaNavigation.IdTransaccion
                });
            }

            dt.TableName = "Datos";

            using (XLWorkbook wb = new XLWorkbook()) 
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream()) 
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Reporte Venta" + DateTime.Now.ToString() + ".xlsx");
                }
            }
        }
        [HttpGet]
        public async Task<IActionResult> Cantidades()
        {
            var productos = await _context.Productos.CountAsync();
            var clientes = await _context.Clientes.CountAsync();
            var ventas = await _context.Venta.CountAsync();

            if (productos >= 0 && clientes >= 0 && ventas >= 0)
            {
                return Ok(new { cantProductos = productos, cantClientes = clientes, cantVentas = ventas });
            }
            else
            {
                return BadRequest();
            }
        }

        #endregion

        #region Usuario
        [HttpGet]
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

        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
