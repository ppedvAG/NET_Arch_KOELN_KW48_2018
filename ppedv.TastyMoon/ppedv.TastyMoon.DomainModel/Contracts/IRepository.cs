﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ppedv.TastyMoon.DomainModel.Contracts
{
    public interface IRepository<T> where T : Entity
    {
        IQueryable<T> Query();

        T GetById(int id);

        void Add(T entity);

        void Delete(T entity);

        void Update(T entity);

    }

    public interface IRezeptRepository : IRepository<Rezept>
    {
        IEnumerable<Rezept> GetRezepteMitMeisterMilch();
    }

    public interface IUnitOfWork
    {
        IRezeptRepository RezeptRepo { get; }
        IRepository<T> GetRepo<T>() where T : Entity;

        void Save();
    }

}
