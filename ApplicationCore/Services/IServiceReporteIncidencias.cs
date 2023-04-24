using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceReporteIncidencias
    {
        IEnumerable<ReporteIncidencias> GetReporteIncidencias();
        ReporteIncidencias GetReporteIncidenciasByID(int id);
        ReporteIncidencias Guardar(ReporteIncidencias reporteIncidencias);
        void BorrarReporteIncidencias(ReporteIncidencias reporteIncidencias);

        IEnumerable<ReporteIncidencias> GetHistorial(int? id);
    }
}