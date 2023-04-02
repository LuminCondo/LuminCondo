using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceGestionReservas
    {
        GestionReservas GetReservaByID(int id);
        GestionReservas GetReservaByIDUsuario(int id);
        GestionReservas GetReservaByIDResidencia(int id);
        GestionReservas Guardar(GestionReservas reserva);
        IEnumerable<GestionReservas> GetReservas();
    }
}
