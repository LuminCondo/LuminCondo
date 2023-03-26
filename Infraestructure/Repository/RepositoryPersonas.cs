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
    public class RepositoryPersonas:IRepositoryPersonas
    {
        IEnumerable<Personas> lista = null;
        public Personas GetPersonasByID(int id)
        {
            Personas oPersonas = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    oPersonas = ctx.Personas.Find(id);
                }
                return oPersonas;
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

        public IEnumerable<Personas> GetPersonasxIDResidencia(int id)
        {
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    lista = ctx.Personas.Where(l => l.IDResidencia == id).ToList();
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

        public Personas Guardar(Personas personas)
        {
            int retorno = 0;
            Personas oPersonas = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oPersonas = GetPersonasByID(personas.IDCedula);

                if (oPersonas == null)
                {
                    ctx.Personas.Add(personas);

                    retorno = ctx.SaveChanges();
                }
                else
                {

                    ctx.Entry(personas).State = EntityState.Modified;

                    retorno = ctx.SaveChanges();
                }
            }

            if (retorno >= 0)
                oPersonas = GetPersonasByID(personas.IDCedula);

            return oPersonas;
        }
    }
}
