using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceCarros
    {
        IEnumerable<Carros> GetCarrosxIDResidencia(int id);

        Carros Guardar(Carros carro);
        Carros GetCarrosByID(string IDPlaca);
    }
}
