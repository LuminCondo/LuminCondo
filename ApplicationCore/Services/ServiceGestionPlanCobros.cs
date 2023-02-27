﻿using Infraestructure.Models;
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
        public void BorrarPlanCobros(int id)
        {
            throw new NotImplementedException();
        }

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

        public GestionPlanCobros Guardar(GestionPlanCobros gestionPlanCobros)
        {
            IRepositoryGestionPlanCobros repository = new RepositoryGestionPlanCobros();
            return repository.Guardar(gestionPlanCobros);
        }
    }
}
