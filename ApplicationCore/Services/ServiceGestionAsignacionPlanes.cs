using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceGestionAsignacionPlanes : IServiceGestionAsignacionPlanes
    {
        public IEnumerable<GestionAsignacionPlanes> GetGestionAsignacionPlanes()
        {
            IRepositoryGestionAsignacionPlanes repository = new RepositoryGestionAsignacionPlanes();
            return repository.GetGestionAsignacionPlanes();
        }

        public GestionAsignacionPlanes GetGestionAsignacionPlanesByID(int id)
        {
            IRepositoryGestionAsignacionPlanes repository = new RepositoryGestionAsignacionPlanes();
            return repository.GetGestionAsignacionPlanesByID(id);
        }
    }
}
