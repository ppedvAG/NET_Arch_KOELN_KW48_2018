using System.Collections.Generic;

namespace ppedv.TastyMoon.DomainModel
{
    public class Rezept : Entity
    {
        public string Name { get; set; }
        public int KaffeeMenge { get; set; }
        public BohnenArt BohnenArt { get; set; }
        public int WasserMenge { get; set; }
        public int WasserTemp { get; set; }
        public int MilchMenge { get; set; }
        public MilchArt MilchArt { get; set; }
        public int Zucker { get; set; }
        public HashSet<KaffeeMaschinenTyp> KaffeeMaschinen { get; set; } = new HashSet<KaffeeMaschinenTyp>();
    }
}