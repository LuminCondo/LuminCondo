using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceGestionRubrosCobros : IServiceGestionRubrosCobros
    {
        public IEnumerable<GestionRubrosCobros> GetGestionRubrosCobros()
        {
            IRepositoryGestionRubrosCobros repository = new RepositoryGestionRubrosCobros();
            return repository.GetGestionRubrosCobros();
        }

        public GestionRubrosCobros GetGestionRubrosCobrosByID(int id)
        {
            IRepositoryGestionRubrosCobros repository = new RepositoryGestionRubrosCobros();
            return repository.GetGestionRubrosCobrosByID(id);
        }

        public GestionRubrosCobros Guardar(GestionRubrosCobros gestionRubrosCobros)
        {
            IRepositoryGestionRubrosCobros repository = new RepositoryGestionRubrosCobros();
            return repository.Guardar(gestionRubrosCobros);
        }
    }
}
