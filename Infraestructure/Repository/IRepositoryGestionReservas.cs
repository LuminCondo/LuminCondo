using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryGestionReservas
    {
        GestionReservas GetReservaByID(int id);
        GestionReservas GetReservaByIDUsuario(int id);
        GestionReservas GetReservaByIDResidencia(int id);
        IEnumerable<GestionReservas> GetReservasByfecha(DateTime fecha);
        GestionReservas Guardar(GestionReservas reserva);
        IEnumerable<GestionReservas> GetReservas();
    }
}
