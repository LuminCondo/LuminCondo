using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceEspacios : IServiceEspacios
    {
        public Espacios GetEspacioByID(int id)
        {
            IRepositoryEspacios repository = new RepositoryEspacios();
            return repository.GetEspacioByID(id);
        }

        public IEnumerable<Espacios> GetEspacios()
        {
            IRepositoryEspacios repository = new RepositoryEspacios();
            return repository.GetEspacios();
        }

        public IEnumerable<Espacios> GetEspaciosxFecha(DateTime fecha)
        {
            IRepositoryEspacios repository = new RepositoryEspacios();
            return repository.GetEspaciosxFecha(fecha);
        }
    }
}
