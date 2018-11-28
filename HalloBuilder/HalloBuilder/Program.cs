using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            Schrank meinSchrank = new Schrank.Builder().SetTüren(6)
                                                       .SetOberfläche(Oberfläche.Lackiert)
                                                       .SetFarbe("Blauz")
                                                       .Create();

            Console.WriteLine("ende");
            Console.ReadLine();
        }
    }

    class Schrank
    {
        public int Türen { get; private set; }
        public Oberfläche Oberfläche { get; private set; }
        public string Farbe { get; private set; }

        private Schrank()
        { }

        public class Builder
        {
            private Schrank schrank = new Schrank();

            public Builder SetTüren(int anzahlTüren)
            {
                if (anzahlTüren > 7)
                    throw new ArgumentException("Es sind max. 7 Türen möglich");
                if (anzahlTüren < 2)
                    throw new ArgumentException("Es müssen mind. 2 Türen verbaut werde");

                schrank.Türen = anzahlTüren;
                return this;
            }

            public Builder SetFarbe(string farbe)
            {
                if (string.IsNullOrWhiteSpace(farbe))
                    throw new ArgumentException("Null oder Leer oder was??");

                if (schrank.Oberfläche != Oberfläche.Lackiert)
                    throw new ArgumentException("Nur lackierte Schränke können eine eigene Farbe haben");

                schrank.Farbe = farbe;
                return this;
            }

            public Builder SetOberfläche(Oberfläche oberfläche)
            {
                schrank.Oberfläche = oberfläche;
                return this;
            }


            public Schrank Create() => schrank;

        }
    }

    enum Oberfläche
    {
        Unbehandelt,
        Gewachst,
        Lackiert
    }
}
