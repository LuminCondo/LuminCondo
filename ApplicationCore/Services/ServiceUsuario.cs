using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceUsuario : IServiceUsuario
    {       
        public IEnumerable<Usuarios> GetUsuarios()
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.GetUsuarios();
        }
        public Usuarios GetUsuarioByID(int id)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.GetUsuarioByID(id);
        }


    }
}
