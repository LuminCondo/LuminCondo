using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceCarros:IServiceCarros
    {
        public IEnumerable<Carros> GetCarrosxIDResidencia(int id)
        {
            IRepositoryCarros repository = new RepositoryCarros();
            return repository.GetCarrosxIDResidencia(id);
        }

        public Carros Guardar(Carros carro)
        {
            IRepositoryCarros repository = new RepositoryCarros();
            return repository.Guardar(carro);
        }
    }
}
