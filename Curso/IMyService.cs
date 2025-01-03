using System;
using Curso.Models;

namespace Curso
{
    public interface IMyService
    {
        int CrearUsuario(Usuario obj, out string mensaje);
        bool ActualizarUsuario(Usuario obj, out string mensaje);
        bool EliminarUsuario(int id, out string mensaje);
        public int CrearCategoria(Categorium obj, out string mensaje);
        public bool ActulizarCategoria(Categorium obj, out string mensaje);
        public bool EliminarCategoria(int id, out string mensaje);
        public int CrearMarca(Marca obj, out string mensaje);
        public bool ActulizarMarca(Marca obj, out string mensaje);
        public bool EliminarMarca(int id, out string mensaje);
        public int CrearProducto(Producto obj, out string mensaje);
        public bool ActulizarProducto(Producto obj, out string mensaje);
        public bool GuardarDatosImagen(Producto obj, out string mensaje);
        public bool EliminarProducto(int id, out string mensaje);
    }
}
