﻿using ppedv.TastyMoon.DomainModel;
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
            return Repository.Query<Rezept>()
                             .OrderByDescending(x => x.MilchMenge)
                             .ThenBy(x => x.Name)
                             .FirstOrDefault();
        }

        public void CreateDemodaten()
        {
            var m1 = new KaffeeMaschinenTyp() { Hersteller = "Bura", Modell = "Z9" };
            var m2 = new KaffeeMaschinenTyp() { Hersteller = "Benseo", Modell = "Padsiff 2000" };
            var m3 = new KaffeeMaschinenTyp() { Hersteller = "Baeco", Modell = "PowerDeluxe" };

            var r1 = new Rezept() { Name = "Kaffee Crema", KaffeeMenge = 180, BohnenArt = BohnenArt.Crema };
            var r2 = new Rezept() { Name = "Kaffee Normal", KaffeeMenge = 180, BohnenArt = BohnenArt.Cafe };
            var r3 = new Rezept() { Name = "Cappuccino", KaffeeMenge = 50, BohnenArt = BohnenArt.Espresso, MilchArt = MilchArt.Schaum, MilchMenge = 150 };
            var r4 = new Rezept() { Name = "Espresso", KaffeeMenge = 50, BohnenArt = BohnenArt.Espresso };
            var r5 = new Rezept() { Name = "Ristretto", KaffeeMenge = 30, BohnenArt = BohnenArt.Espresso };
            var r6 = new Rezept() { Name = "Ladde", KaffeeMenge = 30, BohnenArt = BohnenArt.Cafe, MilchArt = MilchArt.Sahnig, MilchMenge = 150 };

            new[] { r1, r2, r3, r4, r5, r6 }.ToList().ForEach(x => m1.Rezepte.Add(x));
            new[] { r1, r2, r4, r5, }.ToList().ForEach(x => m2.Rezepte.Add(x));
            new[] { r1, r3, r4, r5, }.ToList().ForEach(x => m3.Rezepte.Add(x));

            new[] { m1, m2, m3 }.ToList().ForEach(x => Repository.Add(x));

            Repository.Save();
        }

        public Core() : this(new Data.EF.EfRepository())
        { }
    }
}
