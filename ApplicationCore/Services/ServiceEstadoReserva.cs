using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceEstadoReserva : IServiceEstadoReserva
    {
        public IEnumerable<EstadoReserva> GetEstadoReserva()
        {
            IRepositoryEstadoReserva repository = new RepositoryEstadoReserva();
            return repository.GetEstadoReserva();
        }

        public EstadoReserva GetEstadoReservaByID(int id)
        {
            IRepositoryEstadoReserva repository = new RepositoryEstadoReserva();
            return repository.GetEstadoReservaByID(id);
        }
    }
}
