using ppedv.TastyMoon.DomainModel;
using ppedv.TastyMoon.DomainModel.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ppedv.TastyMoon.Data.EF
{
    public class EfRepository : IRepository
    {

        EfContext context = new EfContext();

        public void Add<T>(T entity) where T : Entity
        {
            //if (typeof(T) == typeof(Rezept))
            //    context.Rezepte.Add(entity as Rezept);
            context.Set<T>().Add(entity);
        }

        public void Delete<T>(T entity) where T : Entity
        {
            context.Set<T>().Remove(entity);
        }

        public IQueryable<T> Query<T>() where T : Entity
        {
            return context.Set<T>();
        }

        public T GetById<T>(int id) where T : Entity
        {
            return context.Set<T>().Find(id);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update<T>(T entity) where T : Entity
        {
            //disconnected szenarien wie WCF, REST, WebAPI, ASP.MVC
            var loaded = GetById<T>(entity.Id);
            if (loaded != null)
            {
                context.Entry(loaded).CurrentValues.SetValues(entity);
            }
        }
    }
}
