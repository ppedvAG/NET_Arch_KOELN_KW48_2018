using Autofac;
using ppedv.TastyMoon.DomainModel;
using ppedv.TastyMoon.DomainModel.Contracts;
using ppedv.TastyMoon.Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ppedv.TastyMoon.UI.MoonConsole
{
    class Program
    {
        public static IContainer Container { get; private set; }

        public static void BuildContainer()
        {
            var builder = new ContainerBuilder();
            //builder.RegisterType<Data.EF.EfRepository>().As<IRepository>();
            //builder.RegisterType<FakeBeeper>().As<IBeeper>();

            //pfad muss absolut sein
            string path = Path.GetFullPath(@"..\..\..\ppedv.TastyMoon.Data.EF\bin\debug\ppedv.TastyMoon.Data.EF.dll");

            //Assemble laden
            Assembly extAss = Assembly.LoadFile(path);

            //erstbeste Klasse mit IBeeper wird verwendet
            builder.RegisterAssemblyTypes(extAss).As<IRepository>();
            builder.RegisterType<Core>().UsingConstructor(typeof(IRepository));
            Container = builder.Build();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("*** Tasty Moon v0.1 ***");
            BuildContainer();

            using (var scope = Container.BeginLifetimeScope())
            {
                //var core = new Core(Container.Resolve<IRepository>());
                var core = Container.Resolve<Core>();


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

            }
            Console.WriteLine("Ende");
            Console.ReadLine();
        }
    }
}
