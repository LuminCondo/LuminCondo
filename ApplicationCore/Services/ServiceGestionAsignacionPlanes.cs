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
        public IEnumerable<GestionAsignacionPlanes> GetEstadodeCuentaByIDResidencia(int id)
        {
            IRepositoryGestionAsignacionPlanes repository = new RepositoryGestionAsignacionPlanes();
            return repository.GetEstadodeCuentaByIDResidencia(id);
        }

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

        public IEnumerable<GestionAsignacionPlanes> GetHistorial(int? mes, int? anno, int? idResidencia, bool estado)
        {
            IRepositoryGestionAsignacionPlanes repository = new RepositoryGestionAsignacionPlanes();
            return repository.GetHistorial(mes, anno, idResidencia, estado);
        }

        public IEnumerable<GestionAsignacionPlanes> GetHistorialGeneral(int? mes, int? anno, int? idResidencia)
        {
            IRepositoryGestionAsignacionPlanes repository = new RepositoryGestionAsignacionPlanes();
            return repository.GetHistorialGeneral(mes, anno, idResidencia);
        }

        public GestionAsignacionPlanes Guardar(GestionAsignacionPlanes gestionAsignacionPlanes)
        {
            IRepositoryGestionAsignacionPlanes repository = new RepositoryGestionAsignacionPlanes();
            return repository.Guardar(gestionAsignacionPlanes);
        }
        public void GetGrafico(out string etiquetas1, out string valores1)
        {
            IRepositoryGestionAsignacionPlanes repository = new RepositoryGestionAsignacionPlanes();
            repository.GetGrafico(out string etiquetas, out string valores);
            etiquetas1 = etiquetas;
            valores1 = valores;
        }
    }
}
