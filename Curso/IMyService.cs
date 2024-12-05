using System;
using Curso.Models;

namespace Curso
{
    public interface IMyService
    {
        int CrearUsuario(Usuario obj, out string mensaje);
        bool ActualizarUsuario(Usuario obj, out string mensaje);

        bool EliminarUsuario(int id, out string mensaje);
    }
}
