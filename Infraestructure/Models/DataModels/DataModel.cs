using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models.DataModels
{
    public class DataModel:DbContext
    {
        public DataModel(): base("name=LuminCondoBD") { }
    }
}
