using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IServiceGestionPlanCobros
    {
        IEnumerable<GestionPlanCobros> GetGestionPlanCobros();
        GestionPlanCobros GetGestionPlanCobrosByID(int id);
        GestionPlanCobros Guardar(GestionPlanCobros gestionPlanCobros);
        void BorrarPlanCobros(GestionPlanCobros gestionPlanCobros);
    
    }
}
