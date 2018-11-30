using ppedv.TastyMoon.DomainModel;
using ppedv.TastyMoon.DomainModel.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ppedv.TastyMoon.Data.EF
{
    public class EfUnitOfWork : IUnitOfWork
    {
        EfContext context = new EfContext();

        public IRezeptRepository RezeptRepo => new EfRezeptRepository(context);

        public IRepository<T> GetRepo<T>() where T : Entity
        {
            return new EfRepository<T>(context);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

    public class EfRezeptRepository : EfRepository<Rezept>, IRezeptRepository
    {
        public EfRezeptRepository(EfContext context) : base(context)
        { }

        public IEnumerable<Rezept> GetRezepteMitMeisterMilch()
        {
            return context.Rezepte.OrderByDescending(x => x.MilchMenge).ThenBy(x => x.Name);
        }
    }

    public class EfRepository<T> : IRepository<T> where T : Entity
    {
        protected EfContext context;
        public EfRepository(EfContext context)
        {
            this.context = context;
        }

        public void Add(T entity)
        {
            //if (typeof(T) == typeof(Rezept))
            //    context.Rezepte.Add(entity as Rezept);
            context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public IQueryable<T> Query()
        {
            return context.Set<T>();
        }

        public T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }


        public void Update(T entity)
        {
            //disconnected szenarien wie WCF, REST, WebAPI, ASP.MVC
            var loaded = GetById(entity.Id);
            if (loaded != null)
            {
                context.Entry(loaded).CurrentValues.SetValues(entity);
            }
        }
    }
}
