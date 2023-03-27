using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServicePersonas
    {
        IEnumerable<Personas> GetPersonasxIDResidencia(int id);

        Personas Guardar(Personas personas);
        Personas GetPersonasByID(int id);
    }
}
