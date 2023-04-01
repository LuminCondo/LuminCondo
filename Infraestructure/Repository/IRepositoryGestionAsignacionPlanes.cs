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
        IEnumerable<GestionAsignacionPlanes> GetHistorial(int? mes, int? anno, int? idResidencia, bool estado);
        IEnumerable<GestionAsignacionPlanes> GetHistorialGeneral(int? mes, int? anno, int? idResidencia);
        IEnumerable<GestionAsignacionPlanes> GetEstadodeCuentaByIDResidencia(int id);
        GestionAsignacionPlanes GetGestionAsignacionPlanesByID(int id);
        GestionAsignacionPlanes Guardar(GestionAsignacionPlanes gestionAsignacionPlanes);
    }
}
