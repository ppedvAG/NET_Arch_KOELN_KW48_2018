using System.Collections.Generic;

namespace ppedv.TastyMoon.DomainModel
{
    public class KaffeeMaschinenTyp : Entity
    {
        public string Hersteller { get; set; }
        public string Modell { get; set; }
        public virtual HashSet<Rezept> Rezepte { get; set; } = new HashSet<Rezept>();
    }
}