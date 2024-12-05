using Curso.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Curso
{
    public class MyService:IMyService
    {
        private readonly DbcarritoContext _context;
        public MyService(DbcarritoContext context) 
        {
            _context = context;
        }

        public int CrearUsuario(Usuario obj, out string mensaje)
        {

            mensaje = string.Empty;
            
            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                mensaje = "El nombre del usuario no puede estar vacio";
            }
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                mensaje = "El apellido del usuario no puede estar vacio";
            }
            else if (string.IsNullOrEmpty(obj.Correro) || string.IsNullOrWhiteSpace(obj.Correro))
            {
                mensaje = "El correo del usuario no puede estar vacio";
            }

            if (string.IsNullOrEmpty(mensaje))
            {
                try
                {
                    string clave = "test123";
                    obj.Clave = Recursos.ConvertToSha256(clave);

                    var pmensaje = new SqlParameter
                    {
                        ParameterName = "@Mensaje",
                        Size = 500,
                        Direction = System.Data.ParameterDirection.Output
                    };

                    var resultado = new SqlParameter
                    {
                        ParameterName = "@Resultado",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Output,
                    };

                    _context.Database.ExecuteSqlRaw("SELECT sp_RegistrarUsuario @Nombres,@Apellidos,@Correo,@Clave,@Activo,@Mensaje output,@Resultado output",
                        new SqlParameter("@Nombres", obj.Nombres),
                        new SqlParameter("@Apellidos", obj.Apellidos),
                        new SqlParameter("@Correo", obj.Correro),
                        new SqlParameter("@Clave", obj.Clave),
                        new SqlParameter("@Activo", obj.Activo),
                        pmensaje,
                        resultado
                        );

                    mensaje = pmensaje.ToString();

                    return Convert.ToInt32(resultado);
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public bool ActualizarUsuario(Usuario obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                mensaje = "El nombre del usuario no puede estar vacio";
            }
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                mensaje = "El apellido del usuario no puede estar vacio";
            }
            else if (string.IsNullOrEmpty(obj.Correro) || string.IsNullOrWhiteSpace(obj.Correro))
            {
                mensaje = "El correo del usuario no puede estar vacio";
            }

            if (string.IsNullOrEmpty(mensaje))
            {
                try
                {
                    var pmensaje = new SqlParameter
                    {
                        ParameterName = "@Mensaje",
                        Size = 500,
                        Direction = System.Data.ParameterDirection.Output
                    };

                    var resultado = new SqlParameter
                    {
                        ParameterName = "@Resultado",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Output,
                    };

                    _context.Database.ExecuteSqlRaw("SELECT sp_EditarUsuario @Nombres,@Apellidos,@Correo,@IdUsuario,@Activo,@Mensaje output,@Resultado output",
                        new SqlParameter("@Nombres", obj.Nombres),
                        new SqlParameter("@Apellidos", obj.Apellidos),
                        new SqlParameter("@Correo", obj.Correro),
                        new SqlParameter("@IdUsuario", obj.IdUsuario),
                        new SqlParameter("@Activo", obj.Activo),
                        pmensaje,
                        resultado
                    );

                    mensaje = pmensaje.ToString();

                    return Convert.ToBoolean(resultado);
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                    return false;
                }
            }
            else
            {
                return false;
            }

            
        }

        public bool EliminarUsuario(int id, out string mensaje)
        {
            var resultado = false
            mensaje = string.Empty;

            try
            {
                var usuario = _context.Usuarios.Find(id);
                if (usuario == null)
                {
                    resultado = false;
                }
                else
                {
                    _context.Usuarios.Remove(usuario);
                    _context.SaveChanges();
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                resultado = false;
            }

            return resultado;
        }
    }
}
