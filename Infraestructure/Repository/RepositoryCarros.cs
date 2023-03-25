using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryCarros:IRepositoryCarros
    {
        IEnumerable<Carros> lista = null;
        public Carros GetCarrosByID(string IDPlaca)
        {
            Carros oCarros = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    oCarros = ctx.Carros.Find(IDPlaca);
                }
                return oCarros;
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

        public IEnumerable<Carros> GetCarrosxIDResidencia(int id)
        {
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    lista = ctx.Carros.Where(l => l.IDResidencia == id).ToList();
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

        public Carros Guardar(Carros carro)
        {
            int retorno = 0;
            Carros oCarro = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oCarro = GetCarrosByID(carro.IDPlaca);

                if (oCarro == null)
                {
                    ctx.Carros.Add(carro);

                    retorno = ctx.SaveChanges();
                }
                else
                {
                    ctx.Carros.Add(carro);

                    ctx.Entry(carro).State = EntityState.Modified;

                    retorno = ctx.SaveChanges();
                }
            }

            if (retorno >= 0)
                oCarro = GetCarrosByID(carro.IDPlaca);

            return oCarro;
        }
    }
}
