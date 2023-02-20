using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceGestionResidencias : IServiceGestionResidencias
    {
        public IEnumerable<GestionResidencias> GetGestionResidencias()
        {
            IRepositoryGestionResidencias repository = new RepositoryGestionResidencias();
            return repository.GetGestionResidencias();
        }

        public GestionResidencias GetGestionResidenciasByID(int id)
        {
            IRepositoryGestionResidencias repository = new RepositoryGestionResidencias();
            return repository.GetGestionResidenciasByID(id);
        }
    }
}
