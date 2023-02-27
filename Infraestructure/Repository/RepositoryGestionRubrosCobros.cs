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
    public class RepositoryGestionRubrosCobros : IRepositoryGestionRubrosCobros
    {
        public void BorrarRubroCobros(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GestionRubrosCobros> GetGestionRubrosCobros()
        {
            try
            {
                IEnumerable<GestionRubrosCobros> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    lista = ctx.GestionRubrosCobros.ToList<GestionRubrosCobros>();
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

        public GestionRubrosCobros GetGestionRubrosCobrosByID(int id)
        {
            try
            {
                GestionRubrosCobros oGestionRubrosCobro = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    oGestionRubrosCobro = ctx.GestionRubrosCobros.Find(id);
                }
                return oGestionRubrosCobro;
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

        public GestionRubrosCobros Guardar(GestionRubrosCobros gestionRubrosCobros)
        {
            int retorno = 0;
            GestionRubrosCobros oGestionRubrosCobros = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oGestionRubrosCobros = GetGestionRubrosCobrosByID((int)gestionRubrosCobros.IDRubro);
                IRepositoryGestionRubrosCobros _RepositoryGestionRubrosCobros = new RepositoryGestionRubrosCobros();

                if (oGestionRubrosCobros == null)
                {
                    ctx.GestionRubrosCobros.Add(gestionRubrosCobros);

                    retorno = ctx.SaveChanges();
                }
                else
                {
                    ctx.GestionRubrosCobros.Add(gestionRubrosCobros);

                    ctx.Entry(gestionRubrosCobros).State = EntityState.Modified;

                    retorno = ctx.SaveChanges();
                }
            }

            if (retorno >= 0)
                oGestionRubrosCobros = GetGestionRubrosCobrosByID((int)gestionRubrosCobros.IDRubro);

            return oGestionRubrosCobros;
        }

        
    }
}
