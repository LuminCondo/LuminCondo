using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceEstadoReserva
    {
        IEnumerable<EstadoReserva> GetEstadoReserva();
        EstadoReserva GetEstadoReservaByID(int id);
    }
}
