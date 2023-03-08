using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryTipoInformacion
    {
        IEnumerable<TipoInformacion> GetTipoInformacion();
        TipoInformacion GetTipoInformacionByID(int id);
    }
}
