using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceInformacion : IServiceInformacion
    {
        public void BorrarInformacion(Informacion informacion)
        {
            IRepositoryInformacion repository = new RepositoryInformacion();
            repository.BorrarInformacion(informacion);
        }

        public IEnumerable<Informacion> GetInformacion()
        {
            IRepositoryInformacion repository = new RepositoryInformacion();
            return repository.GetInformacion();
        }
        public Informacion GetInformacionByID(int id)
        {
            IRepositoryInformacion repository = new RepositoryInformacion();
            return repository.GetInformacionByID(id);
        }

        public Informacion Guardar(Informacion informacion)
        {
            IRepositoryInformacion repository = new RepositoryInformacion();
            return repository.Guardar(informacion);
        }
    }
}