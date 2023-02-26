using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceInformacion
    {
        IEnumerable<Informacion> GetInformacion();
        Informacion GetInformacionByID(int id);
        Informacion Guardar(Informacion informacion);
        void BorrarInformacion(int id);
    }
}