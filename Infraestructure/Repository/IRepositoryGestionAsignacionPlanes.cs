using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryGestionAsignacionPlanes
    {
        IEnumerable<GestionAsignacionPlanes> GetGestionAsignacionPlanes();
        IEnumerable<GestionAsignacionPlanes> GetEstadodeCuentaByIDResidencia(int id);
        GestionAsignacionPlanes GetGestionAsignacionPlanesByID(int id);
    }
}
