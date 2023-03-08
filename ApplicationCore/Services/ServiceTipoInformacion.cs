﻿using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceTipoInformacion : IServiceTipoInformacion
    {
        public TipoInformacion GetTipoInformacioByID(int id)
        {
            IRepositoryTipoInformacion repository = new RepositoryTipoInformacion();
            return repository.GetTipoInformacionByID(id);
        }

        public IEnumerable<TipoInformacion> GetTipoInformacion()
        {
            IRepositoryTipoInformacion repository = new RepositoryTipoInformacion();
            return repository.GetTipoInformacion();
        }
    }
}
