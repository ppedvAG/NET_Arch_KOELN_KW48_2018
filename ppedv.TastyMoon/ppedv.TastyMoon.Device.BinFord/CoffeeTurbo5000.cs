using Binford.CoffeeTurbo5000;
using ppedv.TastyMoon.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ppedv.TastyMoon.Device.BinFord
{
    public class CoffeeTurbo5000 : IKaffeemaschine
    {
        private PowerDriver driver = new PowerDriver(5000);

        public KaffeeMaschinenTyp Typ { get; set; }
        public string Port { get; set; }

        public MaschinenStatus Status
        {
            get
            {
                switch (driver.GetStatus())
                {
                    case "error": return MaschinenStatus.OnFire;
                    case "beans": return MaschinenStatus.OutOfBeans;
                    default: return MaschinenStatus.Ready;
                }
            }
        }

        public void MacheKaffee(Rezept rezept)
        {
            driver.MakeCoffee(rezepte.ToList().IndexOf(rezept));
        }

        IEnumerable<Rezept> rezepte;
        public void SendeRezepte(IEnumerable<Rezept> rezepte)
        {
            this.rezepte = rezepte;
            rezepte.ToList().ForEach(x => driver.Add(x.Name, x.KaffeeMenge, x.MilchMenge, x.Zucker));
        }
    }
}
