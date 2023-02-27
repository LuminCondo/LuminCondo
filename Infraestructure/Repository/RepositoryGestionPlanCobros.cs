using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryGestionPlanCobros : IRepositoryGestionPlanCobros
    {
        public void BorrarPlanCobros(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GestionPlanCobros> GetGestionPlanCobros()
        {
            try
            {
                IEnumerable<GestionPlanCobros> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    lista = ctx.GestionPlanCobros.ToList<GestionPlanCobros>();
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

        public GestionPlanCobros GetGestionPlanCobrosByID(int id)
        {
            GestionPlanCobros oGestionPlanCobros = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oGestionPlanCobros = ctx.GestionPlanCobros.
                        Where(l => l.IDPlan == id).
                        Include("GestionRubrosCobros").
                        FirstOrDefault();
                }
                return oGestionPlanCobros;
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

        public GestionPlanCobros Guardar(GestionPlanCobros gestionPlanCobros)
        {
            int retorno = 0;
            GestionPlanCobros oGestionPlanCobros = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oGestionPlanCobros = GetGestionPlanCobrosByID((int)gestionPlanCobros.IDPlan);
                IRepositoryGestionPlanCobros _RepositoryGestionPlanCobros = new RepositoryGestionPlanCobros();

                if (oGestionPlanCobros == null)
                {
                    ctx.GestionPlanCobros.Add(gestionPlanCobros);

                    retorno = ctx.SaveChanges();
                }
                else
                {
                    ctx.GestionPlanCobros.Add(gestionPlanCobros);

                    ctx.Entry(gestionPlanCobros).State = EntityState.Modified;

                    retorno = ctx.SaveChanges();

                }
            }

            if (retorno >= 0)
                oGestionPlanCobros = GetGestionPlanCobrosByID((int)gestionPlanCobros.IDPlan);

            return oGestionPlanCobros;
        }
    }
}
