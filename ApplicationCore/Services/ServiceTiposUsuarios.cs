using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceTiposUsuarios : IServiceTiposUsuarios
    {
        public TiposUsuarios GetTiposUsuariosByID(int id)
        {
            IRepositoryTiposUsuarios repository = new RepositoryTiposUsuarios();
            return repository.GetTiposUsuariosByID(id);
        }

        public IEnumerable<TiposUsuarios> GetTiposUsuarios()
        {
            IRepositoryTiposUsuarios repository = new RepositoryTiposUsuarios();
            return repository.GetTiposUsuarios();
        }
    }
}
