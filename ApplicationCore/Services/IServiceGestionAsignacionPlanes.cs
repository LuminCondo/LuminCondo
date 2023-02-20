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
        GestionAsignacionPlanes GetGestionAsignacionPlanesByID(int id);
    }
}
