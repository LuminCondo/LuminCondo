﻿using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryGestionPlanCobros
    {
        IEnumerable<GestionPlanCobros> GetGestionPlanCobros();
        GestionPlanCobros GetGestionPlanCobrosByID(int id);
        GestionPlanCobros Guardar(GestionPlanCobros gestionPlanCobros, string[] selectedRubrosCobros);
        void BorrarPlanCobros(GestionPlanCobros gestionPlanCobros);
    }
}
