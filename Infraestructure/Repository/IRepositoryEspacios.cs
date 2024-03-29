﻿using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryEspacios
    {
        IEnumerable<Espacios> GetEspacios();
        IEnumerable<Espacios> GetEspaciosxFecha(DateTime fecha);
        Espacios GetEspacioByID(int id);
    }
}
