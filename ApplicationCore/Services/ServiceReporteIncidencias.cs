using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceReporteIncidencias : IServiceReporteIncidencias
    {
        public void BorrarReporteIncidencias(ReporteIncidencias reporteIncidencias)
        {
            IRepositoryReporteIncidencias repository = new RepositoryReporteIncidencias();
            repository.BorrarReporteIncidencias(reporteIncidencias);
        }

        public IEnumerable<ReporteIncidencias> GetReporteIncidencias()
        {
            IRepositoryReporteIncidencias repository = new RepositoryReporteIncidencias();
            return repository.GetReporteIncidencias();
        }

        public ReporteIncidencias GetReporteIncidenciasByID(int id)
        {
            IRepositoryReporteIncidencias repository = new RepositoryReporteIncidencias();
            return repository.GetReporteIncidenciasByID(id);
        }

        public ReporteIncidencias Guardar(ReporteIncidencias reporteIncidencias)
        {
            IRepositoryReporteIncidencias repository = new RepositoryReporteIncidencias();
            return repository.Guardar(reporteIncidencias);
        }
    }
}