﻿using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryGestionResidencias
    {
        IEnumerable<GestionResidencias> GetGestionResidencias();
        GestionResidencias GetGestionResidenciasByID(int id);
        GestionResidencias Guardar(GestionResidencias gestionResidencias);
    }
}
