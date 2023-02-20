using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryGestionAsignacionPlanes : IRepositoryGestionAsignacionPlanes
    {
        IEnumerable<GestionAsignacionPlanes> lista = null;
        public IEnumerable<GestionAsignacionPlanes> GetGestionAsignacionPlanes()
        {
            try
            {
                
                using(MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled= false;

                    lista=ctx.GestionAsignacionPlanes.Include("Rubros").ToList();
                }
                return lista;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public GestionAsignacionPlanes GetGestionAsignacionPlanesByID(int id)
        {
            GestionAsignacionPlanes oGestionAsignacionPlane = null;
            try
            {
                
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    oGestionAsignacionPlane = ctx.GestionAsignacionPlanes.Find(id);
                }
                return oGestionAsignacionPlane;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }
    }
}
