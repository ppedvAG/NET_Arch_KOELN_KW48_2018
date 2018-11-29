using ppedv.TastyMoon.DomainModel;
using ppedv.TastyMoon.DomainModel.Contracts;
using System;
using System.Linq;

namespace ppedv.TastyMoon.Logic
{
    public class Core
    {
        public IRepository Repository { get; private set; }

        public Core(IRepository repo)
        {
            Repository = repo;
        }

        public Rezept GetRezeptWithMostUsedMilk()
        {
            return Repository.GetAll<Rezept>().OrderBy(x => x.MilchMenge).FirstOrDefault();
        }

        public Core() : this(new Data.EF.EfRepository())
        { }
    }
}
