using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IServiceGestionAsignacionPlanes
    {
        IEnumerable<GestionAsignacionPlanes> GetGestionAsignacionPlanes();
        IEnumerable<GestionAsignacionPlanes> GetHistorial(int? mes, int? anno, int? idResidencia, bool estado);
        IEnumerable<GestionAsignacionPlanes> GetHistorialGeneral(int? mes, int? anno, int? idResidencia);
        GestionAsignacionPlanes GetGestionAsignacionPlanesByID(int id);

        IEnumerable<GestionAsignacionPlanes> GetEstadodeCuentaByIDResidencia(int id);
        GestionAsignacionPlanes Guardar(GestionAsignacionPlanes gestionAsignacionPlanes);
        void GetGrafico(out string etiquetas, out string valores);
    }
}
