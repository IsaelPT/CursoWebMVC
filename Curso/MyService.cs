using AspNetCoreGeneratedDocument;
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


        // CRUD de Usuarios
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
                    string clave = Recursos.GenerarClave();
                    string asunto = "Registro de usuario";
                    string mensajeCorreo = "<h3>Su cuenta fue creada correctamente</h3><br><p>Su contrasena es: !clave!</P>".Replace("!clave!", clave);
                    bool respuesta = Recursos.EnviarCorreo(obj.Correro, asunto, mensajeCorreo);

                    if (!respuesta)
                    {
                        mensaje = "Error al enviar correo";
                        return 0;
                    }
                    else
                    {
                        obj.Clave = Recursos.ConvertToSha256("123");

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

                        _context.Database.ExecuteSqlRaw("EXEC sp_RegistrarUsuario @Nombres, @Apellidos, @Correo, @Clave, @Activo, @Mensaje OUTPUT, @Resultado OUTPUT",
                            new SqlParameter("@Nombres", obj.Nombres),
                            new SqlParameter("@Apellidos", obj.Apellidos),
                            new SqlParameter("@Correo", obj.Correro),
                            new SqlParameter("@Clave", obj.Clave),
                            new SqlParameter("@Activo", obj.Activo),
                            pmensaje,
                            resultado
                            );

                        mensaje = pmensaje.Value.ToString();

                        return Convert.ToInt32(resultado.Value);
                    }
                    
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

                    _context.Database.ExecuteSqlRaw("EXEC sp_EditarUsuario @Nombres,@Apellidos,@Correo,@IdUsuario,@Activo,@Mensaje output,@Resultado output",
                        new SqlParameter("@Nombres", obj.Nombres),
                        new SqlParameter("@Apellidos", obj.Apellidos),
                        new SqlParameter("@Correo", obj.Correro),
                        new SqlParameter("@IdUsuario", obj.IdUsuario),
                        new SqlParameter("@Activo", obj.Activo),
                        pmensaje,
                        resultado
                    );

                    mensaje = pmensaje.Value.ToString();

                    return Convert.ToBoolean(resultado.Value);
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
            var resultado = false;
            mensaje = string.Empty;

            try
            {
                var usuario = _context.Usuarios.Find(id);
                if (usuario == null)
                {
                    resultado = false;
                    mensaje = "Usuario no existe";
                }
                else
                {
                    _context.Usuarios.Remove(usuario);
                    _context.SaveChanges();
                    mensaje = "Usuario eliminado con exito";
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


        //CRUD de Categoria
        public int CrearCategoria(Categorium obj, out string mensaje)
        {
            mensaje = string.Empty;
            Categorium categoria;

            if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                mensaje = "La descripcion de la categoria no puede estar vacia";
            }

            if (string.IsNullOrEmpty(mensaje))
            {
                categoria = new Categorium
                {
                    Descripcion = obj.Descripcion,
                    Activo = obj.Activo
                };
                try
                {
                    _context.Categoria.Add(categoria);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
            }
            else
            {
                return 0;
            }
            return categoria.IdCategoria;
        }

        public bool ActulizarCategoria(Categorium obj, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado;

            if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                mensaje = "La descripcion de la categoria no puede estar vacia";
            }

            if (string.IsNullOrEmpty(mensaje)) 
            {
                try
                {
                    var categoria_existente = _context.Categoria.Find(obj.IdCategoria);
                    if (categoria_existente != null)
                    {
                        categoria_existente.Descripcion = obj.Descripcion;
                        categoria_existente.Activo = obj.Activo;
                        _context.SaveChanges();
                        resultado = true;
                    }
                    else
                    {
                        mensaje = "La categoria no existe";
                        resultado = false;
                    }
                }
                catch (Exception ex)
                {
                    resultado = false;
                    mensaje = ex.Message;
                }
            }
            else
            {
                resultado = false;
            }
            return resultado;
        }

        public bool EliminarCategoria(int id, out string mensaje)
        {
            var resultado = false;
            mensaje = string.Empty;

            try
            {
                var categoria = _context.Categoria.Find(id);
                if (categoria == null)
                {
                    resultado = false;
                    mensaje = "Categoria no existe";
                }
                else
                {
                    _context.Categoria.Remove(categoria);
                    _context.SaveChanges();
                    mensaje = "Categoria eliminada con exito";
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

        //CRUD de Marcas
        public int CrearMarca(Marca obj, out string mensaje)
        {
            mensaje = string.Empty;
            Marca marca;

            if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                mensaje = "La descripcion de la categoria no puede estar vacia";
            }

            if (string.IsNullOrEmpty(mensaje))
            {
                marca = new Marca
                {
                    Descripcion = obj.Descripcion,
                    Activo = obj.Activo
                };
                try
                {
                    _context.Marcas.Add(marca);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
            }
            else
            {
                return 0;
            }
            return marca.IdMarca;
        }

        public bool ActulizarMarca(Marca obj, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado;

            if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                mensaje = "La descripcion de la categoria no puede estar vacia";
            }

            if (string.IsNullOrEmpty(mensaje))
            {
                try
                {
                    var marca_existente = _context.Marcas.Find(obj.IdMarca);
                    if (marca_existente != null)
                    {
                        marca_existente.Descripcion = obj.Descripcion;
                        marca_existente.Activo = obj.Activo;
                        _context.SaveChanges();
                        resultado = true;
                    }
                    else
                    {
                        mensaje = "La categoria no existe";
                        resultado = false;
                    }
                }
                catch (Exception ex)
                {
                    resultado = false;
                    mensaje = ex.Message;
                }
            }
            else
            {
                resultado = false;
            }
            return resultado;
        }

        public bool EliminarMarca(int id, out string mensaje)
        {
            var resultado = false;
            mensaje = string.Empty;

            try
            {
                var marca = _context.Marcas.Find(id);
                if (marca == null)
                {
                    resultado = false;
                    mensaje = "Categoria no existe";
                }
                else
                {
                    _context.Marcas.Remove(marca);
                    _context.SaveChanges();
                    mensaje = "Categoria eliminada con exito";
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
