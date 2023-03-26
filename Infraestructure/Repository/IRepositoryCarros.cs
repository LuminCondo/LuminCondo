using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryCarros
    {
        IEnumerable<Carros> GetCarrosxIDResidencia(int id);
        Carros GetCarrosByID(string IDPlaca);
        Carros Guardar(Carros carro);  
    }
}
