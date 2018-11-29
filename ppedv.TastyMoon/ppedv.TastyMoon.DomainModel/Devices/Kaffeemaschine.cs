using System;
using System.Collections.Generic;
using System.Text;

namespace ppedv.TastyMoon.DomainModel
{
    public abstract class Kaffeemaschine
    {
        public KaffeeMaschinenTyp Typ { get; set; }
        public string Port { get; set; }
        public abstract void SendeRezepte(IEnumerable<Rezept> repepte);
        public abstract void MacheKaffee(Rezept rezept);

        public abstract MaschinenStatus Status { get; }

    }
}
