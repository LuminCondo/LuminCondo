using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IServiceGestionRubrosCobros
    {
        IEnumerable<GestionRubrosCobros> GetGestionRubrosCobros();
        GestionRubrosCobros GetGestionRubrosCobrosByID(int id);
        GestionRubrosCobros Guardar(GestionRubrosCobros gestionRubrosCobros);
    }
}
