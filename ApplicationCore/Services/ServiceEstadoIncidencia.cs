using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceEstadoIncidencia : IServiceEstadoIncidencia
    {
        public IEnumerable<EstadoIncidencia> GetEstadoIncidencia()
        {
            IRepositoryEstadoIncidencia repository = new RepositoryEstadoIncidencia();
            return repository.GetEstadoIncidencia();
        }

        public EstadoIncidencia GetEstadoIncidenciaByID(int id)
        {
            IRepositoryEstadoIncidencia repository = new RepositoryEstadoIncidencia();
            return repository.GetEstadoIncidenciaByID(id);
        }
    }
}
