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
    public class RepositoryGestionReservas : IRepositoryGestionReservas
    {
        IEnumerable<GestionReservas> lista = null;

        public IEnumerable<GestionReservas> GetReservasByfecha(DateTime fecha)
        {
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    lista = ctx.GestionReservas.
                        Where(r => r.fecha == fecha).
                        Include("Espacios").
                        Include("Usuarios").
                        Include("EstadoReserva").
                        ToList();
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

        public GestionReservas GetReservaByID(int id)
        {
            GestionReservas oGestionReservas = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    oGestionReservas = ctx.GestionReservas.
                        Where(r => r.IDReserva == id).
                        Include("Espacios").
                        Include("Usuarios").
                        Include("EstadoReserva").
                        FirstOrDefault();
                }
                return oGestionReservas;
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

        public GestionReservas GetReservaByIDResidencia(int id)
        {
            throw new NotImplementedException();
        }

        public GestionReservas GetReservaByIDUsuario(int id)
        {
            GestionReservas oGestionReservas = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    oGestionReservas = ctx.GestionReservas.
                        Where(r => r.IDUsuario == id).FirstOrDefault();
                }
                return oGestionReservas;
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

        public IEnumerable<GestionReservas> GetReservas()
        {
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    lista = ctx.GestionReservas.
                        Include("Espacios").
                        Include("Usuarios").
                        Include("EstadoReserva").
                        ToList();
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

        public GestionReservas Guardar(GestionReservas reserva)
        {
            try
            {
                    int retorno = 0;
                GestionReservas oGestionReservas = null;

                using(MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oGestionReservas = GetReservaByID(reserva.IDReserva);

                    if (oGestionReservas == null)
                    {
                        ctx.GestionReservas.Add(reserva);

                        retorno = ctx.SaveChanges();
                    }
                    else
                    {
                        var objetoExistente = ctx.GestionReservas.FirstOrDefault(o => o.IDReserva == reserva.IDReserva);
                        objetoExistente.IDEstado = reserva.IDEstado;

                        retorno = ctx.SaveChanges();
                    }
                }

                if (retorno >= 0)
                    oGestionReservas = GetReservaByID(reserva.IDReserva);

                return oGestionReservas;
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
