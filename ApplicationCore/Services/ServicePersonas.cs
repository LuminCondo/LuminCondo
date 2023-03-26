using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServicePersonas:IServicePersonas
    {
        public IEnumerable<Personas> GetPersonasxIDResidencia(int id)
        {
            IRepositoryPersonas repository = new RepositoryPersonas();
            return repository.GetPersonasxIDResidencia(id);
        }

        public Personas Guardar(Personas personas)
        {
            IRepositoryPersonas repository = new RepositoryPersonas();
            return repository.Guardar(personas);
        }
    }
}
