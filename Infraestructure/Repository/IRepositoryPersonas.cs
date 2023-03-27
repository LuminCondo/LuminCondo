using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryPersonas
    {
        IEnumerable<Personas> GetPersonasxIDResidencia(int id);

        Personas Guardar(Personas personas);
        Personas GetPersonasByID(int id);
    }
}
