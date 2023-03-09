using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryUsuario
    {
        IEnumerable<Usuarios> GetUsuarios();
        Usuarios GetUsuarioByID(int id);

        Usuarios Save(Usuarios usuario);
        Usuarios GetUsuario(string email, string contrasenna);
    }
}
