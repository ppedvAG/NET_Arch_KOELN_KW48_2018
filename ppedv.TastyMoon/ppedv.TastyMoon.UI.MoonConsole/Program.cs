using ppedv.TastyMoon.DomainModel;
using ppedv.TastyMoon.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ppedv.TastyMoon.UI.MoonConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** Tasty Moon v0.1 ***");

            var core = new Core();

            if (core.Repository.Query<Rezept>().Count() == 0)
                core.CreateDemodaten();

            foreach (var rez in core.Repository.Query<Rezept>().ToList())
            {
                Console.WriteLine($"{rez.Name} {rez.BohnenArt} Milch: {rez.MilchMenge}ml");
                foreach (var m in rez.KaffeeMaschinen)
                {
                    Console.WriteLine($"\t{m.Hersteller} {m.Modell}");
                }
            }

            Console.WriteLine($"Meiste Milch: {core.GetRezeptWithMostUsedMilk()?.Name}");

            Console.WriteLine("Ende");
            Console.ReadLine();
        }
    }
}
