using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceGestionPlanCobros : IServiceGestionPlanCobros
    {
        public IEnumerable<GestionPlanCobros> GetGestionPlanCobros()
        {
            IRepositoryGestionPlanCobros repository = new RepositoryGestionPlanCobros();
            return repository.GetGestionPlanCobros();
        }

        public GestionPlanCobros GetGestionPlanCobrosByID(int id)
        {
            IRepositoryGestionPlanCobros repository = new RepositoryGestionPlanCobros();
            return repository.GetGestionPlanCobrosByID(id);
        }
    }
}
