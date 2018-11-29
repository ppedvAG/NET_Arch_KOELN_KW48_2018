using System.Collections.Generic;

namespace ppedv.TastyMoon.DomainModel
{
    public class KaffeeMaschinenTyp : Entity
    {
        public string Hersteller { get; set; }
        public string Modell { get; set; }
        public HashSet<Rezept> Rezepte { get; set; } = new HashSet<Rezept>();
    }
}