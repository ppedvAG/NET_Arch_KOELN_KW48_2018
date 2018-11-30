using System;
using System.Collections.Generic;
using System.Text;

namespace ppedv.TastyMoon.DomainModel
{
    public interface IKaffeemaschine
    {
        KaffeeMaschinenTyp Typ { get; set; }
        string Port { get; set; }
        void SendeRezepte(IEnumerable<Rezept> rezepte);
        void MacheKaffee(Rezept rezept);

        MaschinenStatus Status { get; }

    }
}
