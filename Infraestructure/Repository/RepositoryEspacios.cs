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
    public class RepositoryEspacios : IRepositoryEspacios
    {
        IEnumerable<Espacios> lista = null;

        public Espacios GetEspacioByID(int id)
        {
            Espacios oEspacios = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    oEspacios = ctx.Espacios.Find(id);
                }
                return oEspacios;
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

        public IEnumerable<Espacios> GetEspacios()
        {
            try
            {
                using(MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    lista = ctx.Espacios.ToList<Espacios>();
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

        public IEnumerable<Espacios> GetEspaciosxFecha(DateTime fecha)
        {
            IEnumerable<GestionReservas> listaReservas = null;
            IRepositoryGestionReservas _RepositoryReservas= new RepositoryGestionReservas();
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    lista = ctx.Espacios.ToList<Espacios>();
                    ctx.Configuration.LazyLoadingEnabled = false;
                    listaReservas = _RepositoryReservas.GetReservasByfecha(fecha);
                }

                if (listaReservas != null)
                {
                    List<Espacios> listaModificable = lista.ToList();

                    foreach (var item in listaReservas)
                    {
                        if (item.IDEstado!=3)
                        {
                            listaModificable.RemoveAll(x => x.IDEspacio == item.Espacios.IDEspacio);
                        }
                    }
                    lista = listaModificable;
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
    }
}
