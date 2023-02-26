using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryInformacion : IRepositoryInformacion
    {
        public void BorrarInformacion(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Informacion> GetInformacion()
        {
            try
            {
                IEnumerable<Informacion> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    lista = ctx.Informacion.Include("TipoInformacion").ToList();
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

        public Informacion GetInformacionByID(int id)
        {
            try
            {
                Informacion oInformacion = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    oInformacion = ctx.Informacion.Find(id);
                }
                return oInformacion;
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

        public Informacion Guardar(Informacion informacion)
        {
            int retorno = 0;
            Informacion oInformacion = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oInformacion = GetInformacionByID((int)informacion.IDInformacion);
                IRepositoryInformacion _RepositoryInformacion = new RepositoryInformacion();

                if (oInformacion == null)
                {
                    ctx.Informacion.Add(informacion);

                    retorno = ctx.SaveChanges();
                }
                else
                {
                    ctx.Informacion.Add(informacion);
                    
                    ctx.Entry(informacion).State = EntityState.Modified;
                    
                    retorno = ctx.SaveChanges();

                }
            }

            if (retorno >= 0)
                oInformacion = GetInformacionByID((int)informacion.IDInformacion);

            return oInformacion;
        }
    }
}