using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceEstadoResidencia : IServiceEstadoResidencia
    {
        public IEnumerable<EstadoResidencia> GetEstadoResidencia()
        {
            IRepositoryEstadoResidencia repository = new RepositoryEstadoResidencia();
            return repository.GetEstadoResidencia();
        }

        public EstadoResidencia GetEstadoResidenciaByID(int id)
        {
            IRepositoryEstadoResidencia repository = new RepositoryEstadoResidencia();
            return repository.GetEstadoResidenciaByID(id);
        }
    }
}
