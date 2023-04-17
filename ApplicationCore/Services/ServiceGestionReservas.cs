using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceGestionReservas : IServiceGestionReservas
    {
        public IEnumerable<GestionReservas> GetHistorial(int? id)
        {
            IRepositoryGestionReservas repository = new RepositoryGestionReservas();
            return repository.GetHistorial(id);
        }

        public GestionReservas GetReservaByID(int id)
        {
            IRepositoryGestionReservas repository = new RepositoryGestionReservas();
            return repository.GetReservaByID(id);
        }

        public GestionReservas GetReservaByIDResidencia(int id)
        {
            IRepositoryGestionReservas repository = new RepositoryGestionReservas();
            return repository.GetReservaByIDResidencia(id);
        }

        public GestionReservas GetReservaByIDUsuario(int id)
        {
            IRepositoryGestionReservas repository = new RepositoryGestionReservas();
            return repository.GetReservaByIDUsuario(id);
        }

        public IEnumerable<GestionReservas> GetReservas()
        {
            IRepositoryGestionReservas repository = new RepositoryGestionReservas();
            return repository.GetReservas();
        }

        public GestionReservas Guardar(GestionReservas reserva)
        {
            IRepositoryGestionReservas repository = new RepositoryGestionReservas();
            return repository.Guardar(reserva);
        }
    }
}
